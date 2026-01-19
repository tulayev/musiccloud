import { BrowserHistory } from 'history';
import { ReactNode, useLayoutEffect, useState } from 'react';
import { Router } from 'react-router-dom';

interface Props {
  children: ReactNode;
  history: BrowserHistory;
}

export default function CustomRouter({ children, history }: Props) {
  const [state, setState] = useState({
    action: history.action,
    location: history.location
  });
  
  useLayoutEffect(() => {
    history.listen(setState)
  }, [history]);
  
  return (
    <Router
      location={state.location}
      navigationType={state.action}
      navigator={history}
    >
      {children}
    </Router>
  )
}
