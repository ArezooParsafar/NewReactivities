import Form from "antd/lib/form/Form";
import React from "react";
import { TextInput } from "../components/Input/TextInput";
import { Alert, Button } from "antd";
import { useStore } from "../../library/main/store/rootStore";
import { observer } from "mobx-react-lite";

export default observer(function LoginForm() {
  const { userStore } = useStore();
  const { UserLogin, loginErrorText, LoginError } = userStore;
  return (
    <div>
      <Form onFinish={UserLogin} onFinishFailed={LoginError}>
        <TextInput name="Email" label="Email:" placeholder="email" />
        <TextInput
          name="Password"
          label="Password:"
          placeholder="password"
          type="password"
        />
        {loginErrorText && <Alert message="Error Text" type="error" />}
        <Button htmlType="submit">OK</Button>
      </Form>
    </div>
  );
});
