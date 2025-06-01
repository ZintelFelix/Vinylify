<template>
  <section class="home">
    <h1>Willkommen bei Vinylify</h1>
    <p>Suche, verwalte und teile deine Schallplattensammlung.</p>
  </section>
</template>

<script lang="ts">
import { defineComponent } from 'vue'

export default defineComponent({
  name: 'HomeView',
})
</script>

<style scoped>
.home {
  text-align: center;
  margin-top: 4rem;
}
.home h1 {
  font-size: 2.5rem;
  margin-bottom: 1rem;
}
.home p {
  font-size: 1.2rem;
  color: #ccc;
}
</style>
