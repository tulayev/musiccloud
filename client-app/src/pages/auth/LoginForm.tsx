import { ErrorMessage, Form, Formik } from 'formik'
import { observer } from 'mobx-react-lite'
import { Button, Header, Label } from 'semantic-ui-react'
import MyTextInput from '../../components/form/MyTextInput'
import { useStore } from '../../store/store'

export default observer(function LoginForm() {
    const {userStore} = useStore()

    return (
        <Formik
            initialValues={{email: '', password: '', error: null}}
            onSubmit={(values, {setErrors}) => userStore.login(values).catch(_ => setErrors({error: 'Неправильный пароль или email'}))}
        >
            {({handleSubmit, isSubmitting, errors}) => (
                <Form className="ui form" onSubmit={handleSubmit} autoComplete="off">
                    <Header as="h2" content="Войти" color="teal" textAlign="center" />
                    <MyTextInput name="email" placeholder="Email" type="email" />
                    <MyTextInput name="password" placeholder="Пароль" type="password" />
                    <ErrorMessage 
                        name="error"
                        render={() => <Label style={{marginBottom: '10px'}} basic color="red" content={errors.error} />}  
                    />
                    <Button loading={isSubmitting} positive content="Войти" type="submit" fluid />
                </Form>
            )}
        </Formik>
    )
})