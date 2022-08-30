# MusicCloud

## The app where you can upload and download music 

### To run the app locally:

Create initial migrations files if thare are not any:

### `dotnet ef migrations add <Name> -p Data -s API`

Drop the database if necessary:

### `dotnet ef database drop -p Data -s API`

Go to API folder and run the app:

### `cd API && dotnet watch run`

Go to client-app folder and start the client:

### `cd ../client-app && npm i && npm start`