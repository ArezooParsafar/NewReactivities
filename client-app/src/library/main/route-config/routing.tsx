import { createBrowserHistory } from "history";
import React from "react";
import { Route } from "react-router-dom";
import HomePage from "../../../modules/home/HomePage";

export const history = createBrowserHistory();
export const routing = () => {
  return (
    <>
      <Route exact path="/" component={HomePage} />
    </>
  );
};
