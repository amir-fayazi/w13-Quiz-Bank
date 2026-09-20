

namespace LibraryManagement.Domain.Extensions
{
    public static class String
    {
        public static bool IsValidText(this string text)
        {
            return !string.IsNullOrWhiteSpace(text);
        }
    }
}
