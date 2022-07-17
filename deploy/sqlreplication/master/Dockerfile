# https://github.com/elastic/elasticsearch-docker
FROM mcr.microsoft.com/mssql/server:2019-latest

USER root
RUN /opt/mssql/bin/mssql-conf traceflag 9481 on && \
    /opt/mssql/bin/mssql-conf traceflag 1234 on
USER mssql
RUN /opt/mssql/bin/mssql-conf set hadr.hadrenabled  1
RUN /opt/mssql/bin/mssql-conf set sqlagent.enabled true 
 
CMD /opt/mssql/bin/sqlservr

# docker exec -it container1 mkdir /var/opt/mssql/data/ReplData/