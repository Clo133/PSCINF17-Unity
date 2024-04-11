import openai 
import time
import sys


#client = openai.OpenAI(api_key='INSEREZ VOTRE CLE API')
client = openai.OpenAI(api_key='')

assistant = client.beta.assistants.create(
    name="Game assistant", 
    instructions="I'll describe below a programming language. Your goal is, given an order in natural language,  to transform it using the programming language below : \
        You can use several instructions, on the same line \
        Objects : \
    - LABEL({s : string}) := an object refferd by its label e.g. LABEL('A') \
    You can use these labels: 'FRIEND', 'NORTH_GARDEN', 'SOUTH_GARDEN', 'BUTTON', 'VIOLET_PYRAMID'.  \
    Labels in the scene : \
    'FRIEND', 'NORTH_GARDEN', 'SOUTH_GARDEN', 'BUTTON', 'VIOLET_PYRAMID' are labels \
    Instructions : \
    - GOTO('LABEL') \
    - LEFT() := to go to the left \
    - LEFT({x : integer}) := to walk x steps to the left \
    - RIGHT() := to go to the right \
    - RIGHT({x : integer}) := to walk x steps to the right \
    - UP() := to go up \
    - UP({x : integer}) := to walk x steps up \
    - DOWN() := to go down \
    - DOWN({x : integer}) := to walk x steps down \
    - IDLE() := if the given order if none of the above \
    You must respond with ONLY one of these instructions, no other sentences, no introduction, no other word. \
        for example answer 'RIGHT(3) ; UP(4); GOTO('FRIEND')'",
    tools=[{"type": "code_interpreter"}],
    model="gpt-3.5-turbo",
)

thread = client.beta.threads.create()

message = client.beta.threads.messages.create(
    thread_id=thread.id,
    role="user",
    content= sys.argv[1],
)

run = client.beta.threads.runs.create(
    thread_id=thread.id,
    assistant_id=assistant.id,
    instructions="",
)


while True:
    run = client.beta.threads.runs.retrieve(thread_id=thread.id, run_id=run.id)

    if run.status == "completed":
        messages = client.beta.threads.messages.list(thread_id=thread.id)

        for message in messages:
            assert message.content[0].type == "text"
            if message.role =='assistant':
                print(message.content[0].text.value)
            break
    
        client.beta.assistants.delete(assistant.id)

        break
    else:
        time.sleep(1)

