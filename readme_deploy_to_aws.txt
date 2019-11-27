Deploy to aws:
1) create a new user in IAM service. Get access key and secret. Use them in travis file
2) create a new security group in VPC service. Add inbound rule for TCP protocol. Allow everybody from this new security group
3) create a new key pair in EC2. Get a key (.pem file). With PuttyGen program convert this file to private key .ppk (with RSA algorithm)
4) create a new application and environment in ElasticBeanstalk
  a) Use multidocker
  b) Use bigger capacity t2-large
  c) Use VPC from the beginning with new security group (from step 2) (you won't be able to add it after the creation of application)
  d) Use key pair created on step 3
  e) Add new environment variable for connection to db (after creating db in step 5)
  f) Update travis file with s3 bucket (create if it doesn't exist) and app/env from elasticbeanstalk
5) create a db in AWS DocumentDb service
  a) Do not use tls, in order not to generate a certificate (by adding a different parameter group and assigning it to cluster)
  b) Add to the security group from step 2

In order to test everything you can use PuTTy. Connect to you ec2 instance (setting name, public dns host and add key (generated in step 3) to SHH/Security)
There you should download mongo and connect to remote cluster (all credentials from AWS Document db cluster)
You can't connect from your local machine as it's available only there
Also, make sure everything is in the same vpc and have the same security group (with inbound rule for allowing communicate all services in this securtiy group)