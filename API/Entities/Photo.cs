using System.ComponentModel.DataAnnotations.Schema;
using App.Entities;

namespace API.Entities;

[Table("Photos")]
public class Photo
{

    public int? Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool IsMain { get; set; } = false;
    public string? PublicId { get; set; }
    public int AppUserId { get; set; }
    public AppUser? AppUser { get; set; } = null;
}