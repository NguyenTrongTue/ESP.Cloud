import axios from "axios";
const axiosClient = axios.create({
  baseURL: window.__configAPI,
});

axiosClient.interceptors.response.use(
  (res) => {
    if (res && res.data) {
      return res.data;
    }
    return res;
  },
  (error) => {
    const { response } = error;
    const { data } = response;

    return data;
  }
);
export default axiosClient;
