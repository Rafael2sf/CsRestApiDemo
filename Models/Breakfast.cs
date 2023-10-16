using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace BuberBreakfast.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Breakfast
    {
        public int Id { get; set; }

        [StringLength(42)]
        public string Name { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }
        public int Duration { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public Breakfast(
            int id,
            string name,
            string description,
            int duration,
            DateTime lastModifiedDate
        ) {
            Id = id;
            Name = name;
            Description = description;
            Duration = duration;
            LastModifiedDate = lastModifiedDate;
        }
    }

    public class SearchBreakfastDto
    {
        [DefaultValue(null)]
        [MaxLength(42)]
        public string? Name { get; set; }

        [DefaultValue(300)]
        [Range(5, 300)]
        public int MaxDuration { get; set; }
        [DefaultValue(100)]
        [Range(1, 100)]
        public int Limit {  get; set; }
    }

    public class CreateBreakfastDto
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(42, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(1000, MinimumLength = 50)]
        public string Description { get; set; }

        [Required]
        [Range(5, 300)]
        public int Duration { get; set; }
    }

    public class UpdateBreakfastDto
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(42, MinimumLength = 3)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(1000, MinimumLength = 50)]
        public string Description { get; set; }

        [Required]
        [Range(5, 300)]
        public int Duration { get; set; }
    }

    public class ResponseBreakfastDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }

        public ResponseBreakfastDto(int id, string name, string description, int duration)
        {
            Id = id;
            Name = name;
            Description = description;
            Duration = duration;
        }

        public ResponseBreakfastDto(Breakfast breakfast)
        {
            Id = breakfast.Id;
            Name = breakfast.Name;
            Description = breakfast.Description;
            Duration = breakfast.Duration;
        }
    }
}
