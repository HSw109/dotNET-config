FROM debian:bookworm-slim

RUN apt-get update && apt-get install -y --no-install-recommends curl ca-certificates bash libc6 libssl3 libicu72 && rm -rf /var/lib/apt/lists/*

ENV DOTNET_ROOT=/usr/share/dotnet DOTNET_ROLL_FORWARD=Disable ASPNETCORE_URLS=http://0.0.0.0:5000
ENV PATH="$PATH:/usr/share/dotnet"

RUN curl -sSL https://dot.net/v1/dotnet-install.sh -o /tmp/dotnet-install.sh && chmod +x /tmp/dotnet-install.sh \
 && /tmp/dotnet-install.sh --install-dir "$DOTNET_ROOT" --version 8.0.100 \
 && /tmp/dotnet-install.sh --install-dir "$DOTNET_ROOT" --runtime aspnetcore --version 8.0.20

WORKDIR /app
COPY ./src/SmuggleDemo/*.csproj ./SmuggleDemo/
RUN dotnet restore ./SmuggleDemo/SmuggleDemo.csproj

RUN mkdir -p /vsdbg && \
    curl -sSL https://aka.ms/getvsdbgsh -o /tmp/getvsdbg.sh && \
    bash /tmp/getvsdbg.sh -v latest -l /vsdbg

EXPOSE 5000
CMD ["dotnet", "watch", "--project" ,"/app/src/SmuggleDemo/SmuggleDemo.csproj", "run"]
# CMD ["bash","-lc","sleep infinity"] 
