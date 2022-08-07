import Track from '../../modules/track'

interface Props {
    tracks: Track[]
}

export default function TrackIndex({tracks}: Props) {
    return (
        <>
            <h1 className="page-heading-big">Все треки</h1>

            <div className="grid-view-container"> 
                {
                    tracks.map(track => (
                        <div className="grid-view-item" key={track.id}>
                            <a href="#">
                                {/* <img src="" alt={track.title} />  */}
                                <div style={{ width: '100%', height: '200px', backgroundColor: 'white', color: 'black' }}>No image</div>
                                <div className="grid-view-info">
                                <h3>{track.title}</h3>
                                    <p>{track.author}</p>
                                </div>
                            </a>
                        </div>
                    ))
                }  
            </div>
        </>
    )
}