<template>
  <div class="wishlist">
    <h1>Meine Wunschliste</h1>
    <ul v-if="items.length">
      <li v-for="item in items" :key="item.discogsId" class="wish-item">
        <img :src="item.thumbUrl" width="50" alt="Cover" />
        <span>{{ item.artist }} – {{ item.title }}</span>
        <button @click="onRemove(item.discogsId)">✕</button>
      </li>
    </ul>
    <p v-else>Deine Wunschliste ist leer.</p>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, ref } from 'vue'
import type { WishListItem } from '@/services/discogsWishlistService'
import { fetchWishlist, removeFromWishlist } from '@/services/discogsWishlistService'

export default defineComponent({
  name: 'WishListView',
  setup() {
    const items = ref<WishListItem[]>([])

    const load = async () => {
      items.value = await fetchWishlist()
    }

    const onRemove = async (discogsId: number) => {
      await removeFromWishlist(discogsId)
      await load()
    }

    onMounted(load)
    return { items, onRemove }
  },
})
</script>

<style scoped>
.wishlist {
  max-width: 600px;
  margin: auto;
}
.wish-item {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  margin-bottom: 1rem;
}
.wish-item button {
  margin-left: auto;
  background: transparent;
  border: none;
  color: #c00;
  font-size: 1.2rem;
  cursor: pointer;
}
</style>
