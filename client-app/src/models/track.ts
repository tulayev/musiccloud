import { AppFile } from './file'

interface Uploader {
    username: string
    displayName: string
    bio?: string
    image?: string
}

export interface Track {
    id: string
    title: string
    author: string
    genre: string
    uploader?: Uploader
    poster?: AppFile
    audio?: AppFile
    posterId?: number
    audioId?: number
}
