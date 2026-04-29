namespace AspireApp1.ApiService.Domain;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("t_e_voiture_voi")]
public class Voiture
{
    [Key]
    [Column("voi_id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(9)]
    [Column("voi_immatriculation")]
    public string Immatriculation { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    [Column("voi_marque")]
    public string Marque { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    [Column("voi_modele")]
    public string Modele { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    [Column("voi_categorie")]
    public string Categorie { get; set; } = null!;

    [Required]
    [Column("voi_prixlocationparjour", TypeName = "numeric(5,2)")]
    public decimal PrixLocationParJour { get; set; }
}