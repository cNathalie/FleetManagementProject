FROM node:alpine
ENV VITE_DOCKER=true
WORKDIR /srv/app
COPY frontend/package.json ./
COPY frontend/package-lock.json ./
RUN yarn
COPY frontend/. ./
EXPOSE 5173
CMD ["yarn", "dev"]