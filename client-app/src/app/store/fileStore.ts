import { makeAutoObservable, runInAction } from 'mobx'
import agent from '../api/agent'
import AppFile from '../models/file'

export default class FileStore {
    file: AppFile | null = null
    uploading: boolean = false

    constructor() {
        makeAutoObservable(this)
    }

    upload = async (file: Blob) => {
        runInAction(() => this.uploading = true)
        try {
            const { data } = await agent.Files.upload(file)
            runInAction(() => {
                this.file = data
                this.uploading = false
            })
        } catch (err) {
            runInAction(() => this.uploading = false)
        }
    }
}