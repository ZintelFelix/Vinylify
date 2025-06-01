import axios from 'axios'

const API = import.meta.env.VITE_API_BASE_URL as string

export interface WishListItem {
  userId?: string // wird im Backend über Header abgeleitet
  discogsId: number
  title: string
  artist: string
  thumbUrl: string
}

/** Liest die WishList des aktuellen Users aus. */
export async function fetchWishlist(): Promise<WishListItem[]> {
  const user = localStorage.getItem('discogs_username')!
  if (!user) throw new Error('Kein Benutzer angemeldet für die Wunschliste')

  console.log('[discogsWishlistService] fetchWishlist, user =', user)

  const { data } = await axios.get<WishListItem[]>(`${API}/api/wishlist`, {
    headers: {
      'X-Auth-UserName': user,
    },
  })
  return data
}

/** Fügt ein Release zur Wunschliste hinzu. */
export async function addToWishlist(item: WishListItem): Promise<void> {
  const user = localStorage.getItem('discogs_username')!
  if (!user) throw new Error('Kein Benutzer angemeldet für die Wunschliste')

  item.userId = user

  // Debug-Ausgabe
  console.log('[addToWishlist] User:', user)
  console.log('[addToWishlist] Payload:', item)

  await axios.post(`${API}/api/wishlist/add`, item, {
    headers: {
      'X-Auth-UserName': user,
    },
  })
}

/** Entfernt ein Release aus der Wunschliste. */
export async function removeFromWishlist(discogsId: number): Promise<void> {
  const user = localStorage.getItem('discogs_username')!
  if (!user) throw new Error('Kein Benutzer angemeldet für die Wunschliste')

  console.log(
    '[discogsWishlistService] removeFromWishlist → user =',
    user,
    ', discogsId =',
    discogsId,
  )

  await axios.delete(`${API}/api/wishlist/remove/${discogsId}`, {
    headers: {
      'X-Auth-UserName': user,
    },
  })

  console.log('[discogsWishlistService] DELETE an /api/wishlist/remove/{discogsId} abgeschlossen')
}
