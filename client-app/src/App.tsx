import axios from 'axios'
import { useEffect, useState } from 'react'

export default function App() {
	const [tracks, setTracks] = useState([])

	useEffect(() => {
		const load = async () => {
			const { data } = await axios.get('http://localhost:5000/api/tracks')
			setTracks(data)
		}

		load()
	}, [])

	return (
		<div>
			<header>
				{tracks.map((track: any) => (
					<div key={track.id}>
						<h2>{track.title}</h2>
						<p>{track.author}</p>
					</div>
				))}
			</header>
		</div>
	)
}