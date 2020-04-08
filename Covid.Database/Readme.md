# DOCKER COMMAND TO CREATE DB :

docker run --name covid-db -e POSTGRES_PASSWORD=postgres -p 6000:5432 -d postgres

# CONFIG.JSON

this file must have his property 'Copy to output directory' set to 'Always copy'

# HOW TO SEND EF COMMANDS ON OSX ENV :

open project contextual menu,
go to 'Tools', then 'Open in Terminal'
send your EF commands in the opened terminal

# EF commands reminder

Create the DB => dotnet ef migrations add CovidDB
Update DB => dotnet ef database update
Add a new Migration => dotnet ef migrations add <migration_name>
RollBack => dotnet ef database update <migration_name>