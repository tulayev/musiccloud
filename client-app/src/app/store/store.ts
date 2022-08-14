import { createContext, useContext } from 'react'
import TrackStore from './trackStore'

interface Store {
    trackStore: TrackStore
}

export const store: Store = {
    trackStore: new TrackStore()
}

export const StoreContext = createContext(store)

export function useStore() {
    return useContext(StoreContext)
}