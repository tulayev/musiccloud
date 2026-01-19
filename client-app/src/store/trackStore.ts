import { makeAutoObservable, runInAction } from 'mobx';
import api from '../api';
import { Track } from '../models';

export default class TrackStore {
  tracks: Track[] = [];
  track: Track | undefined = undefined;
  loading = false;
  loadingInitial = false;

  constructor() {
    makeAutoObservable(this);
  }

  loadTracks = async () => {
    this.setLoadingInitial(true);
    
    try {
      const tracksFromDb = await api.Tracks.list();

      runInAction(() => {
          this.tracks = tracksFromDb;
      });
      
      this.setLoadingInitial(false);
    } catch (err) {
      console.log(err);
      this.setLoadingInitial(false);
    }
  }

  loadTrackSingle = async (id: string) => {
    this.setLoadingInitial(true);
    
    try {
      const trackFromDb = await api.Tracks.details(id);
      
      runInAction(() => {
        this.track = trackFromDb;
      });
      
      this.setLoadingInitial(false)
      return this.track;
    } catch (err) {
      console.log(err);
      this.setLoadingInitial(false);
    }
  }

  createTrack = async (track: Track) => {
    this.setLoading(true);
    
    try {
      await api.Tracks.create(track);
      this.setLoading(false);
    } catch (err) {
      console.log(err);
      this.setLoading(false);
    }
  }
  
  updateTrack = async (track: Track) => {
    this.setLoading(true);
    
    try {
      await api.Tracks.update(track);
      this.setLoading(false);
    } catch (err) {
      console.log(err);
      this.setLoading(false);
    }
  }

  deleteTrack = async (id: string) => {
    this.setLoading(true);

    try {
      await api.Tracks.delete(id);
      this.setLoading(false);
    } catch (err) {
      console.log(err);
      this.setLoading(false);
    }
  }

  setLoading = (state: boolean) => {
    this.loading = state;
  }

  setLoadingInitial = (state: boolean) => {
    this.loadingInitial = state;
  }
}
