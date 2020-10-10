echo off
set migrationName=%1
dotnet tool install dotnet-ef
dotnet ef migrations add --project .\src\OmahaMtg\ --startup-project .\src\OmahaMTG.WebUi\ --context OmahaMtgDbContext --output-dir Infrastructure\Migrations %migrationName% -v 