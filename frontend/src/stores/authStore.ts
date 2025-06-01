// frontend/src/stores/authStore.ts

import { defineStore } from 'pinia'
import type { AccessTokenResponse } from '@/services/discogsAuthService'

export interface AuthState {
  oauthToken: string
  oauthTokenSecret: string
  userName: string
}

export const useAuthStore = defineStore('auth', {
  state: (): AuthState => ({
    oauthToken: '',
    oauthTokenSecret: '',
    userName: '',
  }),

  getters: {
    isLoggedIn: (state) => !!state.oauthToken,
  },

  actions: {
    setTokens(data: AccessTokenResponse) {
      this.oauthToken = data.oauthToken
      this.oauthTokenSecret = data.oauthTokenSecret
      this.userName = data.userName
      localStorage.setItem('discogs_access_token', data.oauthToken)
      localStorage.setItem('discogs_access_secret', data.oauthTokenSecret)
      localStorage.setItem('discogs_username', data.userName) // <--- MUSS exakt data.userName sein!
    },

    loadFromStorage() {
      // Lädt die gespeicherten Werte (oder setzt leere Strings)
      this.oauthToken = localStorage.getItem('discogs_access_token') || ''
      this.oauthTokenSecret = localStorage.getItem('discogs_access_secret') || ''
      this.userName = localStorage.getItem('discogs_username') || ''
    },

    clear() {
      // Setzt alle Werte zurück und löscht sie aus localStorage
      this.oauthToken = ''
      this.oauthTokenSecret = ''
      this.userName = ''
      localStorage.removeItem('discogs_access_token')
      localStorage.removeItem('discogs_access_secret')
      localStorage.removeItem('discogs_username')
    },
  },
})
