import { Spin } from "antd";
import { LoadingOutlined } from "@ant-design/icons";
import React from "react";
interface Props {
  inverted: boolean;
  content: string;
}
const antIcon = <LoadingOutlined style={{ fontSize: 24 }} spin />;

export default function LoadingComponent({
  inverted = true,
  content = "Loading...",
}: Props) {
  return (
    <div className="example">
      <Spin tip={content} indicator={antIcon} />
    </div>
  );
}
