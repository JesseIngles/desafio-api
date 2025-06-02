using System.Threading.Tasks;
using api.DAL;
using api.DAL.Entities;
using api.DTOs;
using api.Helpers;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
  public class UsuarioService
  {
    private readonly AppDbContext _context;

    public UsuarioService(AppDbContext context)
    {
      _context = context;
    }

    public async Task<Result<string>> LogarAsync(LogarCredenciaisDto dto)
    {
      try
      {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u =>
                (u.Email == dto.EmailOrUsername || u.Nome == dto.EmailOrUsername || u.Telefone == dto.EmailOrUsername)
                && u.Verificado);

        if (usuario == null)
          return Result<string>.Error("Conta não encontrada ou não verificada");

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, usuario.SenhaHash))
          return Result<string>.Error("Credenciais inválidas.");

        // Apenas token usuário comum, para exemplo simples
        var token = JwtHelper.GerarTokenUsuario(usuario);

        return Result<string>.Success(token, "Logado usuário com sucesso");
      }
      catch (Exception e)
      {
        return Result<string>.Error($"Erro ao logar usuário: {e.Message}");
      }
    }
  }
}
