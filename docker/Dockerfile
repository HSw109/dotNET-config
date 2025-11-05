FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ./src/SmuggleDemo/*.csproj ./SmuggleDemo/
RUN dotnet restore ./SmuggleDemo/SmuggleDemo.csproj

FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app

RUN apt-get update && apt-get install -y curl unzip && rm -rf /var/lib/apt/lists/*  
RUN mkdir -p /vsdbg && \
    curl -sSL https://aka.ms/getvsdbgsh -o /tmp/getvsdbg.sh && \
    bash /tmp/getvsdbg.sh -v latest -l /vsdbg

ENV ASPNETCORE_URLS=http://0.0.0.0:5000
EXPOSE 5000

CMD ["bash","-lc","dotnet watch --project /app/src/SmuggleDemo/SmuggleDemo.csproj run"]
