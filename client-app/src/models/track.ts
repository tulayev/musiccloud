import AppFile from './file'

interface Uploader {
    username: string
    displayName: string
    bio?: string
    image?: string
}

export default interface Track {
    id: string
    title: string
    author: string
    genre: string
    uploader?: Uploader
    poster?: AppFile
    audio?: AppFile
}