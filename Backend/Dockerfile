# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy solution and projects
COPY Backend.sln ./
COPY Models/Models.csproj ./Models/
COPY Services/Services.csproj ./Services/
COPY Apis/Apis.csproj ./Apis/
COPY Tester/Tester.csproj ./Tester/

# Restore dependencies
RUN dotnet restore

# Copy all files and build the solution
COPY . ./
RUN dotnet publish Apis/Apis.csproj -c Release -o /out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app

# Copy build output from the build stage
COPY --from=build-env /out .

# Expose the necessary ports
EXPOSE 80
EXPOSE 443

# Set the entry point to the API project
ENTRYPOINT ["dotnet", "Apis.dll"]
