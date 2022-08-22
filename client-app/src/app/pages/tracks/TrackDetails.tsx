import { useEffect } from 'react'
import { Link, useParams } from 'react-router-dom'
import { Button } from 'semantic-ui-react'
import Spinner from '../../layout/Spinner'
import { observer } from 'mobx-react-lite'
import { useStore } from '../../store/store'

const TrackDetails = () => {
    const {id} = useParams<{id: string}>()
    const {trackStore} = useStore()
    const {loadTrackSingle, track, loadingInitial} = trackStore

    useEffect(() => {
        if (id) {
            loadTrackSingle(id)
        }
    }, [id, loadTrackSingle])

    if (!track || loadingInitial)
        return <Spinner />

    return (
        <>
            <h2 className="page-heading-big">{ track.title }</h2>
            <Button as={Link} to={`/tracks/edit/${track.id}`} floated="right" content="Edit" />    
        </>
    )
}

export default observer(TrackDetails)