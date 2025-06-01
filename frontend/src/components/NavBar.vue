<template>
  <nav class="navbar">
    <router-link to="/" class="logo">Vinylify</router-link>
    <ul class="menu">
      <li><router-link to="/">Home</router-link></li>
      <li v-if="!auth.isLoggedIn">
        <router-link to="/login">Login</router-link>
      </li>
      <li v-else class="user-info">
        <span>Hi, {{ auth.userName }}</span>
        <button @click="onLogout" class="btn-logout">Logout</button>
      </li>
      <li v-if="auth.isLoggedIn"><router-link to="/collection">Meine Sammlung</router-link></li>
      <li v-if="auth.isLoggedIn"><router-link to="/search">Suche</router-link></li>
      <li v-if="auth.isLoggedIn"><router-link to="/wishlist">Wunschliste</router-link></li>
    </ul>
  </nav>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/authStore'

export default defineComponent({
  name: 'NavBar',
  setup() {
    const auth = useAuthStore()
    const router = useRouter()

    function onLogout() {
      auth.clear()
      router.push({ name: 'Home' })
    }

    return { auth, onLogout }
  },
})
</script>

<style scoped>
.navbar {
  display: flex;
  align-items: center;
  padding: 0.5rem 1rem;
  background: #1f1f1f;
  color: #fff;
}
.logo {
  font-size: 1.5rem;
  font-weight: bold;
  margin-right: 2rem;
  text-decoration: none;
  color: #fff;
}
.menu {
  list-style: none;
  display: flex;
  gap: 1rem;
  align-items: center;
  margin: 0;
  padding: 0;
}
.menu a {
  text-decoration: none;
  color: #fff;
}
.menu a.router-link-active {
  border-bottom: 2px solid #1db954;
}
.user-info {
  display: flex;
  align-items: center;
  gap: 0.5rem;
}
.btn-logout {
  background: transparent;
  border: 1px solid #fff;
  color: #fff;
  padding: 0.2rem 0.6rem;
  border-radius: 4px;
  cursor: pointer;
}
.btn-logout:hover {
  background: #333;
}
</style>
