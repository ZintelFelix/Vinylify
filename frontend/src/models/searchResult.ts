export interface Pagination {
  page: number
  pages: number
  per_page: number
  items: number
}

export interface SearchRelease {
  id: number
  title: string
  thumb: string
  year: number
  uri: string
  type: string
  community: {
    rating: {
      average: number
    }
  }
}

export interface SearchResponse {
  pagination: Pagination
  results: SearchRelease[]
}
