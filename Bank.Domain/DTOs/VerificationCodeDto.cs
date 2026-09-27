

namespace w13_Quiz.DTOs
{
    public class VerificationCodeDto
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
