# Use Linux-based .NET 8 runtime as base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use .NET 8 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Ecommerce.Web/Ecommerce.Web.csproj", "Ecommerce.Web/"]
RUN dotnet restore "Ecommerce.Web/Ecommerce.Web.csproj"

# Copy and build
COPY . .
WORKDIR "/src/Ecommerce.Web"
RUN dotnet build "Ecommerce.Web.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Ecommerce.Web.csproj" -c Release -o /app/publish

# Use Linux runtime for the final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Web.dll"]
