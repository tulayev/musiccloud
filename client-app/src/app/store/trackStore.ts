import { makeAutoObservable, runInAction } from 'mobx'
import agent from '../api/agent'
import Track from '../modules/track'

export default class TrackStore {
    tracks: Track[] = []
    track: Track | undefined = undefined
    loading = false
    loadingInitial = false

    constructor() {
        makeAutoObservable(this)
    }

    loadTracks = async () => {
        this.setLoadingInitial(true)
        try {
            const tracksFromDb = await agent.Tracks.list()
            runInAction(() => {
                this.tracks = tracksFromDb
            })
            this.setLoadingInitial(false)
        } catch (err) {
            console.log(err)
            this.setLoadingInitial(false)
        }
    }

    loadTrackSingle = async (id: string) => {
        this.setLoadingInitial(true)
        try {
            const trackFromDb = await agent.Tracks.details(id)
            runInAction(() => {
                this.track = trackFromDb
            })
            this.setLoadingInitial(false)
            return this.track
        } catch (err) {
            console.log(err)
           this.setLoadingInitial(false)
        }
    }

    createTrack = async (track: Track) => {
        this.setLoading(true)
        try {
            await agent.Tracks.create(track)
            this.setLoading(false)
        } catch (err) {
            console.log(err)
            this.setLoading(false)
        }
    }
    
    updateTrack = async (track: Track) => {
        this.setLoading(true)
        try {
            await agent.Tracks.update(track)
            this.setLoading(false)
        } catch (err) {
            console.log(err)
            this.setLoading(false)
        }
    }

    deleteTrack = async (id: string) => {
        this.setLoading(true)
        try {
            await agent.Tracks.delete(id)
            this.setLoading(false)
        } catch (err) {
            console.log(err)
            this.setLoading(false)
        }
    }

    setLoading = (state: boolean) => {
        this.loading = state
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state
    }
}