import { useEffect, useState } from 'react'
import Player from './Player'
import Sidebar from './Sidebar'
import Track from '../modules/track'
import './index.css'
import TrackIndex from '../pages/tracks/TrackIndex'
import { Routes, Route, useNavigate } from 'react-router-dom'
import TrackDetails from '../pages/tracks/TrackDetails'
import TrackForm from '../pages/tracks/TrackForm'
import { v4 as uuidv4 } from 'uuid'
import agent from '../api/agent'
import Spinner from './Spinner'

export default function App() {
	const [tracks, setTracks] = useState<Track[]>([])
	const navigate = useNavigate()
	const [loading, setLoading] = useState(true)
	const [submitting, setSubmitting] = useState(false)

	useEffect(() => {
		agent.Tracks
			.list()
			.then(res => {
				setTracks(res)
				setLoading(false)
			})
	}, [])

	function handleCreateOrEditTrack(track: Track) {
		setSubmitting(true)

		if (track.id) {
			agent.Tracks
				.update(track)
				.then(() => {
					setTracks([...tracks.filter(t => t.id !== track.id), track]) 
					setSubmitting(false)
				})
		} else {
			track.id = uuidv4()
			agent.Tracks
				.create(track)
				.then(() => {
					setTracks([...tracks, track])
					setSubmitting(false)
				})
		}

		navigate('/')
	}

	if (loading) 
		return <Spinner />

	return (
		<div id="mainContainer">
			<div id="topContainer">
				<Sidebar />
				<div id="mainViewContainer">
					<div id="mainContent">
						<Routes>
							<Route path="/" element={ <TrackIndex tracks={ tracks } /> } />
							<Route path="tracks/:id" element={ <TrackDetails /> } />
							<Route 
								path="upload" 
								element={ 
									<TrackForm 
										track={undefined} 
										submitting={submitting}
										closeForm={() => console.log('cancelled')} 
										createOrEdit={handleCreateOrEditTrack} 
									/> } 
							/>
						</Routes>
					</div>
				</div>
			</div>
			<Player />
		</div>
	)
}