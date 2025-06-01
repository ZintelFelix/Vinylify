import { createRouter, createWebHistory } from 'vue-router'
import type { RouteRecordRaw } from 'vue-router'
import { useAuthStore } from '@/stores/authStore'

import HomeView from '@/views/HomeView.vue'
import LoginView from '@/views/Login.vue'
import CallbackView from '@/views/CallbackView.vue'
import CollectionView from '@/views/CollectionView.vue'
import SearchView from '@/views/SearchView.vue'
import WishListView from '@/views/WishListView.vue'

const routes: RouteRecordRaw[] = [
  { path: '/', name: 'Home', component: HomeView },
  { path: '/login', name: 'Login', component: LoginView },
  { path: '/auth/callback', name: 'Callback', component: CallbackView },
  { path: '/collection', name: 'Collection', component: CollectionView },
  { path: '/search', name: 'Search', component: SearchView },
  { path: '/wishlist', name: 'Wishlist', component: WishListView },
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})

router.beforeEach((to, _, next) => {
  const auth = useAuthStore()
  auth.loadFromStorage()

  const needAuth = ['Collection', 'Search', 'Wishlist']
  if (needAuth.includes(to.name as string) && !auth.isLoggedIn) {
    return next({ name: 'Login' })
  }
  next()
})

export default router
