using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class User
{
    [BsonId]
    public ObjectId Id { get; set; }

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
    public required string firstName { get; set; }

    public string? lastName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    public required string email { get; set; }

    public string? guid { get; set; }

    public string? address { get; set; }

    public DateTime created_date { get; set; }

    public string? created_user { get; set; }
}
