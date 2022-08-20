import { createContext, useContext } from 'react'
import CommonStore from './commonStore'
import ModalStore from './modalStore'
import TrackStore from './trackStore'
import UserStore from './userStore'

interface Store {
    trackStore: TrackStore
    commonStore: CommonStore
    userStore: UserStore
    modalStore: ModalStore
}

export const store: Store = {
    trackStore: new TrackStore(),
    commonStore: new CommonStore(),
    userStore: new UserStore(),
    modalStore: new ModalStore()
}

export const StoreContext = createContext(store)

export function useStore() {
    return useContext(StoreContext)
}