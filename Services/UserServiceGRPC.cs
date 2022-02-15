using Grpc.Core;
using Grpc.Net.Client;
using ApiGetwaySalesOrder.Dtos;

namespace ApiGetwaySalesOrder.Services
{
    public class UserServiceGRPC
    {
        //Local PORT 
        private int PORT = 50051;

        //container PORT 
        //private int PORT = 9090;

        // use from local to docker container without compose
        //private String SERVICEURL = "http://127.0.0.1:";
        //private String SERVICEURL = "172.17.0.6";
        // use from container to docker container without compose
        //private String SERVICEURL = "salesusernode";
        // use from container to docker container with compose
        //private String SERVICEURL = "user-api";
        // use for service kubernetes
        private String SERVICEURL = "apiusergrpc";


        public async Task<SalesUserApi.User> GetByEmailAndPassword(UserEmailPasswordDto dto)
        {
            var url = SERVICEURL + PORT;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
            var client = new SalesUserApi.UserService.UserServiceClient(channel);

            SalesUserApi.UserEmailPassword request = new SalesUserApi.UserEmailPassword();
            request.Email = dto.Email;
            request.Password = dto.Password;

            var reply = await client.FindByEmailAndPasswordAsync(request);

            return Task.FromResult(reply).Result;

        }


        public async Task<List<SalesUserApi.User>> GetUsers(SalesUserApi.Empty request)
        {
            var url = SERVICEURL + PORT;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
            var client = new SalesUserApi.UserService.UserServiceClient(channel);

            var reply = await client.GetAllAsync(request);
            var usersResult = reply.Users;
            List<SalesUserApi.User> users = new List<SalesUserApi.User>();

            foreach (var userResult in usersResult)
            {
                SalesUserApi.User user = new SalesUserApi.User();
                user.Id = userResult.Id;
                user.Name = userResult.Name;
                user.Email = userResult.Email;
                user.Password = userResult.Password;
                user.Token = userResult.Token;
                user.UserType = userResult.UserType;
                user.Status = userResult.Status;
                users.Add(user);
            }

            return Task.FromResult(users).Result;

        }


        public async Task<SalesUserApi.User> Get(SalesUserApi.UserRequestId request)
        {
            var url = SERVICEURL + PORT;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
            var client = new SalesUserApi.UserService.UserServiceClient(channel);

            var reply = await client.GetAsync(request);

            SalesUserApi.User user = new SalesUserApi.User();
            user.Id = reply.Id;
            user.Name = reply.Name;
            user.Email = reply.Email;
            user.Password = reply.Password;
            user.Token = reply.Token;
            user.UserType = reply.UserType;
            user.Status = reply.Status;

            return Task.FromResult(user).Result;

        }


        public UserCreateDto Create(UserCreateDto dto)
        {
            var url = SERVICEURL + PORT;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
            var client = new SalesUserApi.UserService.UserServiceClient(channel);

            SalesUserApi.User request = new SalesUserApi.User();
            request.Name = dto.name;
            request.Email = dto.Email;
            request.Password = dto.Password;
            request.UserType = dto.UserType;
            request.Status = dto.Status;

            var reply = client.Create(request);
            dto.Id = reply.Id;

            return Task.FromResult(dto).Result;
        }


        public UserUpdateDto Update(UserUpdateDto dto)
        {
            var url = SERVICEURL + PORT;
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(url, new GrpcChannelOptions { Credentials = ChannelCredentials.Insecure });
            var client = new SalesUserApi.UserService.UserServiceClient(channel);

            SalesUserApi.User request = new SalesUserApi.User();
            request.Id = dto.Id;
            request.Name = dto.name;
            request.Email = dto.Email;
            request.Password = dto.Password;
            request.UserType = dto.UserType;
            request.Status = dto.Status;

            var reply =  client.Update(request);

            return Task.FromResult(dto).Result;

        }



    }
}