<template>
  <div class="search">
    <h1>Suche</h1>
    <form @submit.prevent="onSearch">
      <input
        v-model="query"
        type="search"
        placeholder="Album oder Künstler..."
        aria-label="Suchbegriff"
      />
      <button type="submit" :disabled="loading || !query">Suchen</button>
    </form>

    <p v-if="loading">Lade…</p>
    <p v-else-if="error" class="error">{{ error }}</p>

    <ul v-else class="results">
      <li v-for="r in results" :key="r.id" class="result-item">
        <img :src="r.thumb" alt="Cover" width="50" />
        <div class="info">
          <strong>{{ r.title }}</strong>
          <small> ({{ r.year }}) Rating: {{ r.community.rating.average.toFixed(1) }} </small>
        </div>
        <!-- WICHTIG: type="button" verhindern, dass das Formular submitet -->
        <button type="button" @click="addCollection(r)">＋</button>
        <button type="button" @click="addWishlist(r)">❤</button>
      </li>
    </ul>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import type { SearchRelease } from '@/models/searchResult'
import { searchReleases } from '@/services/discogsSearchService'
import type { CollectionItem } from '@/services/discogsCollectionService'
import { addToCollection } from '@/services/discogsCollectionService'
import type { WishListItem } from '@/services/discogsWishlistService'
import { addToWishlist } from '@/services/discogsWishlistService'

export default defineComponent({
  name: 'SearchView',
  setup() {
    const query = ref<string>('')
    const loading = ref<boolean>(false)
    const error = ref<string | null>(null)
    const results = ref<SearchRelease[]>([])

    async function onSearch(): Promise<void> {
      if (!query.value) return
      loading.value = true
      error.value = null
      try {
        const resp = await searchReleases(query.value)
        results.value = resp.results
      } catch (e: unknown) {
        error.value = (e as Error).message || 'Fehler bei der Suche.'
      } finally {
        loading.value = false
      }
    }

    async function addCollection(r: SearchRelease): Promise<void> {
      console.log('[SearchView] addCollection aufgerufen für ID:', r.id)
      const item: CollectionItem = {
        discogsId: r.id,
        title: r.title,
        artist: r.title.split(' – ')[0] || '',
        thumbUrl: r.thumb,
        year: r.year,
      }

      try {
        await addToCollection(item)
        console.log('[SearchView] addToCollection Axios-Aufruf abgeschlossen für ID:', r.id)
      } catch (err) {
        console.error('[SearchView] addToCollection Fehler:', err)
      }
    }

    async function addWishlist(r: SearchRelease): Promise<void> {
      alert('Wishlist-Handler ausgelöst!')
      console.log('[SearchView] addWishlist aufgerufen für ID:', r.id)
      const item: WishListItem = {
        discogsId: r.id,
        title: r.title,
        artist: r.title.split(' – ')[0] || '', // MUSS wirklich existieren!
        thumbUrl: r.thumb,
      }

      try {
        await addToWishlist(item)
        console.log('[SearchView] addToWishlist Axios-Aufruf abgeschlossen für ID:', r.id)
      } catch (err) {
        console.error('[SearchView] addToWishlist Fehler:', err)
      }
    }

    return {
      query,
      loading,
      error,
      results,
      onSearch,
      addCollection,
      addWishlist,
    }
  },
})
</script>

<style scoped>
.search {
  max-width: 600px;
  margin: auto;
  padding: 1rem;
}
form {
  display: flex;
  gap: 0.5rem;
  margin-bottom: 1rem;
}
.results {
  list-style: none;
  padding: 0;
}
.result-item {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  margin-bottom: 1rem;
}
.info {
  flex: 1;
}
button[disabled] {
  opacity: 0.5;
  cursor: not-allowed;
}
.error {
  color: red;
}
</style>
