@REM curl -L https://github.com/kubernetes/kompose/releases/download/v1.26.0/kompose-windows-amd64.exe -o kompose.exe
@REM mkdir exportk8s
@REM kompose -v convert -f docker-compose.yml -o exportk8s

kompose -v convert -f docker-compose.yml -o k8s.yaml

@REM đường dẫn tới file đường dẫn tới chart chưa file
@REM helm install -f k8s-helm/master-api/values.yaml master-api k8s-helm/master-api
@REM helm upgrade master-api k8s-helm/master-api
@REM kubectl get events
@REM kubectl get pods
@REM kubectl describe pod  master-api-7b69f6dfd7-t294j
@REM kubectl get svc,po,deploy
@REM helm uninstall master-api
@REM helm upgrade wh-api k8s-helm/wh-api
@REM helm install -f k8s-helm/wh-api/values.yaml wh-api k8s-helm/wh-api 

@REM co the trien khai nhieu container trong cung mot pod, co ma khong nen vi kho trien khai
@REM vi microservie la tach biet
@REM giao tiep giua cac service dung tenservice:port cua no nhu docker compose, quan trong la cai ten va port