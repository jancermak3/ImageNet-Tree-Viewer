FROM node:14

WORKDIR /app

COPY package*.json ./

RUN npm install

COPY . .

ENV REACT_APP_API_BASE_URL=http://localhost:5248/api

EXPOSE 3000

CMD ["npm", "start"]