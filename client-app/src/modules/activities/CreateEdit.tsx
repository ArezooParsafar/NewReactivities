import { Form, Button } from "antd";
import { observer } from "mobx-react-lite";
import React, { useEffect, useState } from "react";
import { TextArea } from "../components/Input/TextArea";
import { TextInput } from "../components/Input/TextInput";
import { TimeInput } from "../components/Input/TimeInput";
import { useStore } from "./../../library/main/store/rootStore";
import SelectInput from "./../components/Input/SelectInput";
import { DateInput } from "./../components/Input/DateInput";
import { useParams } from "react-router-dom";
import LoadingComponent from "../components/common/loadingComponent";
import moment from "moment";
export default observer(function CreateEdit() {
  const { activityStore } = useStore();
  const {
    submitForm,
    loadCategories,
    categories,
    loadActivity,
    activity,
  } = activityStore;
  const { id } = useParams<{ id: string }>();
  const [selectedActivity, setActivity] = useState(activity);
  const [loadingInitial, setLoadingInitial] = useState(true);
  const [form] = Form.useForm();
  const layout = {
    labelCol: { xs: { span: 24 }, sm: { span: 16 } },
    wrapperCol: { xs: { span: 24 }, sm: { span: 16 } },
  };
  const validateMessages = {
    required: "${label} is required!",
    types: {
      email: "${label} is not a valid email!",
    },
  };
  useEffect(() => {
    loadCategories();
    if (id) {
      loadActivity(id).then((resultActivity) => {
        setActivity(resultActivity);
        form.setFieldsValue(selectedActivity);
      });
      setLoadingInitial(false);
    } else {
      setActivity(undefined);
      form.resetFields();
    }
  }, [id, selectedActivity, loadActivity, loadCategories, form]);

  if (loadingInitial) {
    <LoadingComponent inverted={true} content="Loading..." />;
  }
  return (
    <Form
      {...layout}
      validateMessages={validateMessages}
      initialValues={selectedActivity}
      onFinish={submitForm}
      form={form}
    >
      <TextInput label="Title" name="Title" required={true} />
      <TextArea name="Description" label="Description" />
      {categories.length > 0 && (
        <SelectInput
          label="Category"
          name="CategoryId"
          optionsData={categories}
        />
      )}
      <TextInput label="City" name="City" required={true} />
      <TextInput label="Venue" name="Venue" required={true} />
      <TimeInput name="HoldingTime" label="Holding Time" format="HH:mm" />
      <DateInput
        name="HoldingDateDisplay"
        label="Holding Date"
        format="YYYY/MM/DD"
      />

      <Form.Item>
        <Button htmlType="button">Cancel</Button>
        <Button htmlType="submit">OK</Button>
      </Form.Item>
    </Form>
  );
});
