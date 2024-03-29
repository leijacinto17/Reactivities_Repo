# Getting Started with Create React App

This project was bootstrapped with [Create React App](https://github.com/facebook/create-react-app).

## Available Scripts

In the project directory, you can run:

### `npm start`

Runs the app in the development mode.\
Open [http://localhost:3000](http://localhost:3000) to view it in the browser.

The page will reload if you make edits.\
You will also see any lint errors in the console.

### `npm test`

Launches the test runner in the interactive watch mode.\
See the section about [running tests](https://facebook.github.io/create-react-app/docs/running-tests) for more information.

### `npm run build`

Builds the app for production to the `build` folder.\
It correctly bundles React in production mode and optimizes the build for the best performance.

The build is minified and the filenames include the hashes.\
Your app is ready to be deployed!

See the section about [deployment](https://facebook.github.io/create-react-app/docs/deployment) for more information.

### `npm run eject`

**Note: this is a one-way operation. Once you `eject`, you can’t go back!**

If you aren’t satisfied with the build tool and configuration choices, you can `eject` at any time. This command will remove the single build dependency from your project.

Instead, it will copy all the configuration files and the transitive dependencies (webpack, Babel, ESLint, etc) right into your project so you have full control over them. All of the commands except `eject` will still work, but they will point to the copied scripts so you can tweak them. At this point you’re on your own.

You don’t have to ever use `eject`. The curated feature set is suitable for small and middle deployments, and you shouldn’t feel obligated to use this feature. However we understand that this tool wouldn’t be useful if you couldn’t customize it when you are ready for it.

## Learn More

You can learn more in the [Create React App documentation](https://facebook.github.io/create-react-app/docs/getting-started).

To learn React, check out the [React documentation](https://reactjs.org/).



## yarn add semantic-ui-react semantic-ui-css
## yarn add uuid (if using GUID)
## npm i --save-dev @types/uuid (if using typescript)
## npm i --save-dev @types/babel (if using typescript)
## yarn add @types/node --dev (if using typescript)
## yarn add @types/node --dev (if using typescript)
## yarn add mobx mobx-react-lite (state organizer)
## yarn add react-router-dom (to navigate diffferent page)
## yarn add @types/react-calendar (if using typescript)
## yarn add react-toastify (toast message)
## yarn add formik (Forms)
## yarn add yup (formik  validation )
## yarn add @types/yup --save-dev (if using typescript)
## yarn add axios (to connect in api)
## yarn add react-dropzone (for handling upload image or files)
## npm install react-cropper (for crop image after drag)

## npm install @microsoft/signalr (add signalr)

## yarn add react-infinite-scroller (add infinite scroll)
## yarn add @types/react-infinite-scroller (add infinite scroll)

## After installing react-datepicker
## install @types/react-datepicker
## install date-fns (to format date)


## Need to polish (automatic changing of profile in Activities, infinite loop when theres no activities)


## it is okay to use SQL Server but when deploying in HEROKU SQL server is not free so for this training we are using POSTRESQL since HEROKU provides free for this DB


## creating POSTGRE in docker
## docker run --name dev -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=1nF0b@hn -p 5432:5432 -d postgres:latest


## deploying in docker container 
## docker build -t leijacinto17/reactivities .
## note leijacinto17 is the username in docker reactivities would be the repo name

## after successfully deploy try to run the image
## docker run --rm -it -p 8080:80 leijacinto17/reactivities

## push to docker repo
## docker login
## docker push leijacinto17/reactivities:latest

## if deploying in fly.io
## install first the flyio using this command in powershell
## powershell -Command iwr https://fly.io/install.ps1 -useb | iex

## after installation

## flyctl auth login (this is to login your account in fly io)

## flyctl launch --image leijacinto17/reactivities:latest

## after the command success

## go to fly.toml modify some config there specially the environment variables

## flyctl auth token (to get the fly api token)

## store sensitive variable 
## flyctl secrets set (name of variable)
## flyctl secrets set Facebook__ApiSecret=bf0d1b481ff483fb1a3b435fd22b70e9


## add facebook login
## yarn add @greatsumini/react-facebook-login

## make localhost to https
## choco install mkcert
## mkcert localhost



## run dotnet api dotnet watch --no-hot-reload