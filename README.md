### OVERVIEW

This is a React frontend, .NET Core Web API backend project that displays a list of products.
The 2 main things this project aims to showcase are the following:

1. show how to structure a .NET Core Web API using Clean Architecture
2. show how to implement a CICD pipeline using Github Actions, Docker, and Azure

The application is hosted in Azure:  

* the frontend is a static website stored in a blob storage  
* the backend is a containerized application hosted in an App Service.  

A CICD pipeline has also been implemented using Github Actions.
On push to the main branch, two workflows are triggered:

1. frontend.yml triggeres a build of the _client_ folder and subsequently pushes it to the web storage container
2. backend.yml triggers a build of the _server_ folder, pushes the built container to the container registry, and updates the App Service with the new container

A more detailed overview of each project can be found in the respective README.md inside the _client_ and _server_ folders.
