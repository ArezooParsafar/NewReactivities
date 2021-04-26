import {
  IActivity,
  IACtivityEnvelope,
  IActivityItem,
  IOptionData,
} from "../../models/IActivity";
import { requests, SetUrlParams } from "./requestHandling";

export const activityHandling = {
  categoryList: () =>
    requests.get<IOptionData[]>("/Activity/categories", undefined),
  list: (params?: object, limit?: number, page?: number) =>
    requests.get<IACtivityEnvelope>(
      "/Activity",
      SetUrlParams(limit, page, params)
    ),
  details: (id: string) => requests.get<IActivityItem>(`/Activity/${id}`, {}),
  userActivities: (username: string, predicate: string) =>
    requests.get(`/Activity/${username}/Attendee/${predicate}`, {}),
  create: (activity: IActivity) => requests.post("/Activity", activity),
  delete: (id: string) => requests.delete(`/Activity/${id}`),
  update: (activity: IActivity, id: string) =>
    requests.edit(`/Activity/${id}`, activity),
};
