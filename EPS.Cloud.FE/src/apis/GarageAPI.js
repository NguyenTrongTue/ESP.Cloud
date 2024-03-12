import request from "@/utils/request.js";
import BaseAPI from "./BaseAPI";

class GarageAPI extends BaseAPI {
  constructor() {
    super("Garages");
  }

  searchGarages(payload) {
    return request.post(this.url + "/get_garages", payload);
  }

  getGarageServices() {
    return request.get(this.url + "/get_garage_services");
  }
  getCars() {
    return request.get(this.url + "/get_cars");
  }
  getGarageById(garage_id) {
    return request.get(this.url + `/get_garage_by_id?garage_id=${garage_id}`);
  }
}

const instance = new GarageAPI();

export default instance;
