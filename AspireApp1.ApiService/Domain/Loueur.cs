namespace AspireApp1.ApiService.Domain;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table("t_e_loueur_lou")]
[Index(nameof(Mobile), IsUnique = true)]
public class Loueur
{
    [Key]
    [Column("lou_id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("lou_nom")]
    public string Nom { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    [Column("lou_prenom")]
    public string Prenom { get; set; } = null!;

    [Required]
    [MaxLength(10)]
    [Column("lou_mobile")]
    public string Mobile { get; set; } = null!;

    [MaxLength(100)]
    [Column("lou_rue")]
    public string? Rue { get; set; }

    [MaxLength(5)]
    [Column("lou_cp")]
    public string? Cp { get; set; }

    [MaxLength(50)]
    [Column("lou_ville")]
    public string? Ville { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("lou_pays")]
    [DefaultValue("France")]
    public string Pays { get; set; } = "France";
}