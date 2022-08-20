import { makeAutoObservable, runInAction } from 'mobx'
import agent from '../api/agent'
import { User, UserFormValues } from '../models/user' 
import { store } from './store'

export default class UserStore {
    user: User | null = null
    
    constructor() {
        makeAutoObservable(this)
    }

    get isLoggedIn() {
        return !!this.user
    }

    login = async (credentials: UserFormValues) => {
        try {
            const user = await agent.Account.login(credentials)
            store.commonStore.setToken(user.token)
            runInAction(() => this.user = user)
            store.modalStore.closeModal()
        } catch (error) {
            throw error
        }
    }

    logout = () => {
        store.commonStore.setToken(null)
        window.localStorage.removeItem('token')
        this.user = null
    }

    getUser = async () => {
        try {
            const user = await agent.Account.current()
            runInAction(() => this.user = user)
        } catch (error) {
            console.log(error)
        }
    }

    register = async (credentials: UserFormValues) => {
        try {
            const user = await agent.Account.register(credentials)
            store.commonStore.setToken(user.token)
            runInAction(() => this.user = user)
            store.modalStore.closeModal()
        } catch (error) {
            throw error
        }
    }
}