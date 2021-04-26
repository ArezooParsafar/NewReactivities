import { notification } from "antd";

export class Alarm {
  openNotification = (message: string, title: string, type: string = "error",duration:number=10) => {
    notification.open({
      message: title,
      description: message,
      className:type,
      placement:'bottomRight',
      onClick: () => {
        console.log("Notification Clicked!");
      },
    });
  };
}
