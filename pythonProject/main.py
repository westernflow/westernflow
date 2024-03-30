#!/usr/bin/env python
from fastapi import FastAPI
from langchain.prompts import ChatPromptTemplate
from langchain_community.chat_models import ChatOpenAI
from langserve import add_routes
from dotenv import load_dotenv
import os

load_dotenv()
openai_api_key = os.getenv("OPENAI_API_KEY")

app = FastAPI(
    title="LangChain Server",
    version="1.0",
    description="A simple api server using Langchain's Runnable interfaces",
)

add_routes(
    app,
    ChatOpenAI(),
    path="/openai",
)


model = ChatOpenAI(model="gpt-4", api_key=openai_api_key)
prompt = ChatPromptTemplate.from_template("tell me a joke about {topic}")
add_routes(
    app,
    prompt | model,
    path="/joke",
    )

prompt = ChatPromptTemplate.from_template("From the given list of professors: {professorOptions}, output the "
                                          "one you believe matches this professor: {professorInformation}."
                                          "Output your response as json containing the professor's name (excluding "
                                          "honorifics such as Dr, Mrs, etc) and their email address as separate "
                                          "fields.")
chain = prompt | model
add_routes(
    app,
    chain,
    path="/linkprofessor",
    )


if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="localhost", port=8000)