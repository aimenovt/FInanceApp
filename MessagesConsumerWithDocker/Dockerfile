FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app
EXPOSE 81
EXPOSE 444

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MessagesConsumerWithDocker.csproj", "./"]
RUN dotnet restore "MessagesConsumerWithDocker.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "MessagesConsumerWithDocker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MessagesConsumerWithDocker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessagesConsumerWithDocker.dll"]