FROM node:16.10.0-slim

COPY . ./

ARG NODE_ENV=production
ENV NODE_ENV $NODE_ENV

ARG PORT=3000
ENV PORT $PORT
EXPOSE $PORT 9229 9230

RUN npm ci && npm cache clean --force

USER node
WORKDIR /opt/westernflow/client

ENV PATH /opt/node_app/node_modules/.bin:$PATH

WORKDIR /opt/node_app/app
COPY --chown=node:node . .

CMD ["npm", "start"]
