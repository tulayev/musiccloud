import axios from 'axios'
import { useEffect, useState } from 'react'
import Player from './Player'
import Sidebar from './Sidebar'
import Track from '../modules/track'
import './index.css'
import TrackIndex from '../pages/tracks/TrackIndex'
import { Routes, Route, useNavigate } from 'react-router-dom'
import TrackDetails from '../pages/tracks/TrackDetails'
import TrackForm from '../pages/tracks/TrackForm'

export default function App() {
	const [tracks, setTracks] = useState<Track[]>([])
	const navigate = useNavigate()

	useEffect(() => {
		const load = async () => {
			const { data } = await axios.get<Track[]>('http://localhost:5000/api/tracks')
			setTracks(data)
		}
		load()
	}, [])

	function handleCreateOrEditTrack(track: Track) {
		track.id 
		? setTracks([...tracks.filter(t => t.id !== track.id), track]) 
		: setTracks([...tracks, track])

		navigate('/')
	}

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
								element={ <TrackForm track={undefined} closeForm={() => console.log('cancelled')} createOrEdit={handleCreateOrEditTrack} /> } 
							/>
						</Routes>
					</div>
				</div>
			</div>
			<Player />
		</div>
	)
}