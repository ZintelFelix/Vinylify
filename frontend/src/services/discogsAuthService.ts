import axios from 'axios'

const API_BASE = import.meta.env.VITE_API_BASE_URL as string
console.log('[discogsAuthService] API_BASE =', API_BASE)

export interface RequestTokenResponse {
  oauthToken: string
  oauthTokenSecret: string
  authorizeUrl: string
}

export interface AccessTokenRequest {
  oauthToken: string
  oauthTokenSecret: string
  oauthVerifier: string
}

export interface AccessTokenResponse {
  oauthToken: string
  oauthTokenSecret: string
  userName: string
}

/**
 * Holt einen Request-Token vom Backend und leitet den Browser zu Discogs weiter.
 */
export async function loginWithDiscogs(): Promise<void> {
  try {
    console.log('[loginWithDiscogs] Aufruf gestartet')
    const { data } = await axios.get<RequestTokenResponse>(`${API_BASE}/api/auth/request-token`)
    console.log('[loginWithDiscogs] request-token response:', data)

    localStorage.setItem('discogs_oauth_secret', data.oauthTokenSecret)
    window.location.href = data.authorizeUrl
  } catch (err) {
    console.error('[loginWithDiscogs] Fehler beim Anfordern des Request-Tokens:', err)
    alert('Login fehlgeschlagen. Bitte prüfe die Konsole auf Details.')
    throw err
  }
}

/**
 * Tauscht Request-Token + Verifier in der Callback-View gegen Access-Token.
 */
export async function finalizeLogin(payload: AccessTokenRequest): Promise<AccessTokenResponse> {
  try {
    console.log('[finalizeLogin] Aufruf mit payload:', payload)
    const { data } = await axios.post<AccessTokenResponse>(
      `${API_BASE}/api/auth/access-token`,
      payload,
    )
    console.log('[finalizeLogin] access-token response:', data)
    return data
  } catch (err) {
    console.error('[finalizeLogin] Fehler beim Tauschen des Access-Tokens:', err)
    throw err
  }
}
