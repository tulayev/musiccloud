import { observer } from 'mobx-react-lite';
import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Button, Header, Segment } from 'semantic-ui-react';
import Spinner from '../../layout/Spinner';
import { useStore } from '../../store/store';
import { v4 as uuidv4 } from 'uuid';
import { Formik, Form } from 'formik';
import * as Yup from 'yup' ;
import MyTextInput from '../../components/form/MyTextInput';
import { Track } from '../../models';
import MyCustomInput from '../../components/form/MyCustomInput';

const TrackForm = () => {
  const {trackStore, fileStore} = useStore();
  const {createTrack, loadTrackSingle, updateTrack, loadingInitial, loading} = trackStore;
  const {audioFile, imageFile, upload, uploadingAudio, uploadingImage} = fileStore;
  const {id} = useParams<{id: string}>();
  const navigate = useNavigate();
  const [track, setTrack] = useState({
    id: '',
    title: '',
    author: '',
    genre: ''
  });

  useEffect(() => {
      if (id) {
        loadTrackSingle(id)
          .then(track => setTrack(track!));
      }
  }, [id, loadTrackSingle]);

  const validationSchema = Yup.object({
    title: Yup.string().required('Поле название трека должно быть заполнено'),
    author: Yup.string().required('Поле автор трека должно быть заполнено'),
    genre: Yup.string().required('Поле жанр трека должно быть заполнено')
  });

  function handleFormSubmit(track: Track) {
      if (track.id.length === 0) {
        // if (!audioFile) {
        //     toast.error('Пожалуйста загрузите аудиофайл')
        //     return
        // }
        const newTrack = {
          ...track,
          id: uuidv4(),
          posterId: imageFile?.id,
          audioId: audioFile?.id
        };

        createTrack(newTrack)
          .then(() => navigate(`/tracks/${newTrack.id}`));
      } else {
          updateTrack(track)
            .then(() => navigate(`/tracks/${track.id}`));
      }
  }

  function handleAudioUpload (e: any) {
    upload(e.target.files[0], true);
  }
  
  function handleImageUpload (e: any) {
    upload(e.target.files[0]);
  }

  if (loadingInitial)
    return <Spinner />

  return (
    <Segment 
      style={{marginTop: 20, backgroundColor: '#181818'}}
      clearing 
    >
      <Header content="Трек" sub color="teal" />
      <Formik 
        validationSchema={validationSchema} 
        enableReinitialize 
        initialValues={track} 
        onSubmit={values => handleFormSubmit(values)}
      >
        {({ handleSubmit, isSubmitting }) => (
          <Form className="ui form" onSubmit={handleSubmit} autoComplete="off">
            <MyTextInput name="title" placeholder="Title"  />
            <MyTextInput name="author" placeholder="Author" />
            <MyTextInput name="genre" placeholder="Genre" />
            {track.id.length === 0 &&
              <>
                <MyCustomInput 
                  name="file" 
                  type="file" 
                  label={ uploadingAudio ? 'Трек загружается, пожалуйста подождите' : 'Выберите аудиофайл' } 
                  handleChange={handleAudioUpload}
                />
                <MyCustomInput 
                  name="file" 
                  type="file" 
                  label={ uploadingImage ? 'Постер загружается, пожалуйста подождите' : 'Выберите постер' } 
                  handleChange={handleImageUpload} 
                />
              </>
            }
            <Button 
              disabled={isSubmitting || uploadingAudio || uploadingImage}
              loading={loading} 
              floated="right" 
              positive 
              type="submit" 
              content="Отправить" 
            />
          </Form>
        )}
      </Formik>
    </Segment>
  )
}

export default observer(TrackForm)
