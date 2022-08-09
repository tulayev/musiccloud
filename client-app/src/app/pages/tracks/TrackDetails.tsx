import axios from 'axios'
import { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import { Button } from 'semantic-ui-react'
import Track from '../../modules/track'
import TrackForm from './TrackForm'

export default function TrackDetails() {
    const { id } = useParams()
    const [track, setTrack] = useState<Track>()
    const [showForm, setShowForm] = useState(false)
    const [tracks, setTracks] = useState<Track[]>([])
    const navigate = useNavigate()

    useEffect(() => {
        const load = async () => {
			const { data } = await axios.get<Track>(`http://localhost:5000/api/tracks/${id}`)
			setTrack(data)
		}
		load()
    }, [])

    function handleCloseForm() {
        setShowForm(false)
    }

    function handleCreateOrEditTrack(track: Track) {
		track.id 
		? setTracks([...tracks.filter(t => t.id !== track.id), track]) 
		: setTracks([...tracks, track])

		navigate('/')
	}

    return (
        <>
            <h1 className="page-heading-big">{ track?.title }</h1>
            <Button onClick={() => setShowForm(true)} floated="right" content="Edit" />
            {
                showForm ?
                <TrackForm
                    track={track} 
                    closeForm={handleCloseForm} 
                    createOrEdit={handleCreateOrEditTrack}
                />
                : null
            }
        </>
    )
}