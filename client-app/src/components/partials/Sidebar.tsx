const Sidebar = () => {
    return (
        <aside id="navBarContainer">
            <nav className="navbar">
                <a href="/" className="logo">
                    <img 
                        src={ require('../../assets/images/icons/logo.png') } 
                        alt="Logo" 
                    />
                </a>

                <div className="group">
                    <div className="nav-item">
                        <a href="#" className="nav-item-link">
                            Поиск
                            <img 
                                src={ require('../../assets/images/icons/search.png') } 
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
                    <div className="nav-item">
                        <a href="/upload" className="nav-item-link">Загрузить трек</a>
                    </div>
                </div>
            </nav>
        </aside>
    )
}

export default Sidebar