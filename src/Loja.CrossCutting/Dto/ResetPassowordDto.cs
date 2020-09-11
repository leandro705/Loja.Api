namespace Loja.CrossCutting.Dto
{
    public class ResetPassowordDto
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
