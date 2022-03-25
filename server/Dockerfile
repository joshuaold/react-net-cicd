FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /app

# copy sln and csproj files into the image
COPY *.sln .
COPY Core/*.csproj ./Core/
COPY Infrastructure/*.csproj ./Infrastructure/
COPY Unit.Tests/*.csproj ./Unit.Tests/
COPY Web/*.csproj ./Web/
# restore package dependencies for the solution
RUN dotnet restore

# copy full solution over
COPY . .

# build the solution
RUN dotnet build

# # run the unit tests
# FROM build AS test
# WORKDIR /app/Unit.Tests
# # tzdata needed
# RUN apk add --no-cache tzdata

# RUN dotnet test

# create a new layer from the build
FROM build AS publish

# set the working directory to be the web project
WORKDIR /app/Web

# publish the web api project to a directory called out
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS runtime

# tzdata needed
# RUN apk add --no-cache tzdata
WORKDIR /app
COPY --from=publish /app/Web/out ./

# # expose port 80,443
# EXPOSE 80
# EXPOSE 443

# # entity framework fix
# RUN apk add icu-libs
# ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# run the web app when the docker image is started
ENTRYPOINT ["dotnet", "Web.dll"]
