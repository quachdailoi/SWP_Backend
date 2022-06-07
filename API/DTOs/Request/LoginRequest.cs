namespace API.DTOs.Request
{
    public class LoginRequest
    {
        public int CampusId { get; set; }
        public string IdToken { get; set; } = string.Empty;
    }
}
