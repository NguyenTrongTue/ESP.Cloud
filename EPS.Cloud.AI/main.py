from flask import Flask, request, jsonify
import getpass
import os
from langchain_community.utilities import SQLDatabase
import pandas as pd
from sqlalchemy import create_engine
from langchain_community.agent_toolkits import create_sql_agent
from langchain_openai import ChatOpenAI
from flask_cors import CORS 

app = Flask(__name__)
CORS(app)

@app.route('/get-message', methods=['POST'])
def get_message():
    question = request.json.get('question')
    message = train_model(question)
    user_data  = {
        "id": 1,
        "message": message,
    }
    extra = request.args.get('extra')

    if extra: 
        user_data['extra'] = extra
        
    return jsonify(user_data), 200


def train_model(question):
    os.environ["OPENAI_API_KEY"] = "sk-proj-5fIg6XGj31oEhEf4gOQYT3BlbkFJRIy316L9XTwOg1aU2zB3"

    engine = create_engine("sqlite:///garage.db")
    db = SQLDatabase(engine=engine)

    llm = ChatOpenAI(model="gpt-3.5-turbo", temperature=0)
    agent_executor = create_sql_agent(llm, db=db, agent_type="openai-tools", verbose=True)

    # query = {"input": "Gara nào gần tôi nhất? kinh độ và vĩ độ hiện tại của tôi là 21.037776 và 105.782996"}
    query = {"input": question}
    
    response = agent_executor.invoke(query)
    
    if response:
        message = response.get("output", "Không có kết quả")                       
    else:
        message = "Lỗi: Không có phản hồi từ mô hình"
    
    return message


if __name__ == "__main__":
    app.run(debug=True)