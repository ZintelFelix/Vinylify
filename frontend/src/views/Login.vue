<template>
  <div class="login">
    <h1>Login mit Discogs</h1>
    <button @click="onLogin" class="btn-login">Mit Discogs anmelden</button>
    <p v-if="error" class="error">{{ error }}</p>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue'
import { loginWithDiscogs } from '@/services/discogsAuthService'

export default defineComponent({
  name: 'LoginView',
  setup() {
    const error = ref<string | null>(null)

    const onLogin = async () => {
      console.log('[LoginView] onLogin clicked')
      try {
        await loginWithDiscogs()
      } catch (e: unknown) {
        error.value = (e instanceof Error ? e.message : String(e)) || 'Unbekannter Fehler'
      }
    }

    return { onLogin, error }
  },
})
</script>

<style scoped>
.login {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 4rem;
}
.btn-login {
  background: #1db954;
  border: none;
  color: white;
  padding: 0.8rem 1.2rem;
  font-size: 1rem;
  border-radius: 4px;
  cursor: pointer;
}
.btn-login:hover {
  background: #17a44b;
}
.error {
  color: #f55;
  margin-top: 1rem;
}
</style>
