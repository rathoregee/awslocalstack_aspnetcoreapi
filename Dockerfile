#FROM mcr.microsoft.com/dotnet/core/sdk:latest
ARG IMAGE_DOCKER_DOTNET_SCANNER
FROM $IMAGE_DOCKER_DOTNET_SCANNER AS platform-client-core-func-test-runner

ARG BUILDBRANCH
ARG BUILD_NUMBER
ARG NUGET_USERNAME
ARG NUGET_PASSWORD

ENV PATH="$PATH:/root/.dotnet/tools"

RUN mkdir /app
RUN mkdir /project

WORKDIR /project

ENV MSBUILDSINGLELOADCONTEXT=1
COPY  ./NuGet.config ./NuGet.config
COPY  ./src/platform-client-core-func/platform-client-core-func.csproj ./src/platform-client-core-func/platform-client-core-func.csproj
RUN dotnet restore ./src/platform-client-core-func/platform-client-core-func.csproj

COPY ./test/platform-client-core-func.Tests/platform-client-core-func.Tests.csproj ./test/platform-client-core-func.Tests/platform-client-core-func.Tests.csproj 
RUN dotnet restore ./test/platform-client-core-func.Tests/platform-client-core-func.Tests.csproj

COPY . .

RUN chmod +x ./entrypoint.sh

ENV EXTENT_REPORT_MODE=true
ENV CURRENT_ENV=development
ENV DB_CONNECTION_STRING "host=postgres_db;port=5432;username=user;password=mysecretpassword;database=clientdb"
ENV AWS_SERVICE_URL "http://localstack:4566"
ENV EVENT_BUS_MAIN_QUEUE_URL "http://localstack:4566/000000000000/platform-eventbus-main-queue"
RUN echo "List ARGs ${EXTENT_REPORT_MODE}, ${CURRENT_ENV}"
