import './index.css'
import Sidebar from './Sidebar'
import Index from '../pages/Index'
import TrackDetails from '../pages/tracks/TrackDetails'
import TrackForm from '../pages/tracks/TrackForm'
import { Routes, Route, useLocation, Navigate } from 'react-router-dom'
import { observer } from 'mobx-react-lite'
import { ToastContainer } from 'react-toastify'
import 'react-toastify/dist/ReactToastify.min.css'
import NotFound from '../pages/errors/NotFound'
import ServerError from '../pages/errors/ServerError'
import LoginForm from '../pages/auth/LoginForm'
import { useStore } from '../store/store'
import { useEffect } from 'react'
import Spinner from './Spinner'
import ModalContainer from '../components/modals/ModalContainer'
import YourMusic from '../pages/YourMusic'

const App = () => {
	const location = useLocation()
	const {commonStore, userStore} = useStore()

	useEffect(() => {
		if (commonStore.token) {
			userStore.getUser().finally(() => commonStore.setAppLoaded())
		} else {
			commonStore.setAppLoaded()
		}
	}, [commonStore, userStore])

	if (!commonStore.appLoaded)
		return <Spinner />

	return (
		<>
			<ToastContainer position="bottom-right" />
			<ModalContainer />
			<div className="main_container">
				<div className="top_container">
					<Sidebar />
					<div className="main_wrapper">
						<div className="main_content">
							<Routes>
								<Route path="/" element={<Index />} />
								<Route path="/tracks/:id" element={<TrackDetails />} />
								<Route path="/tracks/edit/:id" element={<TrackForm key={location.key} />} />
								<Route path="/upload" element={<TrackForm key={location.key} />} />
								<Route path="/your-music" element={<YourMusic />} />
								<Route path="/login" element={<LoginForm />} />
								<Route path="/not-found" element={<NotFound />} />
								<Route path="/server-error" element={<ServerError />} />
								<Route
									path="*"
									element={<Navigate to="/not-found" replace />}
								/>
							</Routes>
						</div>
					</div>
				</div>
			</div>
		</>
	)
}

export default observer(App)