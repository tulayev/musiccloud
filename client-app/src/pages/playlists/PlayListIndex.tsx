import { observer } from 'mobx-react-lite'
import { useEffect } from 'react'
import { Button } from 'semantic-ui-react'
import { useStore } from '../../store/store'
import PlayListForm from './PlayListForm'

const PlayListIndex = () => {
    const {playListStore} = useStore()
    const {modalStore} = useStore()
    const {playLists, loadPlayLists} = playListStore

    useEffect(() => {
        loadPlayLists()
    }, [loadPlayLists])

    return (
        <>
            <h1 className="page-heading-big">Ваши плейлисты</h1>

            <div className="grid-view-container"> 
                {
                    playLists.map(playList => (
                        <div className="grid-view-item" key={playList.id}>
                            <div className="image_cover">
                                <img src="/assets/images/playlist.jpg" alt="PlayList_cover" /> 
                            </div>
                            <div className="grid-view-info">
                                <h3>{playList.name}</h3>
                                <Button
                                    color="green"
                                    onClick={() => modalStore.openModal(<PlayListForm id={playList.id} />)} 
                                >
                                    Изменить
                                </Button>
                                <Button
                                    color="red"
                                    onClick={() => console.log('delete') } 
                                >
                                    Удалить
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