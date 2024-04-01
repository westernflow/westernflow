#!/usr/bin/env python
from fastapi import FastAPI, Request
import time
from langchain.prompts import ChatPromptTemplate
from langchain_community.cache import InMemoryCache
from langchain_community.chat_models import ChatOpenAI
from langserve import add_routes
from dotenv import load_dotenv
import os
from langchain.globals import set_llm_cache


load_dotenv()
openai_api_key = os.getenv("OPENAI_API_KEY")

app = FastAPI(
    title="LangChain Server",
    version="1.0",
    description="A simple api server using Langchain's Runnable interfaces",
)

set_llm_cache(InMemoryCache())
llm = ChatOpenAI(model="gpt-4", api_key=openai_api_key)

prompt = ChatPromptTemplate.from_template("[No prose] From the given list of professors: {professorOptions}, output the "
                                          "one you believe matches this professor: {professorInformation}."
                                          "Output your response as json with two fields: professorName (excluding "
                                          "honorifics such as Dr, Mrs, etc) and emailAddress. E.g. "
                                          "")
chain = prompt | llm
add_routes(
    app,
    chain,
    path="/linkprofessor",
    )


@app.middleware("http")
async def benchmark_middleware(request: Request, call_next):
    start_time = time.time()
    response = await call_next(request)
    process_time = time.time() - start_time
    # Here you can log the processing time, print it, or store it for later analysis
    print(f"Request: {request.url.path} took {process_time} seconds")
    return response

if __name__ == "__main__":
    import uvicorn
    uvicorn.run(app, host="localhost", port=8000)