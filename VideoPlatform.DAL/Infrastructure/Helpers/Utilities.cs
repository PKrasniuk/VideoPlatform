using System.Linq;

namespace VideoPlatform.DAL.Infrastructure.Helpers
{
    public static class Utilities
    {
        public static string FirstCharToUpper(this string input)
        {
            return string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input)
                ? input
                : input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}