using System.ComponentModel.DataAnnotations;

namespace WorkoutReservations.Domain.Entities
{
    public class Location
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string City { get; set; } = null!;

        [Required]
        public string Region { get; set; } = null!;

        [Required]
        public string Address { get; set; } = null!;

        [Required]
        public string Latitude { get; set; } = null!;

        [Required]
        public string Longitude { get; set; } = null!;

        [Required]
        public string ZipCode { get; set; } = null!;

        [Required]
        public int MaxCapacity { get; set; }

        public virtual ICollection<Workout> Workouts { get; set; } = new List<Workout>();
        public virtual ICollection<User> Trainers { get; set; } = new List<User>();

    }
}
