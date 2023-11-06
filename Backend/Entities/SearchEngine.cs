namespace Backend.Entities;

public class StringEnum
{
    public static class SearchEngine
    {
        public const string Google = "Google";
        public const string Bing = "Bing";
        public const string Yahoo = "Yahoo";

        public static bool IsValidValue(string value)
        {
            return value == Google || value == Bing || value == Yahoo;
        }
    }
}
