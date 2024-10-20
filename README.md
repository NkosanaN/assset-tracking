# asset-tracker #
A mining project that is used to track assets transfers.

###  Pre-requisite  ###
 * clone the repos
  * ``` git clone https://github.com/NkosanaN/assset-tracking.git ```
  * ``` git clone https://github.com/NkosanaN/IMSystemUI.git ```
  * Docker Desktop with Kubernetes being enabled
  * optional use Minicube
  * optional install K9s 


### To spin up the backend api project Run with Docker Compose ###
 * Run docker compose cmd at the root folder of backend project it will create sqlserver + api
 * C:\dev\backend> ```` docker compose up --build ````
 * ```` docker compose up --build ````
 * give it some time to spin-up containers
 * After navigate to swagger UI
 * * ```http://localhost:8000/swagger/index.html```
 * start the front end application with your favorite editor vscode or visual studio, as is not containerized yet

### To spin up the backend api project Run with Helm ###
 * ```` helm install sql-server .\Deployments\helm\sqlserver\ ````
 * which will create sqlserver running in Kubernetes, accessing sqlserver port forward        1433:1433
 * * ```kubectl port-forward sql-server 1433:1433``` 
 * ```` helm install asset-tracking .\Deployments\helm\asset-management\ ````
 * This cmd will spin-up the API ,accessing API port forward 8083:80 strictly
 * * ```kubectl port-forward asset-tracking 8083:80```




 ### API Flow ###
 * Import the postman collection which also includes Environments Variable
 * Register User
 * Login
 * Create a Shelf 
 * Hit Get All Users Endpoint to get users Id 
 * Create Item grap ShelfId + Users Id as they're required fields 
 * Create Item Transfer using Existing Item Id plus Users Id

