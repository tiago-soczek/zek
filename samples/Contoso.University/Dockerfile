FROM microsoft/dotnet:2.2-aspnetcore-runtime

WORKDIR /app

COPY ./api .

ENV ASPNETCORE_URLS="http://+:8080"

EXPOSE 8080/tcp

ENTRYPOINT ["dotnet", "Contoso.University.dll"]