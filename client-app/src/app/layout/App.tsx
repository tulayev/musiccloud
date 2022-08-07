import axios from 'axios'
import { useEffect, useState } from 'react'
import Player from './Player'
import Sidebar from './Sidebar'
import Track from '../modules/track'
import './index.css'
import TrackIndex from '../pages/tracks/TrackIndex'
import { Routes, Route } from 'react-router-dom'

export default function App() {
	const [tracks, setTracks] = useState<Track[]>([])

	useEffect(() => {
		const load = async () => {
			const { data } = await axios.get<Track[]>('http://localhost:5000/api/tracks')
			setTracks(data)
		}
		load()
	}, [])

	return (
		<div id="mainContainer">
			<div id="topContainer">
				<Sidebar />
				<div id="mainViewContainer">
					<div id="mainContent">
						<Routes>
							<Route path="/" element={ <TrackIndex tracks={ tracks } /> } />
						</Routes>
					</div>
				</div>
			</div>
			<Player />
		</div>
	)
}