namespace BM_F1_WorkshopInventory.Models.DTO
{
    public class UserLogInDTO(string id, string username, string role, string token) : UserInfoDTO(id,username,role)
    {
        public string Token { get; set; } = token;
    }
}
