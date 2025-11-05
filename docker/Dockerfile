# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
# copy csproj riêng để cache restore tốt hơn (tùy chọn)
COPY ./src/SmuggleDemo/*.csproj ./SmuggleDemo/
RUN dotnet restore ./SmuggleDemo/SmuggleDemo.csproj

# Runtime dev (có SDK để dotnet watch)
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app

# Cài vsdbg để VS Code attach
RUN apt-get update && apt-get install -y curl unzip && rm -rf /var/lib/apt/lists/*
RUN mkdir -p /vsdbg && \
    curl -sSL https://aka.ms/getvsdbgsh -o /tmp/getvsdbg.sh && \
    bash /tmp/getvsdbg.sh -v latest -l /vsdbg

# Map source từ host vào /app/src (volume), không copy code tại build
# (inner-loop nhanh, sửa là có)
ENV ASPNETCORE_URLS=http://0.0.0.0:5000
EXPOSE 5000
# dotnet watch cho hot-reload; bạn có thể đổi thành "dotnet run" nếu muốn
CMD ["bash","-lc","dotnet watch --project /app/src/SmuggleDemo/SmuggleDemo.csproj run"]
