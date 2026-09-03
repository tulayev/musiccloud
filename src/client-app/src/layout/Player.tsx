import { useEffect, useState } from 'react';

const tempTrackList = [
  {
    name: "Take What You Want",
    artist: "Post Malone, Ozzy Osbourne & Travi$ Scott",
    image: "https://i.ytimg.com/vi/d5Sm84rUsL4/maxresdefault.jpg",
    path: "/uploads/post_malone_take_what_you_want.mp3"
  },
  {
    name: "10 Feet Down",
    artist: "NF & Ruelle",
    image: "https://pesenki.net/uploads/mini/postery-pesen/da/f8188121ae23df48de70a4b00b377b.jpg",
    path: "/uploads/nf_10_feet_down.mp3"
  },
  {
    name: "Him & I",
    artist: "G-Eazy & Halsey",
    image: "https://i.discogs.com/qQCUQUB1JV7ue6SbMW2_oZPS35lSXxFqlVbzqYNJBUw/rs:fit/g:sm/q:90/h:600/w:600/czM6Ly9kaXNjb2dz/LWRhdGFiYXNlLWlt/YWdlcy9SLTExODc0/ODc2LTE1NjMwNDI2/NjYtMjUyNi5qcGVn.jpeg",
    path: "/uploads/g-eazy_him_i.mp3"
  }
];

export default function Player() {
  const [trackIndex, setTrackIndex] = useState(0);
  const [audio] = useState(new Audio(tempTrackList[trackIndex].path));
  const [playing, setPlaying] = useState(false) ;
  const [volume, setVolume] = useState(100);
  const [trackProgress, setTrackProgress] = useState(0);
  const [currentTime, setCurrentTime] = useState('00:00');
  const [duration, setDuration] = useState('00:00');

  const reset = () => {
    setCurrentTime('00:00');
    setDuration('00:00');
    setTrackProgress(0);
  }

  const loadTrack = () => {
    let updateTimer;
    clearInterval(updateTimer);
    reset();

    audio.src = tempTrackList[trackIndex].path;
    audio.load();
    
    updateTimer = setInterval(remoteUpdate, 1000);
    audio.addEventListener('ended', next);
  }

  const play = () => {
    setPlaying(true);
    audio.play();
  }

  const pause = () => {
    setPlaying(false);
    audio.pause();
  }

  const prev = () => {
      if (trackIndex > 0) {
        setTrackIndex(trackIndex - 1);
      }
      else {
        setTrackIndex(tempTrackList.length - 1);
      }
  
      loadTrack();
      play();
  }
    
  const next = () => {
      if (trackIndex < tempTrackList.length - 1) {
        setTrackIndex(trackIndex + 1);
      }
      else {
        setTrackIndex(0);
      }
      
      loadTrack();
      play();
  }  

  const remoteUpdate = () => {
    let progressPosition = 0;
    
    if (!isNaN(audio.duration)) {
      progressPosition = audio.currentTime * (100 / audio.duration);
      setTrackProgress(progressPosition);

      let currentMinutes: number | string = Math.floor(audio.currentTime / 60);
      let currentSeconds: number | string = Math.floor(audio.currentTime - currentMinutes * 60);
      let durationMinutes: number | string = Math.floor(audio.duration / 60);
      let durationSeconds: number | string = Math.floor(audio.duration - durationMinutes * 60);

      if (currentSeconds < 10) 
          currentSeconds = `0${currentSeconds}`;
      if (durationSeconds < 10) 
          durationSeconds = `0${durationSeconds}`; 
      if (currentMinutes < 10) 
          currentMinutes = `0${currentMinutes}`;
      if (durationMinutes < 10) 
          durationMinutes = `0${durationMinutes}`; 

      setCurrentTime(`${currentMinutes}:${currentSeconds}`);
      setDuration(`${durationMinutes}:${durationSeconds}`);
    }
  }

  const seek = (value: number) => {
    audio.currentTime = audio.duration * value / 100;
  }

  const changeVolume = (value: number) => {
    setVolume(value);
    audio.volume = value / 100;
  }

  useEffect(() => {
    if (trackIndex >= 0) {
      loadTrack();
    }
  }, [trackIndex]);

  return (
    <div className="player_container">
      <div className="player">
        <div className="player_left_content">
          <div className="content">
              <span className="album-link">
                <img 
                  src={ tempTrackList[trackIndex].image }
                  alt={ tempTrackList[trackIndex].name }
                  className="album-artwork" 
                />
              </span>

              <div className="track-info">
                <span className="track-name">
                  { tempTrackList[trackIndex].name }
                </span>
                <span className="artist-name">
                  { tempTrackList[trackIndex].artist }
                </span>
              </div>
          </div>
        </div>

        <div className="player_center_content">
          <div className="content player-controls">
            <div className="buttons">
              <button 
                onClick={() => prev()}
                id="prevBtn" 
                className="control-button previous" 
                title="Previous button"
              >
                <img 
                  src="/assets/images/previous.png" 
                  alt="Previous" 
                />
              </button>
              
              <button 
                onClick={() => play()}
                style={{ display: `${ playing ? 'none' : 'inline' }` }}
                id="playBtn" 
                className="control-button play" 
                title="Play button"
              >
                <img 
                  src="/assets/images/play.png" 
                  alt="Play" 
                />
              </button>
              
              <button 
                onClick={() => pause()}
                style={{ display: `${ playing ? 'inline' : 'none' }` }}
                id="pauseBtn" 
                className="control-button pause" 
                title="Pause button" 
              >
                <img 
                  src="/assets/images/pause.png" 
                  alt="Pause" 
                />
              </button>
                
              <button 
                onClick={() => next()}
                id="nextBtn" 
                className="control-button next" 
                title="Next button"
              >
                <img 
                  src="/assets/images/next.png" 
                  alt="Next" 
                />
              </button>
            </div>

            <div className="playback-bar">
                <span id="currentTime" className="progress-time current">
                  { currentTime }
                </span>
                <div className="progressbar">
                  <input 
                    value={ trackProgress }
                    onChange={(e) => seek(parseInt(e.target.value))} 
                    id="trackProgress" 
                    type="range" 
                    min="0" 
                    max="100" 
                    className="progressbar-bg" 
                  />
                </div>
                <span id="duration" className="progress-time remaining">
                  { duration }
                </span>
            </div>
          </div>
        </div>

        <div className="player_right_content">
          <div className="volume-bar">
            <button 
              id="muteBtn" 
              className="control-button volume" 
              title="Volume button"
              // onClick={() => mute()}
            >
              <img 
                src="/assets/images/volume.png" 
                alt="Volume" 
              />
            </button>
            
            <div className="progressbar">
              <input 
                value={ volume }
                onChange={(e) => changeVolume(parseInt(e.target.value))}
                id="volumeProgress" 
                type="range" 
                min="0" 
                max="100" 
                className="progressbar-bg" 
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  )
}
