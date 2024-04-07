from langchain.schema import SystemMessage, HumanMessage
from langchain.schema.runnable import RunnableMap
from langchain_core.prompts import ChatPromptTemplate
from langserve import RemoteRunnable
import asyncio
import httpx
import psycopg2


async def main():
    async with httpx.AsyncClient() as client:
        openai = RemoteRunnable("http://localhost:8000/openai/")
        joke_chain = RemoteRunnable("http://localhost:8000/joke/")

        print(joke_chain.invoke({"topic": "parrots"}))
        print(await joke_chain.ainvoke({"topic": "Professors"}))

        prompt = [
            SystemMessage(content='Act like either a cat or a parrot.'),
            HumanMessage(content='Hello!')
        ]

        async for msg in openai.astream(prompt):
            print(msg, end="", flush=True)

        prompt = ChatPromptTemplate.from_messages(
            [("system", "Tell me a long story about {topic}")]
        )

        # Can define custom chains
        chain = prompt | RunnableMap({
            "openai": openai,
        })

        chain.batch([{"topic": "parrots"}, {"topic": "cats"}])


if __name__ == "__main__":
    asyncio.run(main())