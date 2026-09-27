

namespace Bank.Application.Services
{
    public static class VerificationCodeGenerator
        
    {
        public static string Generate()
        {
            return Random.Shared
                .Next(10000, 100000)
                .ToString();
        }
    }
}
