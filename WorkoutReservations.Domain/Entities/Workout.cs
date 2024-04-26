using System.ComponentModel.DataAnnotations;

namespace WorkoutReservations.Domain.Entities
{
    public class Workout
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;
        public string EquipmentNeeded { get; set; } = null!;

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public string Gender { get; set; } = null!;

        [Required]
        public int IntensityLevel { get; set; }

        [Required]
        public string Status { get; set; } = null!;

        [Required]
        public string Picture { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string RecommendedFrequency { get; set; } = null!;
        public Guid WorkoutCategoryId { get; set; }
        public virtual WorkoutCategory WorkoutCategory { get; set; } = null!;

        public virtual ICollection<User> Trainers { get; set; } = new List<User>();

        public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

        //public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}
