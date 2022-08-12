import { ChangeEvent, useState } from 'react'
import { Button, Form, Segment } from 'semantic-ui-react'
import Track from '../../modules/track'

interface Props {
    track: Track | undefined
    submitting: boolean
    closeForm: () => void
    createOrEdit: (track: Track) => void
}

export default function TrackForm({track: selectedTrack, closeForm, createOrEdit, submitting}: Props) {
    const initialState = selectedTrack ?? {
        id: '',
        title: '',
        author: '',
        genre: ''
    }

    const [track, setTrack] = useState(initialState)

    function handleSubmit() {
        createOrEdit(track)
    }

    function handleInputChange(e: ChangeEvent<HTMLInputElement>) {
        const {name, value} = e.target
        setTrack({...track, [name]: value})
    }

    return (
        <Segment clearing>
            <Form onSubmit={handleSubmit} autoComplete="off">
                <Form.Input placeholder="Title" value={track.title} name="title" onChange={handleInputChange} />
                <Form.Input placeholder="Author" value={track.author} name="author" onChange={handleInputChange} />
                <Form.Input placeholder="Genre" value={track.genre} name="genre" onChange={handleInputChange} />
                <Button loading={submitting} floated="right" positive type="submit" content="Submit" />
                {selectedTrack &&
                    <Button onClick={() => closeForm()} floated="right" type="button" content="Cancel" />
                }
            </Form>
        </Segment>
    )
}