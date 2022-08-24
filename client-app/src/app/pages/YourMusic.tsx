import { Button } from 'semantic-ui-react'
import { useStore } from '../store/store'
import PlayListForm from './playlists/PlayListForm'
import PlayListIndex from './playlists/PlayListIndex'

export default function YourMusic() {
    const {modalStore} = useStore()

    return (
        <>
            <Button 
                floated="left" 
                content="Create PlayList"
                onClick={() => modalStore.openModal(<PlayListForm />)} 
            />

            <PlayListIndex />
        </>
    )
}