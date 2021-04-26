import ActivityStore from "./activityStore";
import { createContext, useContext } from "react";
import { configure } from "mobx";
import UserStore from "./userStore";
import { CommonStore } from "./commonStore";

configure({ enforceActions: "always" });
interface Store {
  activityStore: ActivityStore;
  userStore: UserStore;
  commonStore: CommonStore;
}
export const rootStore: Store = {
  activityStore: new ActivityStore(),
  userStore: new UserStore(),
  commonStore: new CommonStore(),
};

export const RootStoreContext = createContext(rootStore);
// create hooks
export function useStore() {
  return useContext(RootStoreContext);
}
