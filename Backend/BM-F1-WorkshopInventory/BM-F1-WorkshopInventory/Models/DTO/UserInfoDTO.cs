namespace BM_F1_WorkshopInventory.Models.DTO
{
    public class UserInfoDTO
    {
        public string Username { get; set; }
        public string Id { get; set; }
        public string Role { get; set; }

        public UserInfoDTO(string id, string username,  string role)
        {
            Username = username;
            Id = id;
            Role = role;
        }
    }
}
