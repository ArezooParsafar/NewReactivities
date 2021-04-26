import { makeAutoObservable, runInAction } from "mobx";
import { activityHandling } from "./../axios-config/activitiyHandling";
import { IActivityItem, IOptionData } from "../../models/IActivity";
import moment from "moment";
import { IActivity } from "./../../models/IActivity";

const LIMIT = 4;

export default class ActivityStore {
  constructor() {
    makeAutoObservable(this);
  }

  activities: IActivityItem[] = [];
  categories: IOptionData[] = [];
  activity: IActivityItem | undefined = undefined;
  loadingInitial = false;
  submitting = false;
  activityCount = 0;
  page = 0;

  get totalPages() {
    return Math.ceil(this.activityCount / LIMIT);
  }

  loadActivity = async (id: string) => {
    let activity = this.getActivity(id);
    if (activity) {
      this.activity = activity;
    } else {
      this.loadingInitial = true;
      try {
        activity = await activityHandling.details(id);
        runInAction(() => {
          this.setActivity(activity!);
          this.activity = activity;
        });
      } catch (e) {
        this.setInitialLoading(false);
      }
      this.setInitialLoading(false);
    }
    return activity;
  };

  loadActivities = async () => {
    this.setInitialLoading(true);
    try {
      const activitiesEnvelope = await activityHandling.list(
        undefined,
        LIMIT,
        this.page
      );
      const { ActivityItems, TotalCount } = activitiesEnvelope;
      runInAction(() => {
        ActivityItems.forEach((item) => {
          this.setActivity(item);
        });

        this.activityCount = TotalCount;
      });
    } catch (e) {
      console.log(e);

      this.setInitialLoading(false);
    }

    this.setInitialLoading(false);
  };
  private setActivity = (activity: IActivityItem) => {
    activity.HoldingDateDisplay = moment(activity.HoldingDate, "YYYY/MM/DD");
    activity.HoldingTime = moment(activity.HoldingDate, "HH:mm");

    this.activities.push(activity);
  };
  setInitialLoading = (state: boolean) => {
    this.loadingInitial = state;
  };

  get activitiesByDate(): [key: string, items: IActivityItem[]][] {
    var ss = this.groupActivitiesByDate(Array.from(this.activities));

    return ss;
  }

  setPage = (page: number) => {
    this.page = page;
  };

  groupActivitiesByDate = (activities: IActivityItem[]) => {
    return Object.entries(
      activities.reduce((activities, activity) => {
        let date = new Date(activity.HoldingDate!).toDateString();
        activities[date] = activities[date]
          ? [...activities[date], activity]
          : [activity];
        return activities;
      }, {} as { [key: string]: IActivityItem[] })
    );
  };

  getActivity = (id: string) => {
    return this.activities.find((c) => c.ActivityId === id);
  };

  submitForm = (values: IActivityItem) => {
    try {
      let sendActivity = values as IActivity;
     sendActivity.HoldingDate = new Date(
        values.HoldingDateDisplay.format("YYYY-MM-DD") +
          "T" +
          values.HoldingTime.format("HH:mm")
      );
      this.activity && this.activity.ActivityId
        ? activityHandling.update(sendActivity, this.activity.ActivityId!)
        : activityHandling.create(sendActivity);
    } catch (e) {
      console.log(e);
    }
  };

  loadCategories = async () => {
    this.loadingInitial = true;
    if (this.categories.length <= 0) {
      try {
        const categories = await activityHandling.categoryList();
        runInAction(() => {
          categories.forEach((item) => {
            this.categories.push(item);
          });
          this.setInitialLoading(false);
        });
      } catch (e) {
        console.log(e);
        this.setInitialLoading(false);
      }
    }
  };
}
