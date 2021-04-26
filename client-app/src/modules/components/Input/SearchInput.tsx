import { Form, Input } from "antd";
import { Rule } from "antd/lib/form";
import React, { FC } from "react";
import { SearchProps } from "antd/lib/input";

interface IProps extends SearchProps {
  rules?: Rule[];
  required?: boolean;
  label: string;
}

export const SearchInput: FC<IProps> = (props) => {
  let { name, rules, label, required, size, type, ...input } = props;

  if (required) {
    rules?.push({ required: required, message: `Please input your ${label}!` });
  }
  return (
    <>
      <Form.Item label={label} name={name} rules={rules}>
        <Input.Search size={size} {...input} />
      </Form.Item>
    </>
  );
};
