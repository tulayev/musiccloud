import { observer } from 'mobx-react-lite'
import { ChangeEvent, useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import { Button, Form, Segment } from 'semantic-ui-react'
import Spinner from '../../layout/Spinner'
import { useStore } from '../../store/store'
import { v4 as uuidv4 } from 'uuid'

const TrackForm = () => {
    const {trackStore} = useStore()
    const {createTrack, loadTrackSingle, updateTrack, loadingInitial, loading} = trackStore
    const {id} = useParams<{id: string}>()
    const navigate = useNavigate()
    const [track, setTrack] = useState({
        id: '',
        title: '',
        author: '',
        genre: ''
    })

    useEffect(() => {
        if (id) {
            loadTrackSingle(id)
                .then(track => setTrack(track!))
        }
    }, [id, loadTrackSingle])


    function handleSubmit() {
        if (track.id.length === 0) {
            const newTrack = {
                ...track,
                id: uuidv4()
            }
            createTrack(newTrack)
                .then(() => navigate(`/tracks/${newTrack.id}`))
        } else {
            updateTrack(track)
                .then(() => navigate(`/tracks/${track.id}`))
        }
    }

    function handleInputChange(e: ChangeEvent<HTMLInputElement>) {
        const {name, value} = e.target
        setTrack({...track, [name]: value})
    }

    if (loadingInitial)
        return <Spinner />

    return (
        <Segment clearing>
            <Form onSubmit={handleSubmit} autoComplete="off">
                <Form.Input placeholder="Title" value={track.title} name="title" onChange={handleInputChange} />
                <Form.Input placeholder="Author" value={track.author} name="author" onChange={handleInputChange} />
                <Form.Input placeholder="Genre" value={track.genre} name="genre" onChange={handleInputChange} />
                <Button loading={loading} floated="right" positive type="submit" content="Submit" />
            </Form>
        </Segment>
    )
}

export default observer(TrackForm)