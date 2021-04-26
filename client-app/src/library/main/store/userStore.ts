import { UserItem, UserForm } from "../../models/user";
import { makeAutoObservable, runInAction } from "mobx";
import { userHandling } from "./../axios-config/userHandling";
import { rootStore } from "./rootStore";
import { history } from "./../../../index";

export default class UserStore {
  user: UserItem | null = null;
  loginErrorText: string | null = null;

  constructor() {
    makeAutoObservable(this);
  }

  get isLoggedIn() {
    debugger;
    return !!this.user;
  }

  UserLogin = async (creds: UserForm) => {
    try {
      const user = await userHandling.login(creds);
      debugger;
      rootStore.commonStore.setToken(user.Token);
      runInAction(() => {
        this.user = user;
      });
      history.push("/activities");
    } catch (e) {
      console.log(e);
      throw e;
    }
  };

  logout = () => {
    rootStore.commonStore.setToken(null);
    this.user = null;
    history.push("/");
  };

  IsLoggedIn = () => {
    return !!this.user;
  };

  getUser = async () => {
    try {
      var user = await userHandling.current();
      runInAction(() => (this.user = user));
    } catch (e) {
      console.log(e);
    }
  };
  LoginError = (error: any) => {
    let { values, errorFields, outOfDate } = error;
    this.loginErrorText = errorFields;
  };
}
