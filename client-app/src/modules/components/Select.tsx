import React, { FC } from "react";
import { Form, Select as AntSelect } from "antd";
import { SelectProps } from "antd/lib/select";
import { Rule } from "antd/lib/form";
import { IOptionData } from "../../library/models/IActivity";

interface IProps extends SelectProps<string> {
  rules?: Rule[];
  required?: boolean;
  label: string;
  name: string;
  optionData: IOptionData[];
}
export const Select: FC<IProps> = (props) => {
  let { name, rules, label, required, size, optionData, ...select } = props;
  const { Option } = AntSelect;

  if (required) {
    rules?.push({ required: required, message: `Please input your ${label}!` });
  }

  return (
    <Form.Item name={name} label={label} rules={rules}>
      <AntSelect {...select}>
        {optionData.map((item) => {
          return (
            <Option key={item.Key} value={item.Value}>
              {item.Label}
            </Option>
          );
        })}
      </AntSelect>
    </Form.Item>
  );
};
