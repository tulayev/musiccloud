import { makeAutoObservable, runInAction } from 'mobx'
import { toast } from 'react-toastify'
import api from '../api'
import AppFile from '../models/file'

export default class FileStore {
    audioFile: AppFile | undefined = undefined
    imageFile: AppFile | undefined = undefined
    uploadingAudio: boolean = false
    uploadingImage: boolean = false

    constructor() {
        makeAutoObservable(this)
    }

    upload = async (file: Blob, isAudio = false) => {
        runInAction(() => isAudio ? this.uploadingAudio = true : this.uploadingImage = true)
        try {
            const { data } = await api.Files.upload(file)
            runInAction(() => {
                if (isAudio) {
                    this.audioFile = data
                    toast.success("Трек загружен")
                    this.uploadingAudio = false
                } else {
                    this.imageFile = data
                    toast.success("Постер загружен")
                    this.uploadingImage = false
                }
            })
        } catch (err) {
            runInAction(() => isAudio ? this.uploadingAudio = false : this.uploadingImage = false)
        }
    }
}