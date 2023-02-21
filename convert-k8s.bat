@REM curl -L https://github.com/kubernetes/kompose/releases/download/v1.26.0/kompose-windows-amd64.exe -o kompose.exe
@REM mkdir exportk8s
@REM kompose -v convert -f docker-compose.yml -o exportk8s

kompose -v convert -f docker-compose.yml -o k8s.yaml

@REM đường dẫn tới file đường dẫn tới chart chưa file
@REM helm install -f k8s-helm/master-api/values.yaml master-api k8s-helm/master-api
@REM helm upgrade master-api k8s-helm/master-api