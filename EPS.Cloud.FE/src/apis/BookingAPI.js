import request from "@/utils/request.js";
import BaseAPI from "./BaseAPI";

class GarageAPI extends BaseAPI {
  constructor() {
    super("Booking");
  }

  /**
   * Get the make by garage ID.
   *
   * @param {type} garageId - description of parameter
   * @return {type} description of return value
   */
  getMakeByGarageId(garageId) {
    return request.get(
      this.url + `/get_make_by_garage_id?garageId=${garageId}`
    );
  }

  /**
   * Retrieves models by garage ID, make, and year of manufacture.
   *
   * @param {string} garageId - The ID of the garage
   * @param {string} make - The make of the car
   * @param {number} yearOfManufacture - The year of manufacture
   * @return {Promise} A Promise that resolves to the requested models
   */
  getModelsByGarageId(garageId, make, yearOfManufacture) {
    return request.get(
      `${this.url}/get_model_by_garage_id?garageId=${garageId}&make=${make}&year=${yearOfManufacture}`
    );
  }

  /**
   * Get years by garage ID and make.
   *
   * @param {type} garageId - description of parameter
   * @param {type} make - description of parameter
   * @return {type} description of return value
   */
  getYearsByGarageIdAndMake(garageId, make) {
    return request.get(
      `${this.url}/get_year_by_garage_id?garageId=${garageId}&make=${make}`
    );
  }
}

const instance = new GarageAPI();

export default instance;
