Use ".travis_google.yml" file
Deploy to google cloud (kubernetes):
1) Go to console.cloud.google.com -> Kubernetes engine
2) Create a new project
3) Create a new cluster
4) Create a secret for mongo url in cluster
5) install Helm and Ingress-Nginx
6) Go to IAM -> Service accounts
   a) create a new service account
   b) get a new json key
   c) encrypt this key with travis locally on your computer
   d) add to git only encrypted file
7) Rename ".travis_google.yml" on ".travis.yml"
8) update projectId, zone and cluster in file
9) you should delete database-persistent-volume.yaml, and "storageclass: manual" from database-persistent-volume-claim.yaml
9) push changes to repository