# Build Stage

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /Src

COPY . ./
RUN dotnet restore 

RUN dotnet publish -c release -o out

# Serve Stage

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /Src
EXPOSE 5161
COPY --from=build /Src/out .
ENV IS_DOCKER=1

ENTRYPOINT [ "dotnet", "achei-backEnd.dll" ]
