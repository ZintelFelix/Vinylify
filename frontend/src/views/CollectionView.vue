<template>
  <div class="collection">
    <h1>Meine Sammlung</h1>
    <ul v-if="items.length">
      <li v-for="item in items" :key="item.discogsId" class="col-item">
        <img :src="item.thumbUrl" width="50" alt="Cover" />
        <div>{{ item.artist }} – {{ item.title }} ({{ item.year }})</div>
        <button @click="onRemove(item.discogsId)">✕</button>
      </li>
    </ul>
    <p v-else>Deine Sammlung ist leer.</p>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from 'vue'
import type { CollectionItem } from '@/services/discogsCollectionService'
import { fetchCollection, removeFromCollection } from '@/services/discogsCollectionService'

export default defineComponent({
  name: 'CollectionView',
  setup() {
    const items = ref<CollectionItem[]>([])

    const load = async () => {
      items.value = await fetchCollection()
    }

    const onRemove = async (id: number) => {
      await removeFromCollection(id)
      await load()
    }

    onMounted(load)
    return { items, onRemove }
  },
})
</script>

<style scoped>
.collection {
  max-width: 600px;
  margin: auto;
}
.col-item {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  margin-bottom: 1rem;
}
.col-item button {
  margin-left: auto;
  background: transparent;
  border: none;
  color: #c00;
  font-size: 1.2rem;
  cursor: pointer;
}
</style>
