import axios from 'axios'
import { useEffect, useState } from 'react'
import NowPlaying from './components/partials/Player'
import Sidebar from './components/partials/Sidebar'
import './assets/css/index.css'

export default function App() {
	// const [tracks, setTracks] = useState([])

	// useEffect(() => {
	// 	const load = async () => {
	// 		const { data } = await axios.get('http://localhost:5000/api/tracks')
	// 		setTracks(data)
	// 	}

	// 	load()
	// }, [])

	return (
		<div id="mainContainer">
            <div id="topContainer">
                
                <Sidebar />
                
                <div id="mainViewContainer">
                    <div id="mainContent">
                        
                    </div>
                </div>
            </div>
            
            <NowPlaying />
        </div>
	)
}