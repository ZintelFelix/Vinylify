/// <reference types="vite/client" />

// Shim für Vue SFCs – nutzt keine '{}'- und 'any'-Typen mehr
declare module '*.vue' {
  import type { DefineComponent } from 'vue'
  const component: DefineComponent<Record<string, unknown>, Record<string, unknown>, unknown>
  export default component
}

// Deine VITE_...-Variablen
interface ImportMetaEnv {
  readonly VITE_API_BASE_URL: string
  readonly VITE_DISCOGS_AUTHORIZE_URL: string
}

interface ImportMeta {
  readonly env: ImportMetaEnv
}
