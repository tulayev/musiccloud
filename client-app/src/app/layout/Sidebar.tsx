import { Link } from 'react-router-dom'

export default function Sidebar() {
    return (
        <aside id="navBarContainer">
            <nav className="navbar">
                <Link to="/" className="logo">
                    <img 
                        src="/assets/images/icons/logo.png" 
                        alt="Logo" 
                    />
                </Link>

                <div className="group">
                    <div className="nav-item">
                        <a href="#" className="nav-item-link">
                            Поиск
                            <img 
                                src="/assets/images/icons/search.png" 
                                className="icon" 
                                alt="Search button" 
                            />
                        </a>
                    </div>
                </div>

                <div className="group">
                    <div className="nav-item">
                        <a href="/your-music" className="nav-item-link">Ваша музыка</a>
                    </div>
                    <div className="nav-item">
                        <a href="/auth" className="nav-item-link">Войти</a>
                    </div>
                </div>
            </nav>
        </aside>
    )
}