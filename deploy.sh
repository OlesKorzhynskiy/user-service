docker build -t korzhynskiy/gateway:latest -t korzhynskiy/gateway:$GIT_SHA -f ./Gateway/Dockerfile ./gateway
docker build -t korzhynskiy/user-service-command:latest -t korzhynskiy/user-service-command:$GIT_SHA -f ./UserService.Command/Dockerfile ./gateway
docker build -t korzhynskiy/user-service-query:latest -t korzhynskiy/user-service-query:$GIT_SHA -f ./UserService.Query/Dockerfile ./gateway


docker push korzhynskiy/gateway:latest
docker push korzhynskiy/user-service-command:latest
docker push korzhynskiy/user-service-query:latest

docker push korzhynskiy/gateway:$GIT_SHA
docker push korzhynskiy/user-service-command:$GIT_SHA
docker push korzhynskiy/user-service-query:$GIT_SHA

kubectl apply -f k8s

kubectl set image deployments/gateway-deployment gateway=korzhynskiy/gateway:$GIT_SHA
kubectl set image deployments/command-deployment command=korzhynskiy/user-service-command:$GIT_SHA
kubectl set image deployments/query-deployment query=korzhynskiy/user-service-query:$GIT_SHA