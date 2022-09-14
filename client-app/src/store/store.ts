import { createContext, useContext } from 'react'
import CommentStore from './commentStore'
import CommonStore from './commonStore'
import FileStore from './fileStore'
import ModalStore from './modalStore'
import PlayListStore from './playListStore'
import TrackStore from './trackStore'
import UserStore from './userStore'

interface Store {
    trackStore: TrackStore
    commonStore: CommonStore
    userStore: UserStore
    modalStore: ModalStore
    playListStore: PlayListStore
    fileStore: FileStore
    commentStore: CommentStore
}

export const store: Store = {
    trackStore: new TrackStore(),
    commonStore: new CommonStore(),
    userStore: new UserStore(),
    modalStore: new ModalStore(),
    playListStore: new PlayListStore(),
    fileStore: new FileStore(),
    commentStore: new CommentStore()
}

export const StoreContext = createContext(store)

export function useStore() {
    return useContext(StoreContext)
}