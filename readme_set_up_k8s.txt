Add secrets to your k8s cluster:
for localhost: kubectl create secret generic mongo --from-literal name=username --from-literal password=password --from-literal url=mongodb://username:password@mongo-cluster-ip-service:27017

