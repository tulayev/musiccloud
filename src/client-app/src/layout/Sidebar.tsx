import { observer } from 'mobx-react-lite'
import { Link } from 'react-router-dom'
import LoginForm from '../pages/auth/LoginForm'
import RegisterForm from '../pages/auth/RegisterForm'
import { useStore } from '../store/store'

export default observer(function Sidebar() {
  const {modalStore, userStore} = useStore();

  return (
    <aside className="sidebar_container">
      <nav className="navbar">
        <Link to="/" className="logo">
          <img 
            src="/assets/images/logo.png" 
            alt="Logo" 
          />
        </Link>

        <div className="group">
          <div className="nav-item">
            <a href="#" className="nav-item-link">
              Поиск
              <img 
                src="/assets/images/search.png" 
                className="icon" 
                alt="Search button" 
              />
            </a>
          </div>
        </div>

        <div className="group">
          {userStore.isLoggedIn ?
            <>
              <div className="nav-item">
                <Link to="/your-music" className="nav-item-link">
                  Ваша музыка
                </Link>
              </div>
              <div className="nav-item">
                <Link to="/upload" className="nav-item-link">
                  Загрузить трек
                </Link>
              </div>
              <div className="nav-item">
                <p>Привет, {userStore.user?.displayName}!</p>
              </div>
              <div className="nav-item">
                <a
                  href="#" 
                  onClick={() => userStore.logout()} 
                  className="nav-item-link"
                >
                    Выйти
                </a> 
              </div>
            </> :
            <>
              <div className="nav-item">
                <a
                  href="#" 
                  onClick={() => modalStore.openModal(<LoginForm />)} 
                  style={{marginRight: 10}} 
                  className="nav-item-link"
                >
                      Войти
                </a> 
                <a
                  href="#" 
                  onClick={() => modalStore.openModal(<RegisterForm />)} 
                  className="nav-item-link"
                >
                  Регистрация
                </a>
              </div>
            </>
          }
        </div>
      </nav>
    </aside>
  )
})