import { makeAutoObservable, reaction } from 'mobx'
import ServerError from '../models/serverError'

export default class CommonStore {
    error: ServerError | null = null
    token: string | null = window.localStorage.getItem('token')
    appLoaded = false

    constructor() {
        makeAutoObservable(this)

        reaction(
            () => this.token,
            token => {
                if (token) {
                    window.localStorage.setItem('token', token)
                } else {
                    window.localStorage.removeItem('token')
                }
            }
        )
    }

    setServerError = (error: ServerError) => {
        this.error = error
    }

    setToken = (token: string | null) => {
        this.token = token
    }

    setAppLoaded = () => {
        this.appLoaded = true
    }
}