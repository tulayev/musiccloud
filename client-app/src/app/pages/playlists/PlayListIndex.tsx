import { observer } from 'mobx-react-lite'
import { useEffect } from 'react'
import { Button } from 'semantic-ui-react'
import { useStore } from '../../store/store'
import PlayListForm from './PlayListForm'

const PlayListIndex = () => {
    const {playListStore} = useStore()
    const {modalStore} = useStore()
    const {playLists} = playListStore

    useEffect(() => {
        playListStore.loadPlayLists()
    }, [playListStore])

    return (
        <>
            <h1 className="page-heading-big">Ваши плейлисты</h1>

            <div className="grid-view-container"> 
                {
                    playLists.map(playList => (
                        <div className="grid-view-item" key={playList.id}>
                            <div className="grid-view-info">
                                <p>{playList.name}</p>
                                <Button
                                    onClick={() => modalStore.openModal(<PlayListForm id={playList.id} />)} 
                                >
                                    Edit
                                </Button>
                            </div>
                        </div>
                    ))
                }  
            </div>
        </>
    )
}

export default observer(PlayListIndex)