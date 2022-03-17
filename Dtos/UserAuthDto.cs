namespace ApiGetwaySalesOrder.Dtos
{
    public class UserAuthDto
    {
        //     private String id;
        // private String email;
        // private String password;
        // private String token;

        public UserAuthDto() {}


        public UserAuthDto(string? id = null, string? email = null, string? password = null, string? token = null) {
            this.Id = id;
            this.Email = email;
            this.password = password;
            this.token = token;
        }

        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? password { get; set; }
        public string? token { get; set; }


    }
}