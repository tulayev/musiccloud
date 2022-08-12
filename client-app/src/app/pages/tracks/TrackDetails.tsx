import axios from 'axios'
import { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import { Button } from 'semantic-ui-react'
import agent from '../../api/agent'
import { v4 as uuidv4 } from 'uuid'
import Track from '../../modules/track'
import TrackForm from './TrackForm'
import Spinner from '../../layout/Spinner'

export default function TrackDetails() {
    const { id } = useParams()
    const [track, setTrack] = useState<Track>()
    const [showForm, setShowForm] = useState(false)
    const [tracks, setTracks] = useState<Track[]>([])
    const [submitting, setSubmitting] = useState(false)
    const [loading, setLoading] = useState(true)
    const navigate = useNavigate()

    useEffect(() => {
        if (id) {
            agent.Tracks
            .details(id)
            .then((res) => {
                setTrack(res)
                setLoading(false)
            })
        }
    }, [])

    function handleCloseForm() {
        setShowForm(false)
    }

    function handleCreateOrEditTrack(track: Track) {
		setSubmitting(true)

		if (track.id) {
			agent.Tracks
				.update(track)
				.then(() => {
					setTracks([...tracks.filter(t => t.id !== track.id), track]) 
					setSubmitting(false)
				})
		} else {
			track.id = uuidv4()
			agent.Tracks
				.create(track)
				.then(() => {
					setTracks([...tracks, track])
					setSubmitting(false)
				})
		}

		navigate('/')
	}

    function handleDelete(id: string) {
        setSubmitting(true)
        agent.Tracks
            .delete(id)
            .then(() => {
                setTracks([...tracks.filter(t => t.id !== id)])
                setSubmitting(false)
            })

        navigate('/')
    }

    if (loading)
        return <Spinner />

    return (
        <>
            {
                track &&
                <>
                    <h1 className="page-heading-big">{ track.title }</h1>
                    <Button onClick={() => setShowForm(true)} floated="right" content="Edit" />
                    {
                        showForm ?
                        <TrackForm
                            track={track} 
                            closeForm={handleCloseForm} 
                            createOrEdit={handleCreateOrEditTrack}
                            submitting={submitting}
                        />
                        : null
                    }
                    <Button onClick={() => handleDelete(track.id)} floated="right" content="Delete" color="red" />
                </>
            }   
        </>
    )
}