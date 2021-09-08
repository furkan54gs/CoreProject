using System;

namespace core.business.Extensions
{
    public class Replacements
    {
        public static string ConvertUrl(string text)
        {
            String[] olds = { "Ğ", "ğ", "Ü", "ü", "Ş", "ş", "İ", "ı", "Ö", "ö", "Ç", "ç"," " };
            String[] news = { "G", "g", "U", "u", "S", "s", "I", "i", "O", "o", "C", "c","-" };

            for (int i = 0; i < olds.Length; i++)
            {
                text = text.Replace(olds[i], news[i]);
            }

            text = text.ToLower();

            return text;
        }
    }
}