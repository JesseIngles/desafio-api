using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.DAL.Entities;

[Table("usuario")]
public class TbUsuario
{
    [Column("id")]
    public int Id { get; set; }

    [Column("nome")]
    public string Nome { get; set; } = null!;

    [Column("email")]
    public string Email { get; set; } = null!;

    [Column("senhahash")]
    public string SenhaHash { get; set; } = null!;

    [Column("telefone")]
    public string? Telefone { get; set; }

    [Column("foto")]
    public string? Foto { get; set; }

    [Column("criadoem")]
    public DateTime DataCriacao { get; set; } = DateTime.Now;

    [Column("atualizadoem")]
    public DateTime? DataAtualizacao { get; set; }

    [Column("deletadoem")]
    public DateTime? DataDeletado { get; set; }

    [Column("verificado")]
    public bool Verificado { get; set; }
}
