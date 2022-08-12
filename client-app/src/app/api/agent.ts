import axios, { AxiosResponse } from 'axios'
import Track from '../modules/track'

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)
    })
}

axios.defaults.baseURL = 'http://localhost:5000/api'

axios.interceptors.response.use(async res => {
    try {
        await sleep(1000)
        return res
    } catch (err) {
        console.log(err)
        return await Promise.reject(err)
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

const agent = {
    Tracks
}

export default agent