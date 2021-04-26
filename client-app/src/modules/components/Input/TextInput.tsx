import { Form, Input } from "antd";
import { Rule } from "antd/lib/form";
import React, { FC } from "react";
import { InputProps } from "antd/lib/input";

interface IProps extends InputProps {
  rules?: Rule[];
  required?: boolean;
  label: string;
}

export const TextInput: FC<IProps> = (props) => {
  let { name, rules, label, required, size, ...input } = props;

  if (required) {
    rules?.push({ required: required, message: `Please input your ${label}!` });
  }
  return (
    <>
      <Form.Item label={label} name={name} rules={rules}>
        <Input size={size} {...input} />
      </Form.Item>
    </>
  );
};

