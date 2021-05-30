namespace VideoPlatform.Common.Infrastructure.Helpers
{
    public static class StringHelper
    {
        public static string RemoveFromEnd(this string s, string suffix)
        {
            return s.EndsWith(suffix) ? s[..^suffix.Length] : s;
        }
    }
}