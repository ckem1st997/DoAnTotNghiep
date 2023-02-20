curl -L https://github.com/kubernetes/kompose/releases/download/v1.26.0/kompose-windows-amd64.exe -o kompose.exe
mkdir exportk8s
kompose -v convert -f docker-compose.yml -o exportk8s