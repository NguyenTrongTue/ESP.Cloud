import request from "@/utils/request.js";
import BaseAPI from "./BaseAPI";

class AnswersAPI extends BaseAPI {
  constructor() {
    super("Answers");
  }
  /**
   * Retrieves an answer by question ID.
   *
   * @param {type} questionId - The ID of the question.
   * @return {type} The answer corresponding to the question ID.
   */
  getAnswerByQuestionId(questionId) {
    return request.get(
      this.url + `/get_answer_by_questions_id?questionId=${questionId}`
    );
  }

  /**
   *  A description of the entire function.
   *
   *  @param {Object} payload - description of parameter
   *  @return {type} description of return value
   */
  getAnswerRecently(payload) {
    return request.post(
      this.url + `/get_answer_recently`, payload
    );
  }
}

const instance = new AnswersAPI();

export default instance;
