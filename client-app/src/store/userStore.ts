import { makeAutoObservable, runInAction } from 'mobx';
import api from '../api';
import { User, UserFormValues } from '../models'; 
import { store } from './store';

export default class UserStore {
  user: User | null = null;
    
  constructor() {
    makeAutoObservable(this);
  }

  get isLoggedIn() {
    return !!this.user;
  }

  login = async (credentials: UserFormValues) => {
    try {
      const user = await api.Account.login(credentials);
      store.commonStore.setToken(user.token);
      runInAction(() => this.user = user);
      store.modalStore.closeModal();
    } catch (error) {
      throw error;
    }
  }

  logout = () => {
    store.commonStore.setToken(null);
    window.localStorage.removeItem('token');
    this.user = null;
  }

  getUser = async () => {
    try {
      const user = await api.Account.current();
      runInAction(() => this.user = user);
    } catch (error) {
      throw error;
    }
  }

  register = async (credentials: UserFormValues) => {
    try {
      const user = await api.Account.register(credentials);
      store.commonStore.setToken(user.token);
      runInAction(() => this.user = user);
      store.modalStore.closeModal();
    } catch (error) {
      throw error;
    }
  }
}
