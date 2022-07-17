#FROM mcr.microsoft.com/mssql/server:2019-latest
# USER root
# RUN /opt/mssql/bin/mssql-conf traceflag 9481 on && \
#     /opt/mssql/bin/mssql-conf traceflag 1234 on
# USER mssql
# RUN /opt/mssql/bin/mssql-conf set hadr.hadrenabled  1
# RUN /opt/mssql/bin/mssql-conf set sqlagent.enabled true 
 
# CMD /opt/mssql/bin/sqlservr

FROM mcr.microsoft.com/mssql/server:2019-latest
COPY . /
USER root
RUN  chmod +x /db-init.sh
CMD /bin/bash ./entrypoint.sh
RUN /opt/mssql/bin/mssql-conf set sqlagent.enabled true 








# DECLARE @distributor AS sysname
# DECLARE @distributorlogin AS sysname
# DECLARE @distributorpassword AS sysname
# -- Specify the Distributor name. Use 'hostname' command on in terminal to find the hostname
# SET @distributor = @@SERVERNAME--in this example, it will be the name of the publisher
# SET @distributorlogin = N'sa'
# SET @distributorpassword = N'Aa!012345679'
# -- Specify the distribution database. 

# use master
# exec sp_adddistributor @distributor = @distributor -- this should be the hostname

# -- Log into Distributor and create Distribution Database. In this example, our Publisher and Distributor is on the same host
# exec sp_adddistributiondb @database = N'distribution', @log_file_size = 2, @deletebatchsize_xact = 5000, @deletebatchsize_cmd = 2000, @security_mode = 0, @login = @distributorlogin, @password = @distributorpassword
# GO

# DECLARE @publisher AS sysname
# DECLARE @distributorlogin AS sysname
# DECLARE @distributorpassword AS sysname
# -- Specify the Distributor name. Use 'hostname' command on in terminal to find the hostname
# SET @publisher = @@SERVERNAME 
# SET @distributorlogin = N'sa'
# SET @distributorpassword = N'Aa!012345679'
# -- Specify the distribution database. 

# -- Adding the distribution publishers
# exec sp_adddistpublisher @publisher = @publisher, @distribution_db = N'distribution', @security_mode = 0, @login = @distributorlogin, @password = @distributorpassword, @working_directory = N'/var/opt/mssql/ReplData', @trusted = N'false', @thirdparty_flag = 0, @publisher_type = N'MSSQLSERVER'
# GO

# DECLARE @replicationdb AS sysname
# DECLARE @publisherlogin AS sysname
# DECLARE @publisherpassword AS sysname
# SET @replicationdb = N'MasterData'
# SET @publisherlogin = N'sa'
# SET @publisherpassword = N'Aa!012345679'