import { Formik } from 'formik'
import { observer } from 'mobx-react-lite'
import { useEffect, useState } from 'react'
import * as Yup from 'yup' 
import { v4 as uuidv4 } from 'uuid'
import Spinner from '../../layout/Spinner'
import PlayList from '../../models/playlist'
import { useStore } from '../../store/store'
import { Button, Form, Header } from 'semantic-ui-react'
import MyTextInput from '../../components/form/MyTextInput'

interface Props {
    id?: string
}

const PlayListForm = ({id}: Props) => {
    const {playListStore} = useStore()
    const {modalStore} = useStore()
    const {createPlayList, loadPlayListSingle, updatePlayList, loadingInitial, loading} = playListStore
    const [playList, setPlayList] = useState({
        id: '',
        name: ''
    })

    useEffect(() => {
        if (id) {
            loadPlayListSingle(id)
                .then(playList => setPlayList(playList!))
        }
    }, [id, loadPlayListSingle])

    const validationSchema = Yup.object({
        name: Yup.string().required('Поле название плейлиста должно быть заполнено')
    })

    function handleFormSubmit(playList: PlayList) {
        if (playList.id.length === 0) {
            const newPlayList = {
                ...playList,
                id: uuidv4()
            }
            createPlayList(newPlayList)
                .then(() => modalStore.closeModal())
        } else {
            updatePlayList(playList)
                .then(() => modalStore.closeModal())
        }
    }

    if (loadingInitial)
        return <Spinner />

    return (
        <Formik
            initialValues={playList}
            validationSchema={validationSchema}
            onSubmit={values => handleFormSubmit(values)}
            enableReinitialize
        >
            {({ handleSubmit, isValid, isSubmitting, dirty }) => (
                <Form className="ui form" onSubmit={handleSubmit} autoComplete="off">
                    <Header content="Плейлист" sub color="teal" />
                    <MyTextInput name="name" placeholder="Название"  />
                    <Button 
                        disabled={isSubmitting || !dirty || !isValid}
                        loading={loading} 
                        floated="right" 
                        type="submit" 
                        content="Submit" 
                        positive 
                    />
                </Form>
            )}
        </Formik> 
    )
}

export default observer(PlayListForm)