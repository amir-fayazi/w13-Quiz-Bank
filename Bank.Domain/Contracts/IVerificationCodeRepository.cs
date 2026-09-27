
using w13_Quiz.DTOs;
using w13_Quiz.Entities;

namespace w13_Quiz.Contracts
{
    public interface IVerificationCodeRepository
    {
        void Save(VerificationCode verificationCode);

        VerificationCode? GetById(Guid id);
    }
}
