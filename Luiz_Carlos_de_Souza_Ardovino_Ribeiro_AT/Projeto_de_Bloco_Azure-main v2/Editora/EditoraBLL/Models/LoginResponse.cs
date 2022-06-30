namespace EditoraBLL.Models.Auth;

public record LoginReponse(string Token, DateTime Expiration, string Username);
