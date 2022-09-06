import ReactDOM from 'react-dom/client'
import { BrowserRouter } from 'react-router-dom'
import App from './layout/App'
import { store, StoreContext } from './store/store'
import reportWebVitals from './reportWebVitals'
import CustomRouter from './components/CustomRouter'
import history from './utils/history'

const root = ReactDOM.createRoot(
	document.getElementById('root') as HTMLElement
)
root.render(
	<StoreContext.Provider value={store}>
		<CustomRouter history={history}>
			<App />
		</CustomRouter>
	</StoreContext.Provider>
)

reportWebVitals()