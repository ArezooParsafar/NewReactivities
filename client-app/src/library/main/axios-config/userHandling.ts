import { UserItem } from "../../models/user";
import { requests } from "./requestHandling";
import { UserForm } from "./../../models/user";

export const userHandling = {
  current: () => requests.get<UserItem>("/api/User", {}),
  login: (user: UserForm) => requests.post<UserItem>("/api/User/login", user),
  register: (user: UserForm) => requests.post<UserItem>("/User/register", user),
};
