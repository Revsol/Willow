FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
# WORKDIR /src
COPY ["Client/Client.csproj", "Client/"]
COPY ["Server.TransportModels/Server.TransportModels.csproj", "Server.TransportModels/"]
RUN dotnet restore "Client/Client.csproj"
COPY . .
WORKDIR "/Client"
RUN dotnet build "Client.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Client.csproj" -c Release -r linux-arm -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Client.dll"]