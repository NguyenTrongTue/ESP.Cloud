from flask import Flask, request, jsonify
import getpass
import os
from langchain_community.utilities import SQLDatabase
import pandas as pd
from sqlalchemy import create_engine
from langchain_community.agent_toolkits import create_sql_agent
from langchain_openai import ChatOpenAI


app = Flask(__name__)

@app.route('/get-message')
def get_message():
    message = train_model()
    user_data  = {
        "id": 1,
        "message": message, 
    }
    extra = request.args.get('extra')

    if extra: 
        user_data['extra'] = extra
        
    return jsonify(user_data), 200


def train_model():
   
    engine = create_engine("sqlite:///garage.db")
    db = SQLDatabase(engine=engine)
    message = db.get_usable_table_names()

    return message

if __name__ == "__main__":
    app.run(debug=True)