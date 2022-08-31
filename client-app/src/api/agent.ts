import axios, { AxiosError, AxiosResponse } from 'axios'
import { toast } from 'react-toastify'
import AppFile from '../models/file'
import PlayList from '../models/playlist'
import Track from '../models/track'
import { User, UserFormValues } from '../models/user'
import { store } from '../store/store'

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)
    })
}

axios.defaults.baseURL = 'http://localhost:5000/api'

axios.interceptors.request.use(config => {
    const token = store.commonStore.token
    if (token && config.headers) {
        config.headers.Authorization = `Bearer ${token}`
    }
    return config
})

axios.interceptors.response.use(async res => {
    await sleep(1000)
    return res
}, (err: AxiosError) => {
    const {data: d, status, config} = err.response!
    const data: any = d!
    switch (status) {
        case 400:
            if (typeof data === 'string') {
                toast.error(data)
            }
            if (config.method === 'get' && data.errors.hasOwnProperty('id')) {
                // push to 'not-found' route
            }
            if (data.errors) {
                const modalStateErrors = []
                for (const key in data.errors) {
                    if (data.errors[key]) {
                        modalStateErrors.push(data.errors[key])
                    }
                }
                throw modalStateErrors.flat()
            }
            break
        case 401:
            toast.error('unauthorized')
            break
        case 404:
            toast.error('not found')
            // push to 'not-found' route
            break
        case 500:
            store.commonStore.setServerError(data)
            // push to 'server-error' route
            break
    }
})

const responseBody = <T> (response: AxiosResponse<T>) => response.data

const requests = {
    get: <T> (url: string) => axios.get<T>(url).then(responseBody),
    post: <T> (url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T> (url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    del: <T> (url: string) => axios.delete<T>(url).then(responseBody),
}

const Tracks = {
    list: () => requests.get<Track[]>('/tracks'),
    details: (id: string) => requests.get<Track>(`/tracks/${id}`),
    create: (track: Track) => requests.post<void>('/tracks', track),
    update: (track: Track) => requests.put<void>(`/tracks/${track.id}`, track),
    delete: (id: string) => requests.del<void>(`/tracks/${id}`)
}

const PlayLists = {
    list: () => requests.get<PlayList[]>('/playlists'),
    details: (id: string) => requests.get<PlayList>(`/playlists/${id}`),
    create: (playList: PlayList) => requests.post<void>('/playlists', playList),
    update: (playList: PlayList) => requests.put<void>(`/playlists/${playList.id}`, playList),
    delete: (id: string) => requests.del<void>(`/playlists/${id}`)
}

const Account = {
    current: () => requests.get<User>('/account'),
    login: (user: UserFormValues) => requests.post<User>('/account/login', user),
    register: (user: UserFormValues) => requests.post<User>('/account/register', user)
}

const Files = {
    upload: (file: Blob) => {
        const formData = new FormData()
        formData.append('file', file);
        return axios.post<AppFile>('files', formData, {
            headers: {'Content-type': 'multipart/form-data'}
        })
    }
}

const agent = {
    Tracks,
    PlayLists,
    Account,
    Files
}

export default agent