import { Form, TimePicker } from "antd";
import { Rule } from "antd/lib/form";
import React, { FC } from "react";
import { Moment } from "moment";
import { TimeRangePickerProps } from "antd/lib/time-picker";

interface IProps extends TimeRangePickerProps {
  rules?: Rule[];
  required?: boolean;
  label: string;
  defaultTime?: Moment;
}

export const TimeInput: FC<IProps> = (props) => {
  let { name, rules, label, required, defaultTime, format } = props;
  if (required) {
    rules?.push({ required: required, message: `Please input your ${label}!` });
  }
  return (
    <>
      <Form.Item label={label} name={name} rules={rules}>
        <TimePicker format={format} />
      </Form.Item>
    </>
  );
};
