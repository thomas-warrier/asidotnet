namespace AspireApp1.ApiService.Domain;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("t_j_location_loc")]
public class Location
{
    [Key]
    [Column("loc_id")]
    public int Id { get; set; }
    
    [Column("loc_paye")]
    public bool EstPaye { get; set; } = false;

    [Required]
    [Column("voi_id")]
    public int VoitureId { get; set; }

    [Required]
    [Column("lou_id")]
    public int LoueurId { get; set; }

    [Required]
    [Column("loc_datedebut")]
    public DateTime DateDebut { get; set; }

    [Required]
    [Column("loc_datefin")]
    public DateTime DateFin { get; set; }
    
    [Column("loc_annule")] 
    public bool EstAnnule { get; set; } = false;

    [ForeignKey(nameof(VoitureId))]
    public virtual Voiture Voiture { get; set; } = null!;

    [ForeignKey(nameof(LoueurId))]
    public virtual Loueur Loueur { get; set; } = null!;
}