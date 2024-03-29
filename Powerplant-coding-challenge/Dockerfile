#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

#FROM mcr.microsoft.com/dotnet/core/aspnet
#LABEL author=”Name”
#ENV ASPNETCORE_URLS=http://*:5000
#ENV ASPNETCORE_ENVIRONMENT=”production”

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
#ENV ASPNETCORE_URLS=http://*:8080
EXPOSE 80
EXPOSE 443
EXPOSE 8888

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Powerplant-coding-challenge/Powerplant-coding-challenge.csproj", "Powerplant-coding-challenge/"]
RUN dotnet restore "Powerplant-coding-challenge/Powerplant-coding-challenge.csproj"
COPY . .
WORKDIR "/src/Powerplant-coding-challenge"
RUN dotnet build "Powerplant-coding-challenge.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Powerplant-coding-challenge.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Powerplant-coding-challenge.dll"]