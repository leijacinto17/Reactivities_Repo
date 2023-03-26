import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { Activity, ActivityFormValues } from "../models/activity";
import { PaginatedResult } from "../models/pagination";
import { Photo, Profiles, UserActivity } from "../models/profiles";
import { User, UserAboutValues, UserFormValues } from "../models/user";
import { router } from "../router/Routes";
import { store } from "../stores/store";

const sleep = (delay: number) => {
  return new Promise((resolve) => {
    setTimeout(resolve, delay);
  });
};

axios.defaults.baseURL = process.env.REACT_APP_API_URL;

axios.interceptors.request.use((config) => {
  const token = store.commonStore.token;
  if (token && config.headers) config.headers.Authorization = `Bearer ${token}`;
  return config;
});

axios.interceptors.response.use(
  async (response) => {
    if (process.env.NODE_ENV === "development") await sleep(1000);
    const pagination = response.headers["pagination"];
    if (pagination) {
      response.data = new PaginatedResult(
        response.data,
        JSON.parse(pagination)
      );
      // we used as AxiosResponse for intellisense type safety
      return response as AxiosResponse<PaginatedResult<any>>;
    }
    return response;
  },
  (error: AxiosError) => {
    const { data, status, config } = error.response as AxiosResponse;
    switch (status) {
      case 400:
        if (config.method === "get" && data.errors.hasOwnProperty("id")) {
          router.navigate("/not-found");
        }
        if (data.errors) {
          const modalStateErrors = [];
          for (const key in data.errors) {
            if (data.errors[key]) {
              modalStateErrors.push(data.errors[key]);
            }
          }
          throw modalStateErrors.flat();
        } else {
          toast.error(data);
        }
        break;
      case 401:
        toast.error("unauthorized");
        break;
      case 403:
        toast.error("forbidden");
        break;
      case 404:
        router.navigate("/not-found");
        break;
      case 500:
        store.commonStore.setServerError(data);
        router.navigate("/server-error");
        break;
    }
    return Promise.reject(error);
  }
);

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
  get: <T>(url: string) => axios.get<T>(url).then(responseBody),
  post: <T>(url: string, body: {}) =>
    axios.post<T>(url, body).then(responseBody),
  put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
  del: <T>(url: string) => axios.delete<T>(url).then(responseBody),
};

const Activities = {
  list: (params: URLSearchParams) =>
    axios
      .get<PaginatedResult<Activity[]>>(`/Activities`, { params })
      .then(responseBody),
  details: (id: string) => requests.get<Activity>(`/Activities/${id}`),
  create: (activity: ActivityFormValues) =>
    requests.post<void>(`/Activities`, activity),
  update: (activity: ActivityFormValues) =>
    requests.put<void>(`/Activities/${activity.id}`, activity),
  delete: (id: string) => requests.del<void>(`/Activities/${id}`),

  attend: (id: string) => requests.post<void>(`/Activities/${id}/Attend`, {}),
};

const Account = {
  current: () => requests.get<User>("/Account"),
  login: (user: UserFormValues) => requests.post<User>("/Account/Login", user),
  register: (user: UserFormValues) =>
    requests.post<User>("/Account/Register", user),
};

const Profile = {
  get: (username: string) => requests.get<Profiles>(`/Profiles/${username}`),
  uploadPhoto: (file: Blob) => {
    let formData = new FormData();
    formData.append("File", file);
    return axios.post<Photo>("Photo", formData, {
      headers: { "Content-Type": "multipart/form-data" },
    });
  },
  setMainPhoto: (id: string) => requests.post(`/Photo/${id}/SetMainPhoto`, {}),
  deletePhoto: (id: string) => requests.del(`/Photo/${id}`),
  updateAbout: (username: string, profile: UserAboutValues) =>
    requests.put<Profiles>(`/Profiles/${username}/EditAbout`, profile),
  updateFollowing: (username: string) =>
    requests.post(`/Follow/${username}/Follow`, {}),
  listFollowings: (username: string, predicate: string) =>
    requests.get<Profiles[]>(
      `Follow/${username}/GetFollowing?predicate=${predicate}`
    ),
  listOfUseActivities: (username: string, predicate: string) =>
    requests.get<UserActivity[]>(
      `Profiles/${username}/Activities?predicate=${predicate}`
    ),
};

const agent = {
  Activities,
  Account,
  Profile,
};

export default agent;
