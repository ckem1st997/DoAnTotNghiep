# https://github.com/elastic/logstash-docker
FROM docker.elastic.co/logstash/logstash-oss:6.0.0

# Add your logstash plugins setup here
# Example: RUN logstash-plugin install logstash-filter-json
RUN logstash-plugin install logstash-input-http
RUN logstash-plugin install logstash-input-jdbc

USER root 

COPY mssql-jdbc-10.2.1.jre8.jar /usr/share/logstash/logstash-core/lib/jars/mssql-jdbc-10.2.1.jre8.jar
# COPY ../../sqljdbc_10.2/enu/mssql-jdbc-10.2.1.jre8.jar /usr/share/logstash/pipeline