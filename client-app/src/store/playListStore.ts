import { makeAutoObservable, runInAction } from 'mobx'
import agent from '../api/agent'
import PlayList from '../models/playlist'

export default class PlayListStore {
    playLists: PlayList[] = []
    playList: PlayList | undefined = undefined
    loading = false
    loadingInitial = false

    constructor() {
        makeAutoObservable(this)
    }

    loadPlayLists = async () => {
        runInAction(() => this.loadingInitial = true)
        
        try {
            const playListsFromDb = await agent.PlayLists.list()
            runInAction(() => {
                this.playLists = playListsFromDb
                this.loadingInitial = false
            })
        } catch (err) {
            console.log(err)
            runInAction(() => this.loadingInitial = false)
        }
    }

    loadPlayListSingle = async (id: string) => {
        runInAction(() => this.loadingInitial = true)
        
        try {
            const playListFromDb = await agent.PlayLists.details(id)
            runInAction(() => {
                this.playList = playListFromDb
                this.loadingInitial = false
            })
            return this.playList
        } catch (err) {
            console.log(err)
            runInAction(() => this.loadingInitial = false)
        }
    }

    createPlayList = async (playList: PlayList) => {
        runInAction(() => this.loading = true)
        try {
            await agent.PlayLists.create(playList)
            runInAction(() => this.loading = false)
        } catch (err) {
            console.log(err)
            runInAction(() => this.loading = false)
        }
    }
    
    updatePlayList = async (playList: PlayList) => {
        runInAction(() => this.loading = true)
        try {
            await agent.PlayLists.update(playList)
            runInAction(() => this.loading = false)
        } catch (err) {
            console.log(err)
            runInAction(() => this.loading = false)
        }
    }

    deletePlayList = async (id: string) => {
        runInAction(() => this.loading = true)
        try {
            await agent.PlayLists.delete(id)
            runInAction(() => this.loading = false)
        } catch (err) {
            console.log(err)
            runInAction(() => this.loading = false)
        }
    }
}