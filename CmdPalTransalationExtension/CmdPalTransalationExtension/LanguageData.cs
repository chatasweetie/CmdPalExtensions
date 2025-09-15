// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.Text.Json;

namespace CmdPalTransalationExtension;

internal static class LanguageData
{
    public static string GetLanguageDataJson()
    {
        return """
            {
                "defaultFromLang": "",
                "defaultToLang": "",
                "languages": [
                    {"title": "Afrikaans", "value": "af"},
                    {"title": "Albanian", "value": "sq"},
                    {"title": "Amharic", "value": "am"},
                    {"title": "Arabic", "value": "ar"},
                    {"title": "Armenian", "value": "hy"},
                    {"title": "Assamese", "value": "as"},
                    {"title": "Azerbaijani", "value": "az"},
                    {"title": "Bangla", "value": "bn"},
                    {"title": "Basque", "value": "eu"},
                    {"title": "Bashkir", "value": "ba"},
                    {"title": "Belarusian", "value": "be"},
                    {"title": "Bhojpuri", "value": "bho"},
                    {"title": "Bodo", "value": "brx"},
                    {"title": "Bosnian", "value": "bs"},
                    {"title": "Bulgarian", "value": "bg"},
                    {"title": "Cantonese (Traditional)", "value": "yue"},
                    {"title": "Catalan", "value": "ca"},
                    {"title": "Chhattisgarhi", "value": "hne"},
                    {"title": "Chinese (Literary)", "value": "lzh"},
                    {"title": "Chinese Simplified", "value": "zh-Hans"},
                    {"title": "Chinese Traditional", "value": "zh-Hant"},
                    {"title": "Croatian", "value": "hr"},
                    {"title": "Czech", "value": "cs"},
                    {"title": "Danish", "value": "da"},
                    {"title": "Dari", "value": "prs"},
                    {"title": "Divehi", "value": "dv"},
                    {"title": "Dogri", "value": "doi"},
                    {"title": "Dutch", "value": "nl"},
                    {"title": "English", "value": "en"},
                    {"title": "Estonian", "value": "et"},
                    {"title": "Faroese", "value": "fo"},
                    {"title": "Fijian", "value": "fj"},
                    {"title": "Filipino", "value": "fil"},
                    {"title": "Finnish", "value": "fi"},
                    {"title": "French", "value": "fr"},
                    {"title": "French (Canada)", "value": "fr-CA"},
                    {"title": "Galician", "value": "gl"},
                    {"title": "Ganda", "value": "lug"},
                    {"title": "Georgian", "value": "ka"},
                    {"title": "German", "value": "de"},
                    {"title": "Greek", "value": "el"},
                    {"title": "Gujarati", "value": "gu"},
                    {"title": "Haitian Creole", "value": "ht"},
                    {"title": "Hausa", "value": "ha"},
                    {"title": "Hebrew", "value": "he"},
                    {"title": "Hindi", "value": "hi"},
                    {"title": "Hmong Daw", "value": "mww"},
                    {"title": "Hungarian", "value": "hu"},
                    {"title": "Igbo", "value": "ig"},
                    {"title": "Icelandic", "value": "is"},
                    {"title": "Indonesian", "value": "id"},
                    {"title": "Inuinnaqtun", "value": "ikt"},
                    {"title": "Irish", "value": "ga"},
                    {"title": "Italian", "value": "it"},
                    {"title": "Inuktitut", "value": "iu"},
                    {"title": "Inuktitut (Latin)", "value": "iu-Latn"},
                    {"title": "Japanese", "value": "ja"},
                    {"title": "Kannada", "value": "kn"},
                    {"title": "Kashmiri", "value": "ks"},
                    {"title": "Kazakh", "value": "kk"},
                    {"title": "Khmer", "value": "km"},
                    {"title": "Kinyarwanda", "value": "rw"},
                    {"title": "Klingon (Latin)", "value": "tlh-Latn"},
                    {"title": "Klingon (pIqaD)", "value": "tlh-Piqd"},
                    {"title": "Konkani", "value": "gom"},
                    {"title": "Korean", "value": "ko"},
                    {"title": "Kurdish (Central)", "value": "ku"},
                    {"title": "Kurdish (Northern)", "value": "kmr"},
                    {"title": "Kyrgyz", "value": "ky"},
                    {"title": "Lao", "value": "lo"},
                    {"title": "Latvian", "value": "lv"},
                    {"title": "Lingala", "value": "ln"},
                    {"title": "Lithuanian", "value": "lt"},
                    {"title": "Lower Sorbian", "value": "dsb"},
                    {"title": "Luxembourgish", "value": "lb"},
                    {"title": "Maithili", "value": "mai"},
                    {"title": "Malagasy", "value": "mg"},
                    {"title": "Malay", "value": "ms"},
                    {"title": "Malayalam", "value": "ml"},
                    {"title": "Manipuri", "value": "mni"},
                    {"title": "Māori", "value": "mi"},
                    {"title": "Marathi", "value": "mr"},
                    {"title": "Maltese", "value": "mt"},
                    {"title": "Macedonian", "value": "mk"},
                    {"title": "Mongolian (Cyrillic)", "value": "mn-Cyrl"},
                    {"title": "Mongolian (Traditional)", "value": "mn-Mong"},
                    {"title": "Myanmar (Burmese)", "value": "my"},
                    {"title": "Nepali", "value": "ne"},
                    {"title": "Norwegian", "value": "nb"},
                    {"title": "Nyanja", "value": "nya"},
                    {"title": "Odia", "value": "or"},
                    {"title": "Pashto", "value": "ps"},
                    {"title": "Persian", "value": "fa"},
                    {"title": "Polish", "value": "pl"},
                    {"title": "Portuguese (Brazil)", "value": "pt"},
                    {"title": "Portuguese (Portugal)", "value": "pt-PT"},
                    {"title": "Punjabi", "value": "pa"},
                    {"title": "Querétaro Otomi", "value": "otq"},
                    {"title": "Romanian", "value": "ro"},
                    {"title": "Rundi", "value": "run"},
                    {"title": "Russian", "value": "ru"},
                    {"title": "Samoan", "value": "sm"},
                    {"title": "Sesotho", "value": "st"},
                    {"title": "Sesotho sa Leboa", "value": "nso"},
                    {"title": "Setswana", "value": "tn"},
                    {"title": "Shona", "value": "sn"},
                    {"title": "Sindhi", "value": "sd"},
                    {"title": "Sinhala", "value": "si"},
                    {"title": "Slovak", "value": "sk"},
                    {"title": "Slovenian", "value": "sl"},
                    {"title": "Somali", "value": "so"},
                    {"title": "Spanish", "value": "es"},
                    {"title": "Swahili", "value": "sw"},
                    {"title": "Swedish", "value": "sv"},
                    {"title": "Tahitian", "value": "ty"},
                    {"title": "Tamil", "value": "ta"},
                    {"title": "Tatar", "value": "tt"},
                    {"title": "Telugu", "value": "te"},
                    {"title": "Thai", "value": "th"},
                    {"title": "Tibetan", "value": "bo"},
                    {"title": "Tigrinya", "value": "ti"},
                    {"title": "Turkish", "value": "tr"},
                    {"title": "Turkmen", "value": "tk"},
                    {"title": "Ukrainian", "value": "uk"},
                    {"title": "Upper Sorbian", "value": "hsb"},
                    {"title": "Urdu", "value": "ur"},
                    {"title": "Uzbek (Latin)", "value": "uz"},
                    {"title": "Vietnamese", "value": "vi"},
                    {"title": "Welsh", "value": "cy"},
                    {"title": "Xhosa", "value": "xh"},
                    {"title": "Yoruba", "value": "yo"},
                    {"title": "Yucatec Maya", "value": "yua"},
                    {"title": "Zulu", "value": "zu"}
                ]
            }
            """;
    }

    // Alternative: Load from external JSON file
    public static string GetLanguageDataFromFile(string filePath = "languages.json")
    {
        try
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            
            // Fallback to embedded data
            return GetLanguageDataJson();
        }
        catch
        {
            return GetLanguageDataJson();
        }
    }
}