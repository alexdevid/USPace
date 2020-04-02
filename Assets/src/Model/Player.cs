using Network.DataTransfer.Security;

namespace Model
{
    public class Player
    {
        public int Id { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string Token { get; private set; }
        public string Flag { get; private set; }
        public string CreatedAt { get; private set; }
        
        public Player()
        {
            Id = 1;
            Username = "unknown";
        }

        public static Player CreateFromDTO(LoginResponse response)
        {
            return new Player
            {
                Username = response.username,
                Email = response.email,
                Token = response.token,
                Flag = response.flag,
                CreatedAt = response.created_at
            };
        }
    }
}