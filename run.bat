 kubectl apply -f angular-service.yaml,master-api-service.yaml,redis-service.yaml,angular-deployment.yaml,master-api-deployment.yaml,redis-deployment.yaml,redis-claim0-persistentvolumeclaim.yaml

@REM kubectl apply -f angular-service.yaml,redis-service.yaml,angular-deployment.yaml,redis-deployment.yaml,redis-claim0-persistentvolumeclaim.yaml

@REM kubectl delete -f angular-service.yaml,redis-service.yaml,angular-deployment.yaml,redis-deployment.yaml,redis-claim0-persistentvolumeclaim.yaml



@REM  kubectl delete -f angular-service.yaml,master-api-service.yaml,redis-service.yaml,angular-deployment.yaml,master-api-deployment.yaml,redis-deployment.yaml,redis-claim0-persistentvolumeclaim.yaml

@REM kubectl get deployments
@REM kubectl get services