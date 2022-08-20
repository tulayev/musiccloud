import { ErrorMessage, Form, Formik } from 'formik'
import { observer } from 'mobx-react-lite'
import { Button, Header, Label } from 'semantic-ui-react'
import MyTextInput from '../../common/form/MyTextInput'
import { useStore } from '../../store/store'
import * as Yup from 'yup'
import ValidationErrors from '../errors/ValidationErrors'

export default observer(function RegisterForm() {
    const {userStore} = useStore()

    return (
        <Formik
            initialValues={{displayName: '', username: '', email: '', password: '', error: null}}
            onSubmit={(values, {setErrors}) => userStore.register(values).catch(error => setErrors({error}))}
            validationSchema={Yup.object({
                displayName: Yup.string().required(),
                username: Yup.string().required(),
                email: Yup.string().required().email(),
                password: Yup.string().required()
            })}
        >
            {({handleSubmit, isSubmitting, errors, isValid, dirty}) => (
                <Form className="ui form error" onSubmit={handleSubmit} autoComplete="off">
                    <Header as="h2" content="Регистрация" color="teal" textAlign="center" />
                    <MyTextInput name="displayName" placeholder="Имя на сайте" />
                    <MyTextInput name="username" placeholder="Юзернейм" />
                    <MyTextInput name="email" placeholder="Email" type="email" />
                    <MyTextInput name="password" placeholder="Пароль" type="password" />
                    <ErrorMessage 
                        name="error"
                        render={() => <ValidationErrors errors={errors.error} />}  
                    />
                    <Button 
                        disabled={!isValid || !dirty || isSubmitting} 
                        loading={isSubmitting} 
                        type="submit" 
                        content="Регистрация" 
                        positive 
                        fluid 
                    />
                </Form>
            )}
        </Formik>
    )
})