import { requests } from "./requestHandling";
import { IPagination } from "../../models/IPagination";

export const attendeeHandling = {
  list: (pageination: IPagination | undefined, activityId: string) =>
    requests.get(`/Attendee/${activityId}`, pageination),
  attend: (activityId: string) =>
    requests.post(`/Attendee/attend`, { activityId: activityId }),
};
