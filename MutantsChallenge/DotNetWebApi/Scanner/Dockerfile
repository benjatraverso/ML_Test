FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Scanner.csproj", "Scanner/"]
#COPY ["Scanner.csproj", "Scanner/"]
RUN dotnet restore "Scanner/Scanner.csproj"
#RUN dotnet restore "Mutants/Scanner/Scanner.csproj"
COPY . .
#WORKDIR "/src/Scanner"
#RUN dotnet build "Scanner.csproj" -c Release -o /app

#FROM build AS publish
RUN dotnet publish "Scanner.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Scanner.dll"]
