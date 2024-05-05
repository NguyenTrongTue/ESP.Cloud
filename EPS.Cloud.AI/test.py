import getpass
import os
from langchain_community.utilities import SQLDatabase
import pandas as pd
from sqlalchemy import create_engine
from langchain_community.agent_toolkits import create_sql_agent
from langchain_openai import ChatOpenAI

# db = SQLDatabase.from_uri("sqlite:///Chinook.db")
# print(db.dialect)
# print(db.get_usable_table_names())
# db.run("SELECT * FROM Artist LIMIT 10;")
# df = pd.read_csv("garage.csv")
engine = create_engine("sqlite:///garage.db")
# df.to_sql("garage", engine, index=False)
db = SQLDatabase(engine=engine)
llm = ChatOpenAI(model="gpt-3.5-turbo", temperature=0)
agent_executor = create_sql_agent(llm, db=db, agent_type="openai-tools", verbose=True)


agent_executor.invoke({"input": "Tên garage nào là dài nhất"})
# print(db.dialect)
# print(db.get_usable_table_names())