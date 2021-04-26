import React, { useEffect } from "react";
import "./App.css";
import ActivityDashboard from "../activities/Dashboard/ActivityDashboard";
import Navbar from "../components/common/Navbar";
import { Route, Switch, useLocation } from "react-router-dom";
import HomePage from "./../home/HomePage";
import CreateEdit from "./../activities/CreateEdit";
import ActivityDetails from "./../activities/Dashboard/ActivityDetails";
import LoginForm from "../users/LoginForm";
import { NotFound } from "./NotFound";
import { useStore } from "./../../library/main/store/rootStore";
import { observer } from "mobx-react-lite";
import LoadingComponent from "../components/common/loadingComponent";

function App() {
  const location = useLocation();
  const { userStore, commonStore } = useStore();
  useEffect(() => {
    if (commonStore.token) {
      userStore.getUser().finally(() => {
        commonStore.setAppLoaded();
      });
    } else {
      commonStore.setAppLoaded();
    }
  }, [commonStore, userStore]);
  if (!commonStore.appLoaded)
    return <LoadingComponent content="Loading..." inverted={true} />;
    
  return (
    <>
      <Route exact path="/" component={HomePage} />
      <Route
        path={"/(.+)"}
        render={() => (
          <>
            <Navbar />
            <Switch>
              <Route exact path="/activities" component={ActivityDashboard} />
              <Route path="/activities/:id" component={ActivityDetails} />
              <Route path="/login" component={LoginForm} />
              <Route
                key={location.key}
                path={["/createActivity", "/manage/:id"]}
                component={CreateEdit}
              />
              <Route component={NotFound} />
            </Switch>
          </>
        )}
      />
    </>
  );
}

export default observer(App);
