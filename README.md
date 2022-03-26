### OVERVIEW

This is a React frontend, .NET Core Web API backend project that displays a list of products.
The 2 main things this project aims to showcase are the following:

1. show how to structure a .NET Core Web API using Clean Architecture
2. show how to implement a simple CICD pipeline using Github Actions, Docker, and Azure

The application is hosted in Azure:  

* the frontend is a static website stored in a blob storage  
* the backend is a containerized application hosted in an App Service.  

A CICD pipeline has also been implemented using Github Actions.
On push to the main branch, two workflows are triggered:

1. frontend.yml triggeres a build of the _client_ folder and subsequently pushes it to the web storage container
2. backend.yml triggers a build of the _server_ folder, pushes the built container to the container registry, and updates the App Service with the new container

A more detailed overview of each project can be found in the respective README.md inside the _client_ and _server_ folders.

### INSTRUCTIONS TO VALIDATE IF THE APP AND ITS CI/CD WORKFLOWS ARE WORKING

* in _client/src/App.js_, modify **line 19** into any string
* int _server/Web/Controllers/ProductsController.cs_, modify **line 35** into any string 

### THINGS TO BE DONE

1. need to create a rollback workflow (the CICD pipeline is very simple in that all it does is deploy the static frontend and container backend to Azure; if the deployment were to fail, there is no functionality to revert the said deployment to the previosu working version)
2. the .NET backend is not connected to any DB at the moment to cut down on costs but the code to access the DB is all set up and ready to go
3. there are only unit tests that run before a container is built and deployed
4. need to add automated testing that runs after a successful deployment to ensure that the app is working
