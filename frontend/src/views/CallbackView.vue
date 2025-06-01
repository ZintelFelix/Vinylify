<script lang="ts">
import { defineComponent, onMounted, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/authStore'
import type { AccessTokenRequest } from '@/services/discogsAuthService'
import { finalizeLogin } from '@/services/discogsAuthService'

export default defineComponent({
  name: 'CallbackView',
  setup() {
    const loading = ref(true)
    const error = ref<string | null>(null)
    const router = useRouter()
    const auth = useAuthStore()

    onMounted(async () => {
      try {
        const params = new URLSearchParams(window.location.search)
        const oauthToken = params.get('oauth_token') || ''
        const oauthVerifier = params.get('oauth_verifier') || ''
        const oauthSecret = localStorage.getItem('discogs_oauth_secret') || ''

        if (!oauthToken || !oauthVerifier || !oauthSecret) {
          console.error('[CallbackView] OAuth-Parameter fehlen!', {
            oauthToken,
            oauthVerifier,
            oauthSecret,
          })
          throw new Error('OAuth-Parameter unvollständig.')
        }

        const payload: AccessTokenRequest = {
          oauthToken,
          oauthTokenSecret: oauthSecret,
          oauthVerifier,
        }
        const resp = await finalizeLogin(payload)
        console.log('[CallbackView] finalizeLogin response:', resp)

        auth.setTokens(resp)

        if (!resp.userName) {
          console.error('[CallbackView] ACHTUNG: resp.userName ist leer!', resp)
        } else {
          localStorage.setItem('discogs_username', resp.userName)
          console.log('[CallbackView] discogs_username gesetzt auf:', resp.userName)
        }

        router.replace({ name: 'Collection' })
      } catch (err: unknown) {
        console.error('[CallbackView] Error:', err)
        error.value = (err as Error).message
      } finally {
        loading.value = false
      }
    })

    return { loading, error }
  },
})
</script>

<style scoped>
.callback-container {
  text-align: center;
  margin-top: 4rem;
}
.error {
  color: red;
}
</style>
