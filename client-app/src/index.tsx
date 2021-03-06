import React from "react";
import ReactDOM from "react-dom";
import "./Css/index.css";
import App from "./modules/layout/App";
import reportWebVitals from "./reportWebVitals";
import "antd/dist/antd.css";
import { rootStore, RootStoreContext } from "./library/main/store/rootStore";
import { Router } from "react-router-dom";
import { createBrowserHistory } from "history";

export const history = createBrowserHistory();
ReactDOM.render(
  <RootStoreContext.Provider value={rootStore}>
    <Router history={history}>
      <App />
    </Router>
  </RootStoreContext.Provider>,
  document.getElementById("root")
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
