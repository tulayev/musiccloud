import { observer } from 'mobx-react-lite'
import { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import { Button, Header, Segment } from 'semantic-ui-react'
import Spinner from '../../layout/Spinner'
import { useStore } from '../../store/store'
import { v4 as uuidv4 } from 'uuid'
import { Formik, Form } from 'formik'
import * as Yup from 'yup' 
import MyTextInput from '../../common/form/MyTextInput'
import Track from '../../modules/track'

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

    const validationSchema = Yup.object({
        title: Yup.string().required('Поле название трека должно быть заполнено'),
        author: Yup.string().required('Поле автор трека должно быть заполнено'),
        genre: Yup.string().required('Поле жанр трека должно быть заполнено')
    })

    function handleFormSubmit(track: Track) {
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

    if (loadingInitial)
        return <Spinner />

    return (
        <Segment clearing style={{marginTop: '20px', backgroundColor: '#181818'}}>
            <Header content="Трек" sub color="teal" />
            <Formik 
                validationSchema={validationSchema} 
                enableReinitialize 
                initialValues={track} 
                onSubmit={values => handleFormSubmit(values)}
            >
                {({ handleSubmit, isValid, isSubmitting, dirty }) => (
                    <Form className="ui form" onSubmit={handleSubmit} autoComplete="off">
                        <MyTextInput name="title" placeholder="Title"  />
                        <MyTextInput name="author" placeholder="Author" />
                        <MyTextInput name="genre" placeholder="Genre" />
                        <Button 
                            disabled={isSubmitting || !dirty || !isValid}
                            loading={loading} 
                            floated="right" 
                            positive 
                            type="submit" 
                            content="Submit" 
                        />
                    </Form>
                )}
            </Formik>
        </Segment>
    )
}

export default observer(TrackForm)