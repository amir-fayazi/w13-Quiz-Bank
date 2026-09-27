using Newtonsoft.Json;

namespace w13_Quiz.Entities
{
    public class VerificationCode
    {
        public Guid Id { get; private set; }

        public string Code { get; private set; } = null!;

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;


        [JsonConstructor]
        private VerificationCode(Guid id, string code, DateTime createdAt)
        {
            Id = id;
            Code = code;
            CreatedAt = createdAt;
        }

        public VerificationCode(string code)
        {
            Code = code;
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

    }
}
