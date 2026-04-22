namespace BM_F1_WorkshopInventory.Models.DTO
{
    public class UserLogInDTO : UserInfoDTO
    {
       
        public string Token { get; set; }
        public UserLogInDTO(string id, string username,string role,string token) : base(id,username,role)
        {
            Token = token;
        }
    }
}
