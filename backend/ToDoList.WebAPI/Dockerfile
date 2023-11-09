FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ToDoList.WebAPI/ToDoList.WebAPI.csproj", "ToDoList.WebAPI/"]
RUN dotnet restore "ToDoList.WebAPI/ToDoList.WebAPI.csproj"
COPY . .
WORKDIR "/src/ToDoList.WebAPI"
RUN dotnet build "ToDoList.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDoList.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoList.WebAPI.dll"]
