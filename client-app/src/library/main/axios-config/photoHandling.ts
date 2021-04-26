import { requests, SetUrlParams } from "./requestHandling";
import { IPagination } from "../../models/IPagination";
export const photoHandling = {
  list: (pagination: IPagination, activityId: string, userPhotoId: string) =>
    requests.get(
      "/comment",
      SetUrlParams(pagination.Limit, pagination.Offset, {
        activityId: activityId,
        userPhotoId: userPhotoId,
      })
    ),
};
