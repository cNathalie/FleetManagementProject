FROM node:alpine
ENV VITE_DOCKER true
WORKDIR /srv/app
COPY package.json .
COPY package-lock.json .
RUN yarn
COPY . .
EXPOSE 5173
CMD ["yarn", "dev"]