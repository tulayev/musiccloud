import { createContext, useContext } from 'react'
import CommonStore from './commonStore'
import TrackStore from './trackStore'

interface Store {
    trackStore: TrackStore
    commonStore: CommonStore
}

export const store: Store = {
    trackStore: new TrackStore(),
    commonStore: new CommonStore()
}

export const StoreContext = createContext(store)

export function useStore() {
    return useContext(StoreContext)
}