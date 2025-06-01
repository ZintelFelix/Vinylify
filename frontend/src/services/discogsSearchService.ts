import axios from 'axios'
import type { SearchResponse } from '@/models/searchResult'

const API = import.meta.env.VITE_API_BASE_URL as string

/**
 * Ruft die Discogs-Suche über Dein Backend auf.
 */
export async function searchReleases(query: string, page = 1): Promise<SearchResponse> {
  // 1) Lese die Werte aus dem Local Storage
  const token = localStorage.getItem('discogs_access_token') ?? ''
  const secret = localStorage.getItem('discogs_access_secret') ?? ''

  // 2) Gib in der Console aus, was gleich gesendet wird
  console.log('[searchReleases] Header → X-Auth-Token:', token)
  console.log('[searchReleases] Header → X-Auth-Secret:', secret)
  console.log('[searchReleases] Query-Parameter → q:', query, 'page:', page)

  // 3) Führe den HTTP-Request aus
  const { data } = await axios.get<SearchResponse>(`${API}/api/search`, {
    params: { q: query, page },
    headers: {
      // Diese Header muss der Backend-Controller später auslesen
      'X-Auth-Token': token,
      'X-Auth-Secret': secret,
    },
  })
  return data
}
