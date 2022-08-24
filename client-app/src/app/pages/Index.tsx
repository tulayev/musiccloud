import { observer } from 'mobx-react-lite'
import { useEffect } from 'react'
import { Link } from 'react-router-dom'
import Spinner from '../layout/Spinner'
import { useStore } from '../store/store'

const Index = () => {
    const {trackStore} = useStore()
    const {tracks} = trackStore

	useEffect(() => {
		trackStore.loadTracks()
	}, [trackStore])

	if (trackStore.loadingInitial) 
		return <Spinner />

    return (
        <>
            <h1 className="page-heading-big">Все треки</h1>

            <div className="grid-view-container"> 
                {
                    tracks.map(track => (
                        <div className="grid-view-item" key={track.id}>
                            <Link to={`tracks/${track.id}`}>
                                {/* <img src="" alt={track.title} />  */}
                                <div style={{ width: '100%', height: '200px', backgroundColor: 'white', color: 'black' }}>No image</div>
                                <div className="grid-view-info">
                                    <h3>{track.title}</h3>
                                    <p>{track.author}</p>
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