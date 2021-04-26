import React from "react";
import { Avatar, Button, Menu, Image, Dropdown } from "antd";
import { Link } from "react-router-dom";
import { useStore } from "./../../../library/main/store/rootStore";
import { observer } from "mobx-react-lite";

export default observer(function Navbar() {
  const {
    userStore: { user, logout },
  } = useStore();
  return (
    <Menu mode="horizontal">
      <Menu.Item key={"logo"}>
        <img src="/assets/logo.png" alt="logo" />
      </Menu.Item>
      <Menu.Item key="activities">
        <Link to="/activities">Activities</Link>
      </Menu.Item>

      <Menu.Item>
        <Button>
          <Link to="/createActivity">Create Activity</Link>
        </Button>
      </Menu.Item>
      <Menu.Item key="login">
        <div>
          <Avatar src={<Image src={user?.ProfileImage} />} />
          <Dropdown
            overlay={
              <Menu>
                <Menu.Item icon="user">
                  <Link to={`/profile/${user?.Username}`}>My Profile</Link>
                </Menu.Item>
                <Menu.Item onClick={logout} icon="power">
                  Logout
                </Menu.Item>
              </Menu>
            }
            placement="bottomLeft"
          >
            <Button>{user?.DisplayName}</Button>
          </Dropdown>
        </div>
      </Menu.Item>
    </Menu>
  );
});
