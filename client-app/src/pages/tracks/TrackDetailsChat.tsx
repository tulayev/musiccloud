import { Form, Formik } from 'formik'
import { observer } from 'mobx-react-lite'
import { useEffect } from 'react'
import { Button, Comment, Header, Segment } from 'semantic-ui-react'
import MyTextArea from '../../components/form/MyTextArea'
import { useStore } from '../../store/store'

interface Props {
    trackId: string
}

export default observer(function TrackDetailsChat({trackId}: Props) {
    const {commentStore} = useStore()

    useEffect(() => {
        if (trackId) {
            commentStore.createHubConnection(trackId)
        }

        return () => {
            commentStore.clearComments()
        }
    }, [commentStore, trackId])

    return (
        <>
            <Segment
                textAlign="center"
                attached="top"
                inverted
                color="teal"
                style={{ border: 'none' }} 
            >
                <Header>Чат-комментарии</Header>
            </Segment>
            <Segment 
                attached
                clearing
                style={{ backgroundColor: '#181818' }}
            >
                <Comment.Group>
                    {commentStore.comments.map(comment => (
                        <Comment key={comment.id}>
                            <Comment.Avatar src={ comment.image || '/assets/images/avatar.png' } />
                            <Comment.Content>
                                <Comment.Author 
                                    style={{ color: '#ffffff' }} 
                                    as="a"
                                >
                                    { comment.displayName }
                                </Comment.Author>

                                <Comment.Metadata style={{ color: '#ffffff' }}>
                                    <div>
                                        { comment.createdAtUtc.toString() }
                                    </div>
                                </Comment.Metadata>
                                
                                <Comment.Text 
                                    style={{ color: '#ffffff' }}
                                >
                                    { comment.body }
                                </Comment.Text>
                            </Comment.Content>
                        </Comment>
                    ))}

                    <Formik
                        onSubmit={(values, {resetForm}) => 
                            commentStore.addComment(values)
                                .then(() => resetForm())}
                        initialValues={{ body: '' }}
                    >
                        {({isSubmitting, isValid}) => (
                            <Form 
                                className="ui form"
                            >
                                <MyTextArea placeholder="Оставить комментарий" name="body" rows={2} />
                                <Button
                                    loading={isSubmitting}
                                    disabled={isSubmitting || !isValid}
                                    content="Отправить"
                                    labelPosition="left"
                                    icon="edit"
                                    primary
                                    type="submit"
                                    floated="right"
                                />
                            </Form>
                        )}
                    </Formik>
                </Comment.Group>
            </Segment>
        </>
    )
})