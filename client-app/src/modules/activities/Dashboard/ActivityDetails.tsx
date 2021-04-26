import {
  CalendarOutlined,
  ClockCircleOutlined,
  EditOutlined,
  LeftCircleOutlined,
} from "@ant-design/icons";
import { Card, Divider } from "antd";
import Meta from "antd/lib/card/Meta";
import { observer } from "mobx-react-lite";
import React, { useEffect } from "react";
import { Link, useParams } from "react-router-dom";
import LoadingComponent from "../../components/common/loadingComponent";
import { useStore } from "./../../../library/main/store/rootStore";

export default observer(function ActivityDetails() {
  const { activityStore } = useStore();
  const { activity, loadActivity } = activityStore;
  const { id } = useParams<{ id: string }>();
  useEffect(() => {
    if (id) {
      loadActivity(id);
    }
  }, [id, loadActivity]);

  if (!activity) return <LoadingComponent inverted={true} content="loading" />;

  return (
    <Card
      style={{ width: 300 }}
      cover={
        <img
          alt="example"
          src={`/assets/categoryImages/${activity.ImageFile}.jpg`}
        />
      }
      actions={[
        <Link to={`/manage/${activity.ActivityId}`}>
          <EditOutlined key="edit" />
        </Link>,
        <Link to="/activities">
          <LeftCircleOutlined />
        </Link>,
      ]}
    >
      <Meta title={activity.Title} description={activity.CategoryName} />
      <p>{activity.Venue}</p>
      <p>
        <ClockCircleOutlined /> {activity.HoldingTime.format("HH:mm")}
        <Divider type="vertical" />
        {activity.HoldingTime.format("YYYY/MM/DD")}
        <Divider type="vertical" />
        <CalendarOutlined />
      </p>
      <p>{activity.Description}</p>
    </Card>
  );
});
