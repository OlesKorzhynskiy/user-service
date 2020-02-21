UserService.Query and UserService.Query.Grpc are used for getting data from db (should be used either of them)

UserService.Query.Grpc:
We can share .proto files by creating a new project and sharing it
But it's also possible to add it as gRPC service reference:
1) right click on Dependencies (in UserService.Adapter)
2) Add connected service -> Service References -> gRPC