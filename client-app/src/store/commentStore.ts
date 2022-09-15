import { makeAutoObservable, runInAction } from 'mobx';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr'
import ChatComment from '../models/comment'
import { store } from './store';

export default class CommentStore {
    comments: ChatComment[] = []
    hubConnection: HubConnection | null = null

    constructor() {
        makeAutoObservable(this)
    }

    createHubConnection = (trackId: string) => {
        if (store.trackStore.track) {
            this.hubConnection = new HubConnectionBuilder()
                .withUrl(`http://localhost:5000/chat?trackId=${trackId}`, {
                    accessTokenFactory: () => store.userStore.user?.token!
                })
                .withAutomaticReconnect()
                .configureLogging(LogLevel.Information)
                .build()

            this.hubConnection.start()
                .catch(error => console.log(error))

            this.hubConnection.on('LoadComments', (comments: ChatComment[]) => {
                runInAction(() => this.comments = comments)
            })

            this.hubConnection.on('ReceiveComment', (comment: ChatComment) => {
                runInAction(() => this.comments.push(comment))
            })
        }
    }

    stopHubConnection = () => {
        this.hubConnection?.stop()
            .catch(error => console.log(error))
    }

    clearComments = () => {
        this.comments = []
        this.stopHubConnection()
    }

    addComment = async (values: any) => {
        values.trackId = store.trackStore.track?.id
        try {
            await this.hubConnection?.invoke('SendComment', values)
        } catch (error) {
            console.log(error)
        }
    }
}