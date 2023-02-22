### STAGE 1:BUILD ###
# Defining a node image to be used as giving it an alias of "build"
# Which version of Node image to use depends on project dependencies 
# This is needed to build and compile our code 
# while generating the docker image
# lấy thư viện node
FROM node:14.17.4  AS build
# Create a Virtual directory inside the docker image
# tạo thư mục ảo trong image
WORKDIR /dist/src/app
# Copy files to virtual directory
# COPY package.json package-lock.json ./
# Run command in Virtual directory
# chạy các câu lệnh
RUN npm cache clean --force
# Copy files from local machine to virtual directory in docker image
COPY . .
RUN npm update
RUN npm install
RUN npm run build --prod


# ### STAGE 2:RUN ###
# # Defining nginx image to be used
# FROM nginx:latest AS ngi
# # Copying compiled code and nginx config to different folder
# # NOTE: This path may change according to your project's output folder 
# #copy file vừa build sang thư mục ảo ngĩn
# COPY --from=build /dist/src/app/dist/WareHouse /usr/share/nginx/html
# COPY /nginx.conf  /etc/nginx/conf.d/default.conf
# # Exposing a port, here it means that inside the container 
# # the app will be using Port 80 while running
# EXPOSE 80