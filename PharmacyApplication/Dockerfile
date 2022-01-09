FROM jelenahrnjak/pharmacy:latest

RUN groupadd -g 1001 student && \
    useradd -r -u 1001 -g student  student 
USER student

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
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app .
ENTRYPOINT [ "dotnet","PharmacyAPI.dll" ]