FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base

RUN groupadd -g 999 appuser && \
    useradd -u 999 -g appuser appuser
USER appuser:appuser
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8081
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build-env

WORKDIR /src
COPY PharmacyApplication.sln ./
COPY Pharmacy/*.csproj ./Pharmacy/
COPY PharmacyApplication/*.csproj ./PharmacyApplication/
COPY PharmacyApplicationTests/PharmacyApplicationTests.csproj ./PharmacyApplicationTests/
RUN dotnet restore 
COPY . ./ 
WORKDIR /src/Pharmacy
RUN dotnet build -c Release -o /app 
WORKDIR /src/PharmacyApplication
RUN dotnet build -c Release -o /app

FROM build-env as publish
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
USER appuser
WORKDIR /app
COPY --from=build-env /app .
ENTRYPOINT [ "dotnet","PharmacyAPI.dll" ]