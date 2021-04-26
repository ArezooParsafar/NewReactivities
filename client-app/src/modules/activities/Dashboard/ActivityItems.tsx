import { Button, Col, Divider, Image, List, Row } from "antd";

import {
  CalendarOutlined,
  ClockCircleOutlined,
  HomeOutlined,
  UserOutlined,
} from "@ant-design/icons";
import React, { FC } from "react";
import { IActivityItem } from "../../../library/models/IActivity";
import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";

export const ActivityItems: FC<{
  activity: IActivityItem;
}> = observer(({ activity }) => {
  return (
    <List.Item key={activity.ActivityId}>
      <Row>
        <Col span={4}>
          <Image
            src={
              activity.ImagePath
                ? activity.ImagePath
                : "../../../../public/default/default.jpg"
            }
          />
        </Col>
        <Col span={8} className="activity-title">
          <span>{activity.Title}</span>
          <span>{activity.CategoryName}</span>
        </Col>
        <Col className="activity-title">
          <span>
            <HomeOutlined /> {activity.City} {activity.Venue}
          </span>
          <span>
            <UserOutlined /> Hosted by : {activity.HostName}
          </span>
        </Col>
      </Row>
      <Divider />
      <Row className="activity-footer">
        <Col span={22}>
          <ClockCircleOutlined /> {activity.HoldingTime.format("HH:mm")}
          <Divider type="vertical" />
          {activity.HoldingTime.format("YYYY/MM/DD")}
          <Divider type="vertical" />
          <CalendarOutlined />
          Activity {activity.MonthDiff} months in{" "}
          {activity.MonthDiff > 0 ? "futur" : "past"}
        </Col>
        <Col className="view-activity" span={2}>
          <Button type="primary">
            <Link to={`/activities/${activity.ActivityId}`}>View</Link>
          </Button>
          <Button htmlType="button">Delete</Button>
        </Col>
      </Row>
    </List.Item>
  );
});
