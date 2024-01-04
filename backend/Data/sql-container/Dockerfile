# Choose ubuntu version
FROM mcr.microsoft.com/mssql/server:2022-preview-ubuntu-22.04

# Create app directory
WORKDIR /usr/src/app

# Copy initialization scripts
COPY Data/sql-container/init-scripts/ .
             
# Set environment variables, not have to write them with the docker run command
# Note: make sure that your password matches what is in the run-initialization script 
ENV SA_PASSWORD=FleetManagement007
ENV ACCEPT_EULA=Y
ENV MSSQL_PID=Evaluation

# Expose port 1433
EXPOSE 1433

# Run Microsoft SQL Server and initialization script (at the same time)
USER mssql
ENTRYPOINT /bin/bash /usr/src/app/entrypoint.sh