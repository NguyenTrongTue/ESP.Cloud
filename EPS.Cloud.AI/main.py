from flask import Flask, request, jsonify
import os
from langchain_community.utilities import SQLDatabase
import pandas as pd
from sqlalchemy import create_engine
from langchain_community.agent_toolkits import create_sql_agent
from langchain_openai import ChatOpenAI
from flask_cors import CORS 

app = Flask(__name__)
CORS(app)

# Khởi tạo agent 
os.environ["OPENAI_API_KEY"] = ""
engine = create_engine("sqlite:///garage.db")
db = SQLDatabase(engine=engine)
llm = ChatOpenAI(model="gpt-3.5-turbo", temperature=0)
agent_executor = create_sql_agent(llm, db=db, agent_type="openai-tools", verbose=True)

@app.route('/get-message', methods=['POST'])
def get_message():
    question = request.json.get('question')
    extra = request.args.get('extra')

    query = {"input": question}
    response = agent_executor.invoke(query)

    if response:
        message = response.get("output", "Không có kết quả")
    else:
        message = "Lỗi: Không có phản hồi từ mô hình"

    user_data = {
        "id": 1,
        "message": message,
    }

    if extra:
        user_data['extra'] = extra

    return jsonify(user_data), 200

if __name__ == "__main__":
    app.run(debug=True)
