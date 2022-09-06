import { observer } from 'mobx-react-lite'
import { useEffect } from 'react'
import { Link } from 'react-router-dom'
import { Button } from 'semantic-ui-react'
import Spinner from '../layout/Spinner'
import { useStore } from '../store/store'

const Index = () => {
    const {trackStore} = useStore()
    const {loadTracks, tracks, loadingInitial} = trackStore

	useEffect(() => {
        loadTracks()
	}, [loadTracks])

	if (loadingInitial) 
		return <Spinner />

    return (
        <>
            <h1 className="page-heading-big">Все треки</h1>

            <div className="grid-view-container"> 
                {
                    tracks.map(track => (
                        <div className="grid-view-item" key={track.id}>
                            <Link to={`tracks/${track.id}`}>
                                <div className="image_cover">
                                <img 
                                    src={track.poster ? track.poster.url : '/assets/images/zaglushka.jpg' } 
                                    alt={track.title} 
                                /> 
                                </div>
                                <div className="grid-view-info">
                                    <h3>{track.title}</h3>
                                    <p>{track.author}</p>
                                    <Button
                                        color="blue"
                                    >
                                        Добавить в плейлист
                                    </Button>
                                </div>
                            </Link>
                        </div>
                    ))
                }  
            </div>
        </>
    )
}

export default observer(Index)