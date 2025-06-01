<script setup lang="ts">
import { RouterView } from 'vue-router'
</script>

<template>
  <div id="app">
    <NavBar />
    <main class="main-content">
      <router-view />
    </main>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue'
import NavBar from '@/components/NavBar.vue'

export default defineComponent({
  name: 'App',
  components: {
    NavBar,
  },
})
</script>

<style>
/* Globale Basis‐Styles */
body {
  margin: 0;
  font-family: Avenir, Helvetica, Arial, sans-serif;
  background: #121212;
  color: #eee;
}

#app {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}

.main-content {
  flex: 1;
  padding: 2rem;
}
</style>
