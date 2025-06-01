import axios from 'axios'

const API = import.meta.env.VITE_API_BASE_URL as string

export interface CollectionItem {
  userId?: string // wird beim Hinzufügen vom Header abgeleitet
  discogsId: number
  title: string
  artist: string
  thumbUrl: string
  year: number
}

/** Liest die Collection des aktuellen Users aus. */
export async function fetchCollection(): Promise<CollectionItem[]> {
  const user = localStorage.getItem('discogs_username')!
  if (!user) throw new Error('Kein Benutzer angemeldet')

  // Debug-Log: prüfen, ob user korrekt geladen wird
  console.log('[discogsCollectionService] fetchCollection, user=', user)

  const { data } = await axios.get<CollectionItem[]>(`${API}/api/collection`, {
    headers: {
      'X-Auth-UserName': user, // Header muss unbedingt gesetzt sein
    },
  })
  return data
}

/** Fügt ein Release zur Collection hinzu. */
export async function addToCollection(item: CollectionItem): Promise<void> {
  const user = localStorage.getItem('discogs_username')!
  if (!user) throw new Error('Kein Benutzer angemeldet')

  item.userId = user // optional, Controller überschreibt aber mit Header

  // Debug-Log: prüfen, ob Header und Payload korrekt sind
  console.log('[discogsCollectionService] addToCollection: user=', user, ', payload=', item)

  await axios.post(`${API}/api/collection/add`, item, {
    headers: {
      'X-Auth-UserName': user, // ausschlaggebend, damit Backend den User erkennt
    },
  })

  console.log('[discogsCollectionService] POST an /api/collection/add abgeschlossen')
}

/** Entfernt ein Release aus der Collection. */
export async function removeFromCollection(discogsId: number): Promise<void> {
  const user = localStorage.getItem('discogs_username')!
  if (!user) throw new Error('Kein Benutzer angemeldet')

  // Debug-Log: prüfen, ob Header korrekt ist
  console.log(
    '[discogsCollectionService] removeFromCollection: user=',
    user,
    'discogsId=',
    discogsId,
  )

  await axios.delete(`${API}/api/collection/remove/${discogsId}`, {
    headers: {
      'X-Auth-UserName': user, // Header darf auch hier nicht fehlen
    },
  })

  console.log('[discogsCollectionService] DELETE an /api/collection/remove abgeschlossen')
}
