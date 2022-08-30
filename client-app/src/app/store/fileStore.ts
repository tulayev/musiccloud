import { makeAutoObservable, runInAction } from 'mobx'
import agent from '../api/agent'
import AppFile from '../models/file'

export default class FileStore {
    audioFile: AppFile | undefined = undefined
    imageFile: AppFile | undefined = undefined
    uploading: boolean = false

    constructor() {
        makeAutoObservable(this)
    }

    upload = async (file: Blob, isAudio = false) => {
        runInAction(() => this.uploading = true)
        try {
            const { data } = await agent.Files.upload(file)
            runInAction(() => {
                isAudio ? this.audioFile = data : this.imageFile = data
                this.uploading = false
            })
        } catch (err) {
            runInAction(() => this.uploading = false)
        }
    }
}