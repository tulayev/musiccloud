import './index.css'
import Player from './Player'
import Sidebar from './Sidebar'
import Index from '../pages/Index'
import TrackDetails from '../pages/tracks/TrackDetails'
import TrackForm from '../pages/tracks/TrackForm'
import { Routes, Route, useLocation } from 'react-router-dom'
import { observer } from 'mobx-react-lite'
import { ToastContainer } from 'react-toastify'
import NotFound from '../pages/errors/NotFound'
import ServerError from '../pages/errors/ServerError'

const App = () => {
	const location = useLocation()

	return (
		<>
			<ToastContainer position="bottom-right" hideProgressBar />
			<div id="mainContainer">
				<div id="topContainer">
					<Sidebar />
					<div id="mainViewContainer">
						<div id="mainContent">
							<Routes>
								<Route path="/" element={<Index />} />
								<Route path="/tracks/:id" element={<TrackDetails />} />
								<Route path="/tracks/edit/:id" element={<TrackForm key={location.key} />} />
								<Route path="/upload" element={<TrackForm key={location.key} />} />
								<Route path="/not-found" element={<NotFound />} />
								<Route path="/server-error" element={<ServerError />} />
							</Routes>
						</div>
					</div>
				</div>
				<Player />
			</div>
		</>
	)
}

export default observer(App)