import { DatePicker, Form } from "antd";
import { Rule } from "antd/lib/form";
import { Moment } from "moment";
import React, { FC } from "react";

interface IProps extends React.AriaAttributes {
  rules?: Rule[];
  required?: boolean;
  label: string;
  name: string;
  format: string;
}

export const DateInput: FC<IProps> = (props) => {
  let { name, rules, label, required, format } = props;
  if (required) {
    rules?.push({ required: required, message: `Please input your ${label}!` });
  }
  return (
    <>
      <Form.Item label={label} name={name} rules={rules}>
        <DatePicker format={format} />
      </Form.Item>
    </>
  );
};
