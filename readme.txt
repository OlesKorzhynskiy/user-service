UserService.Query and UserService.Query.Grpc are used for getting data from db (should be used either of them)

Gateway should not be so complicated. It's better to use ordinary http client and send requests. (without using adapters)
We can use "https://github.com/Pathoschild/FluentHttpClient" or another (where we can redirect by setting up in json file)

It's not obligatory to share models. We can just duplicate classes and use only those fields that we need

UserService.Query.Grpc:
Sharing .proto files:
1) create a new project and sharing it (or by using nuget packages)
2) add it as gRPC service reference:
      a) right click on Dependencies (in UserService.Adapter)
      b) Add connected service -> Service References -> gRPC