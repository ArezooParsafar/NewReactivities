import { Col, Row, Skeleton, Spin } from "antd";
import React, { useEffect, useState } from "react";
import { ActivityList } from "./ActivityList";
import "../../../Css/activity.css";
import InfiniteScroll from "react-infinite-scroll-component";
import { observer } from "mobx-react-lite";
import { useStore } from "./../../../library/main/store/rootStore";

export default observer(function ActivityDashboard() {
  const { activityStore } = useStore();
  const {
    loadActivities,
    loadingInitial,
    setPage,
    activityCount,
    page,
    totalPages,
  } = activityStore;

  const [loadingNext, setLoadingNext] = useState(false);

  const handleInfiniteOnLoad = () => {
    setLoadingNext(true);
    setPage(page + 1);
    loadActivities().then(() => setLoadingNext(false));
  };

  useEffect(() => {
    loadActivities();
  }, [loadActivities]);

  if (loadingInitial && page === 0) {
    return <Skeleton avatar paragraph={{ rows: 4 }} />;
  }
  return (
    <>
      <Row>
        <Col md={{ span: 13, offset: 2 }}>
          <ActivityList />
          <InfiniteScroll
            dataLength={activityCount}
            next={handleInfiniteOnLoad}
            hasMore={!loadingNext && totalPages >= page}
            loader={<Spin />}
            //initialLoad={loadingNext}
          >
            {loadingInitial && page + 1 <= totalPages && (
              <Skeleton avatar paragraph={{ rows: 4 }} />
            )}
          </InfiniteScroll>
        </Col>
        <Col md={{ span: 6, offset: 1 }}>
          <h2>activity filters</h2>
        </Col>
        <Col md={{ offset: 2 }}></Col>
      </Row>
    </>
  );
});
