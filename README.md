# Martian Robots

## How the project has been developed

After read de documentation provided the firts thing done has been define what kind of application is wanted to get. 
At this point, it has been chosen to develop a ASP.NET CORE 3.1 Web API. 
Due to the time of developing somethings are get straightforward.

Considerations taking before start the developing process:

1. Visual Studio 2019 has been selected as principal IDE. 
2. Entity Framework is the ORM selected to boost the database integration and persistance data. 
3. Non Relational DB has been refused because of data types. As we want to keep in mind that this app could be increase, main data models (robot, sensors, grid position, info from the grid, astronauts in mars...) could have a relationship between each others.
4. The web app has 3 principal classes, Grid, Modules and Robots. Grid is the "plantet" divided by a numbers of modules. For example, 1 grid of 2x2 has 6 module 1(1-N relation). A robot explore one grid and has a relation with module (1-1) but not all modules have a robot.
5. The main purporse has been to make 2 ways of use de app. 1 to explote CRUD calls using "Info" endpoints and another whitch use "Robot" endpoint to modify the model.
6. At the end of developing, info endpoints are set to develop them. The unique endpoint full useful is "martianrobots/deploy" whitch handle the input requeriment.

## Project

#### Structure

```bash
├───src
│   ├───Controllers         # Endoints
│   ├───Database            # Database and model
│   │   ├───Contexts
│   │   │   └───Configurations
│   │   ├───Entities
│   │   └───Repositories
│   │       └───Base
│   ├───Diagrams
│   ├───Helpers             # Logic
│   │   ├───Commands
│   │   ├───Engines
│   │   └───Receiver
│   ├───Migrations
│   ├───Models
│   │   └───Grids
│   ├───Properties
│   ├───Servicies           # Logic
│   └───Shared
│       └───Interfaces
│           ├───Commands
│           └───Servicies
└───test
    ├───Engines

```


#### Endpoints

API works with the following structure:

- Info (not implemented): 
        - Get (id)
        - Get (x,y)
        - GetAll()
        - GetByPlanet(id)
        - GetDanger (danger)
This endpoints just consume data generated by deploy endpoints.
- Deploy
        - DeployRobot(robot): deploy a robot with the input requeriment format

Endpoints can be found inside controller. 
#### Model

Using the clean architecture layer the model was defined using domain driven design and code first with EF Core.

The model is inside Database/Entities folder, and Repositorie and context are folders with the classes to make EF work correctly.

The model is generated taking care of multiples Planets (Plante Enum), more instructions to be added (Intruction Enum) and future extensions.

#### Logic

The the web app core follows the command pattern inside Helpers folder.

When the endopoint controller is posted, tha controller use RobotService to perform model update. 
Then, there are 3 steps, grid creation, robot first deploy and movements.
These 3 steps are represented by 3 commands inside commands folder, 
Engines use this created commands reading the path given in the input and pass it to the specific receiver for each single action.

#### Test

Test project is inside test folder. There the unit tast made were created with XUnit framework and Fluent assertions.

More unit test cases need to be added for example to perform correct insertion in the databse with an in memory database to test. 

The unit test created test the principal cases of the input requirements. 

#### More things to add

The main challenge has been to create a extensible app, using dependency injection, repository, singleton, tdd, single-responsability, segregation, etc patterns. 

At this point, I would like to extend the app ending the Getters endpoints, adding the posibility of a robot "restart" from the finish endpoint, docker integration, microservicies...

One point to work would have be to segregate the base repository to give more flexilibity to CRUD operation extending the current base class in new ones and making base class abstract.

# Running the web app

To run the app, firstly clone this repo with visual studio.

1. Clone this repo with visual studio.
2. Create a database, by default this app use a database called **test**. 
    
    - Connection string is in appsetting.Development.json
    - It's essential to have SSMS and SQL installed. 

3. Check package are installed.
4. Run migrations and model creation using the command: *"Update-Database"* in the package manager console or *"dotnet ef database update"*  in normal terminal.
![Tables](assets/images/ass2.png)
5. Run the app with MartianRobot Debug Profile.
6. Test using Postman passing the input requirements. Postman use to give problems with HTTPS connection so I recommend to use http port (see the image below).


![Example with postman](assets/images/ass1.png "Postman")

![Module Table result](assets/images/ass3.png)
![Grid Table result](assets/images/ass4.png)
![Robot Table result](assets/images/ass5.png)

