namespace Kafu.Model.Helper
{
    public class CultureHelper
    {
        public static string Direction =>
            Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft ? "rtl" : "ltr";
        public static string DirectionClass =>
            Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft ? "left" : "right";
        public static string DirectionFull =>
            Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft ? "ar-SA" : "en-US";
        public static string DirectionChangeFull =>
            Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft ? "en-US" : "ar-SA";
        public static string DirectionString =>
            Thread.CurrentThread.CurrentUICulture.TextInfo.IsRightToLeft ? "English" : "العربية";

        public static string GetCurrentNeutralCulture =>
            GetNeutralCulture(Thread.CurrentThread.CurrentUICulture.Name);

        public static string GetImplementedCulture(string name)
        {
            if (string.IsNullOrEmpty(name))
                return GetDefaultCulture();
            return name;
        }

        public static string GetDefaultCulture()
        {
            return "ar-SA";
        }
        public static string GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentUICulture.Name;
        }

        public static string GetNeutralCulture(string name)
        {
            if (!name.Contains("-")) return name;

            return name.Split('-')[0];
        }
    }
}
