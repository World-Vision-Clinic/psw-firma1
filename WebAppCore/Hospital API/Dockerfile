FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 1025

RUN groupadd -g 1001 student && useradd -r -u 1001 -g student  student 
USER student

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Hospital API/Hospital API.csproj", "Hospital API/"]
COPY ["Hospital/Hospital.csproj", "Hospital/"]
RUN dotnet restore "Hospital API/Hospital API.csproj"
COPY . .
WORKDIR "/src/Hospital API"
RUN dotnet build "Hospital API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Hospital API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hospital API.dll"]
