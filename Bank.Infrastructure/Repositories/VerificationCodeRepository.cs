using Newtonsoft.Json;
using w13_Quiz.Contracts;
using w13_Quiz.Entities;

namespace Bank.Infrastructure.Repositories
{
    public class VerificationCodeRepository : IVerificationCodeRepository

    {
        private readonly string _filePath;

        public VerificationCodeRepository(string filePath)
        {
            _filePath = filePath;

        }

        public void Save(VerificationCode verificationCode)
        {
            var verificationCodes = ReadAll();

            verificationCodes.Add(verificationCode);

            var json = JsonConvert.SerializeObject(verificationCodes, Formatting.Indented);

            File.WriteAllText(_filePath, json);
        }

        public VerificationCode? GetById(Guid id)
        {
            var verificationCodes = ReadAll();

            return verificationCodes
                .FirstOrDefault(x => x.Id == id);
        }

        private List<VerificationCode> ReadAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<VerificationCode>();
            }

            var json = File.ReadAllText(_filePath);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new List<VerificationCode>();
            }

            return JsonConvert.DeserializeObject<List<VerificationCode>>(json) ?? new List<VerificationCode>();


        }


    }
}
