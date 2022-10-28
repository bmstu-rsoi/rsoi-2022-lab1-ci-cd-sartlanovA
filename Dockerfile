FROM mcr.microsoft.com/dotnet/aspnet:3.1-alpine

WORKDIR /app

COPY publish/ .

ENTRYPOINT ["dotnet", "rsoi_lr1.dll"]