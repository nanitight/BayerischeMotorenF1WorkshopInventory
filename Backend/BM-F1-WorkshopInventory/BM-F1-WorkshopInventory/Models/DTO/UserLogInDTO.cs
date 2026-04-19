namespace BM_F1_WorkshopInventory.Models.DTO
{
    public class UserLogInDTO
    {
        public string Username { get; set; }
        public string Id { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public UserLogInDTO(string id, string username,string role,string token)
        {
            Username = username;
            Id = id;
            Role = role;
            Token = token;
        }
    }
}
