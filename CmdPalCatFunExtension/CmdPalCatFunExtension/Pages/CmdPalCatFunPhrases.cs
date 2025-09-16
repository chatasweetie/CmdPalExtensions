using System;

namespace CmdPalCatFunExtension.Pages
{
    internal static class CmdPalCatFunPhrases
    {
        private static readonly Random _rand = new();
        private static readonly string[] _phrases = new[]
        {
            "Chase away the blues",
            "Make you smile",
            "Add a purr to your step",
            "Spark some joy",
            "Light up your mood",
            "Bring a whisker of happiness",
            "Curl up with a smile",
            "Deliver a dose of cute",
            "Make your tail wag",
            "Sprinkle some feline magic",
            "Boost your vibe",
            "Add fluff to your focus",
            "Distract delightfully",
            "Make your heart purr",
            "Break up the blahs",
            "Add sparkle to your scroll",
            "Give paws for joy",
            "Make your screen meow",
            "Add a giggle to your grind",
            "Unleash the whisker wonder"
        };

        public static string GetRandomPhrase()
        {
            return _phrases[_rand.Next(_phrases.Length)];
        }
    }
}
