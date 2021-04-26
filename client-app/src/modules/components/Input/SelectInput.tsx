import { Select } from "antd";
import { SelectProps } from "antd/lib/select";
import React from "react";
import { IOptionData } from "../../../library/models/IActivity";
import Form, { Rule } from "antd/lib/form";
interface IProps extends SelectProps<string> {
  label: string;
  required?: boolean;
  rules?: Rule[];
  name: string;
  optionsData: IOptionData[];
}
export default function SelectInput(props: IProps) {
  const {
    optionsData,
    label,
    name,
    rules,
    defaultValue,
    required,
    ...select
  } = props;
  const { Option } = Select;
  if (required) {
    rules?.push({ required: required, message: `Please input your ${label}!` });
  }

  return (
    <Form.Item label={label} name={name} rules={rules}>
      <Select {...select} allowClear optionLabelProp="title">
        {optionsData?.map((item) => {
          return (
            <Option key={item.Key} value={item.Value} title={item.Label}>
              {item.Label}
            </Option>
          );
        })}
      </Select>
    </Form.Item>
  );
}
