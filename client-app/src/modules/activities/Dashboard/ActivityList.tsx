import React from "react";
import { observer } from "mobx-react-lite";

import { IActivityItem } from "./../../../library/models/IActivity";
import { ActivityItems } from "./ActivityItems";
import { List } from "antd";
import { useStore } from "./../../../library/main/store/rootStore";

export const ActivityList = observer(() => {
  const rootStore = useStore();
  const { activitiesByDate } = rootStore.activityStore;
  return (
    <>
      <List
        className="activity-list"
        dataSource={activitiesByDate}
        renderItem={([key, activities]) => (
          <div key={key}>
            <label>{key}</label>
            {activities.map((currentActivity: IActivityItem) => (
              <ActivityItems activity={currentActivity} />
            ))}
          </div>
        )}
      ></List>
    </>
  );
});
export default observer(ActivityList);
