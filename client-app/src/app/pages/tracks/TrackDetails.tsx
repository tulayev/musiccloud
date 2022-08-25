import { useEffect } from 'react'
import { Link, useParams } from 'react-router-dom'
import { Button } from 'semantic-ui-react'
import Spinner from '../../layout/Spinner'
import { observer } from 'mobx-react-lite'
import { useStore } from '../../store/store'

const TrackDetails = () => {
    const {id} = useParams<{id: string}>()
    const {trackStore} = useStore()
    const {userStore} = useStore()
    const {loadTrackSingle, track, loadingInitial} = trackStore

    useEffect(() => {
        if (id) {
            loadTrackSingle(id)
        }
    }, [id, loadTrackSingle])

    if (!track || loadingInitial)
        return <Spinner />

    return (
        <div className="track_details_container">
            <div className="image_cover">
                <img src="/assets/images/zaglushka.jpg" alt="Zaglushka" />
            </div>
            <div>
                <h2>Название: {track.title}</h2>
                <p>Автор: {track.author}</p>
                <p>Жанр: {track.genre}</p>
            </div>
            <div>
                {
                    userStore.user?.username === track.uploader?.username &&
                    <>
                        <Button 
                            as={Link} 
                            to={`/tracks/edit/${track.id}`} 
                            color="green"
                        >
                            Изменить
                        </Button>  
                        <Button 
                            onClick={() => console.log('delete')}
                            color="red"
                        >
                            Удалить
                        </Button>  
                    </>
                }
            </div>  
        </div>
    )
}

export default observer(TrackDetails)