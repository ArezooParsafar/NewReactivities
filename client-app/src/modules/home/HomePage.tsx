import React from "react";
import { useStore } from "./../../library/main/store/rootStore";
import { Button } from "antd";
import { Link } from "react-router-dom";
import { observer } from "mobx-react-lite";

export default observer(function HomePage() {
  const { userStore } = useStore();

  return (
    <div style={{ margin: "7em" }}>
      <h1>Hello home page</h1>
      {userStore.isLoggedIn ? (
        <>
          <h3> wellcome to reactivities</h3>
          <Button>
            <Link to={"/activities"}>Go to Activities</Link>
          </Button>
        </>
      ) : (
        <Button>
          <Link to={"/login"}>Login!</Link>
        </Button>
      )}
    </div>
  );
});
