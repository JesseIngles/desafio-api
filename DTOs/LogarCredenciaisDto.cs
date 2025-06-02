namespace api.DTOs
{
  public class LogarCredenciaisDto
  {
    public string EmailOrUsername { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = "usuario";  // padrão: usuario, pode ser "parceiro"
  }
}
