namespace WorkoutReservations.Application.Models.Trainer
{
    public class TrainerDetailsDto
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Age { get; set; }
        public string Bio { get; set; } = null!;
        public double Weight { get; set; }
        public double Height { get; set; }
        public string Picture { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
