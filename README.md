# edmi-test
EDMI Initial test for recruitment

## Deployment instructions

Create SQL database

Execute **Script.sql**

Change connection string in appsettings.json

Open solution with Visual Studio

### Angular web app

Right-click on **EdmiTest.Frontend** project and click Publish

At the next screen click Publish

Copy content from publish folder to IIS folder

Open IIS folder from browser

### Console app

Right-click on **EdmiTest.ConsoleApp** project and click Publish

At the next screen click Publish

Go to publish folder and execute ***dotnet EdmiTest.ConsoleApp.dll***
