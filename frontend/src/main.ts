import './assets/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { useAuthStore } from '@/stores/authStore'

import App from './App.vue'
import router from './router'

const app = createApp(App)

const pinia = createPinia()
app.use(pinia)
app.use(router)

// Direkt nach der Pinia-Registrierung den Auth-Store initialisieren:
const auth = useAuthStore()
auth.loadFromStorage()

app.mount('#app')
