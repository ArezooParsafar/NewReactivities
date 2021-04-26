import axios, { AxiosError, AxiosResponse } from "axios";
import { Alarm } from "../../Shared/Alarm";
import { history } from "./../../../index";
import { CommonStore } from "./../store/commonStore";
import { rootStore } from "./../store/rootStore";
axios.defaults.baseURL = "http://localhost:5000/";
axios.interceptors.request.use(
  (config) => {
    const token = rootStore.commonStore.token;
    if (token) config.headers.Authorization = `Bearer ${token}`;

    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

const sleep = (delay: number) => {
  return new Promise((resolved) => {
    setTimeout(resolved, delay);
  });
};

const responseBody = <T>(response: AxiosResponse<T>) => response.data;
export const SetUrlParams = (
  limit?: number,
  page?: number,
  restData?: object
) => {
  const params: { [k: string]: any } = {};
  limit = limit ? limit : 3;
  page = page ? page : 0;
  params.limit = limit;
  params.offset = `${page ? page * limit : 0}`;

  return params;
};

export const requests = {
  get: <T>(url: string, params: {} | undefined) =>
    axios
      .get<T>(url, { params: params })
      .then(responseBody),
  post: <T>(url: string, data: {}) =>
    axios.post<T>(url, data).then(responseBody),
  edit: <T>(url: string, data: {}) =>
    axios.put<T>(url, data).then(responseBody),
  delete: <T>(url: string) => axios.delete<T>(url).then(responseBody),
  fileForm: <T>(url: string, data: {}) => {
    let formData = new FormData();
    for (var [key, value] of Object.entries(data)) {
      formData.append(key, value instanceof Blob ? value : String(value));
    }

    return axios.post<T>(url, formData, {
      headers: { "Content-type": "multipart/form-data" },
    });
  },
};
