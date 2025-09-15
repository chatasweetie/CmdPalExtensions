// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Diagnostics;
using System.Text.Json.Nodes;
using Windows.ApplicationModel.DataTransfer;


namespace CmdPalTransalationExtension;

internal sealed partial class CmdPalTransalationExtensionPage : ContentPage
{
    private readonly TranslationForm _translationForm = new();
    public override IContent[] GetContent() => [_translationForm];

    public CmdPalTransalationExtensionPage()
    {
        Name = "Translator";
        Title = "Text Translator";
        Icon = new IconInfo("\uE8C1"); // Globe icon for translation
    }

    internal sealed partial class TranslationForm : FormContent
    {
        public TranslationForm() {
            TemplateJson = AdaptiveCardTemplates.TranslationCard;
            DataJson = LanguageData.GetLanguageDataJson();
        }
        
        public override ICommandResult SubmitForm(string payload)
        {
            try
            {
                var formInput = JsonNode.Parse(payload)?.AsObject();
                Debug.WriteLine($"Form submitted with payload: {formInput}");

                if (formInput == null)
                {
                    ShowToast("Invalid form data received");
                    return CommandResult.KeepOpen();
                }

                var translationRequest = ExtractTranslationRequest(formInput);
                if (translationRequest == null)
                {
                    ShowToast("Please fill in all required fields");
                    return CommandResult.KeepOpen();
                }

                var translatedText = TranslateTextSafely(translationRequest);

                CopyToClipboard(translatedText);

                return CommandResult.KeepOpen();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Form submission error: {ex.Message}");
                ShowToast("An unexpected error occurred during translation");
                return CommandResult.KeepOpen();
            }
        }

        private static TranslationRequest? ExtractTranslationRequest(JsonObject formInput)
        {
            var fromLang = formInput["fromLang"]?.ToString();
            var toLang = formInput["toLang"]?.ToString();
            var text = formInput["Text"]?.ToString();

            if (string.IsNullOrWhiteSpace(text))
                return null;

            return new TranslationRequest(fromLang, toLang, text);
        }

        private static string TranslateTextSafely(TranslationRequest request)
        {
            try
            {
                return Translator.Translate(request.FromLanguage, request.ToLanguage, request.Text)
                    .GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Translation error: {ex.Message}");
                return "Sorry, translation failed. Please check your internet connection and try again.";
            }
        }

        private static void CopyToClipboard(string text)
        {
            try
            {
                TextCopy.ClipboardService.SetText(text);
                ShowToast($"Translation copied to clipboard:\n{text}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Clipboard error: {ex.Message}");
                ShowToast("An unexpected error occurred on the clipboard");
            }
        }

        private static void ShowToast(string message)
        {
            var toast = new ToastStatusMessage(message);
            toast.Show();
        }

    }

    private record TranslationRequest(string? FromLanguage, string? ToLanguage, string Text);
}





//    {
//        public TranslationForm()
//        {

//            TemplateJson = $$"""
//{
//    "$schema": "http://adaptivecards.io/schemas/adaptive-card.json",
//    "type": "AdaptiveCard",
//    "version": "1.6",
//    "body": [
//        {
//            "type": "TextBlock",
//            "size": "Medium",
//            "weight": "Bolder",
//            "text": " Text Translator",
//            "horizontalAlignment": "Center",
//            "wrap": true,
//            "style": "heading"
//        },
//        {
//            "type": "Input.ChoiceSet",
//            "id": "fromLang",
//            "choices": "${languages}",
//            "placeholder": "Language of the text your translating",
//            "value": "${defaultFromLang}"
//        },
//        {
//            "type": "Input.ChoiceSet",
//            "id": "toLang",
//            "choices": "${languages}",
//            "placeholder": "Language you want to translate your text",
//            "value": "${defaultToLang}"
//        },
//        {
//            "type": "Input.Text",
//            "label": "Input",
//            "id": "Text",
//            "isRequired": true,
//            "errorMessage": "Text is required",
//            "placeholder": "Enter your text"
//        }
//    ],
//    "actions": [
//        {
//            "type": "Action.Submit",
//            "title": "Submit",
//            "data": {
//                "id": "1234567890"
//            }
//        }
//    ]
//}
//""";
//            DataJson = $$"""
//{
//    "defaultFromLang": "",
//    "defaultToLang": "",
//    "languages": [
//        {"title": "Afrikaans", "value": "af"},
//        {"title": "Amharic", "value": "am"},
//        {"title": "Arabic", "value": "ar"},
//        {"title": "Assamese", "value": "as"},
//        {"title": "Azerbaijani", "value": "az"},
//        {"title": "Bashkir", "value": "ba"},
//        {"title": "Belarusian", "value": "be"},
//        {"title": "Bulgarian", "value": "bg"},
//        {"title": "Bhojpuri", "value": "bho"},
//        {"title": "Bangla", "value": "bn"},
//        {"title": "Tibetan", "value": "bo"},
//        {"title": "Bodo", "value": "brx"},
//        {"title": "Bosnian", "value": "bs"},
//        {"title": "Catalan", "value": "ca"},
//        {"title": "Czech", "value": "cs"},
//        {"title": "Welsh", "value": "cy"},
//        {"title": "Danish", "value": "da"},
//        {"title": "German", "value": "de"},
//        {"title": "Dogri", "value": "doi"},
//        {"title": "Lower Sorbian", "value": "dsb"},
//        {"title": "Divehi", "value": "dv"},
//        {"title": "Greek", "value": "el"},
//        {"title": "English", "value": "en"},
//        {"title": "Spanish", "value": "es"},
//        {"title": "Estonian", "value": "et"},
//        {"title": "Basque", "value": "eu"},
//        {"title": "Persian", "value": "fa"},
//        {"title": "Finnish", "value": "fi"},
//        {"title": "Filipino", "value": "fil"},
//        {"title": "Fijian", "value": "fj"},
//        {"title": "Faroese", "value": "fo"},
//        {"title": "French", "value": "fr"},
//        {"title": "French (Canada)", "value": "fr-CA"},
//        {"title": "Irish", "value": "ga"},
//        {"title": "Galician", "value": "gl"},
//        {"title": "Konkani", "value": "gom"},
//        {"title": "Gujarati", "value": "gu"},
//        {"title": "Hausa", "value": "ha"},
//        {"title": "Hebrew", "value": "he"},
//        {"title": "Hindi", "value": "hi"},
//        {"title": "Chhattisgarhi", "value": "hne"},
//        {"title": "Croatian", "value": "hr"},
//        {"title": "Upper Sorbian", "value": "hsb"},
//        {"title": "Haitian Creole", "value": "ht"},
//        {"title": "Hungarian", "value": "hu"},
//        {"title": "Armenian", "value": "hy"},
//        {"title": "Indonesian", "value": "id"},
//        {"title": "Igbo", "value": "ig"},
//        {"title": "Inuinnaqtun", "value": "ikt"},
//        {"title": "Icelandic", "value": "is"},
//        {"title": "Italian", "value": "it"},
//        {"title": "Inuktitut", "value": "iu"},
//        {"title": "Inuktitut (Latin)", "value": "iu-Latn"},
//        {"title": "Japanese", "value": "ja"},
//        {"title": "Georgian", "value": "ka"},
//        {"title": "Kazakh", "value": "kk"},
//        {"title": "Khmer", "value": "km"},
//        {"title": "Kurdish (Northern)", "value": "kmr"},
//        {"title": "Kannada", "value": "kn"},
//        {"title": "Korean", "value": "ko"},
//        {"title": "Kashmiri", "value": "ks"},
//        {"title": "Kurdish (Central)", "value": "ku"},
//        {"title": "Kyrgyz", "value": "ky"},
//        {"title": "Luxembourgish", "value": "lb"},
//        {"title": "Lingala", "value": "ln"},
//        {"title": "Lao", "value": "lo"},
//        {"title": "Lithuanian", "value": "lt"},
//        {"title": "Ganda", "value": "lug"},
//        {"title": "Latvian", "value": "lv"},
//        {"title": "Chinese (Literary)", "value": "lzh"},
//        {"title": "Maithili", "value": "mai"},
//        {"title": "Malagasy", "value": "mg"},
//        {"title": "Māori", "value": "mi"},
//        {"title": "Macedonian", "value": "mk"},
//        {"title": "Malayalam", "value": "ml"},
//        {"title": "Mongolian (Cyrillic)", "value": "mn-Cyrl"},
//        {"title": "Mongolian (Traditional)", "value": "mn-Mong"},
//        {"title": "Manipuri", "value": "mni"},
//        {"title": "Marathi", "value": "mr"},
//        {"title": "Malay", "value": "ms"},
//        {"title": "Maltese", "value": "mt"},
//        {"title": "Hmong Daw", "value": "mww"},
//        {"title": "Myanmar (Burmese)", "value": "my"},
//        {"title": "Norwegian", "value": "nb"},
//        {"title": "Nepali", "value": "ne"},
//        {"title": "Dutch", "value": "nl"},
//        {"title": "Sesotho sa Leboa", "value": "nso"},
//        {"title": "Nyanja", "value": "nya"},
//        {"title": "Odia", "value": "or"},
//        {"title": "Querétaro Otomi", "value": "otq"},
//        {"title": "Punjabi", "value": "pa"},
//        {"title": "Polish", "value": "pl"},
//        {"title": "Dari", "value": "prs"},
//        {"title": "Pashto", "value": "ps"},
//        {"title": "Portuguese (Brazil)", "value": "pt"},
//        {"title": "Portuguese (Portugal)", "value": "pt-PT"},
//        {"title": "Romanian", "value": "ro"},
//        {"title": "Russian", "value": "ru"},
//        {"title": "Rundi", "value": "run"},
//        {"title": "Kinyarwanda", "value": "rw"},
//        {"title": "Sindhi", "value": "sd"},
//        {"title": "Sinhala", "value": "si"},
//        {"title": "Slovak", "value": "sk"},
//        {"title": "Slovenian", "value": "sl"},
//        {"title": "Samoan", "value": "sm"},
//        {"title": "Shona", "value": "sn"},
//        {"title": "Somali", "value": "so"},
//        {"title": "Albanian", "value": "sq"},
//        {"title": "Serbian (Cyrillic)", "value": "sr-Cyrl"},
//        {"title": "Serbian (Latin)", "value": "sr-Latn"},
//        {"title": "Sesotho", "value": "st"},
//        {"title": "Swedish", "value": "sv"},
//        {"title": "Swahili", "value": "sw"},
//        {"title": "Tamil", "value": "ta"},
//        {"title": "Telugu", "value": "te"},
//        {"title": "Thai", "value": "th"},
//        {"title": "Tigrinya", "value": "ti"},
//        {"title": "Turkmen", "value": "tk"},
//        {"title": "Klingon (Latin)", "value": "tlh-Latn"},
//        {"title": "Klingon (pIqaD)", "value": "tlh-Piqd"},
//        {"title": "Setswana", "value": "tn"},
//        {"title": "Tongan", "value": "to"},
//        {"title": "Turkish", "value": "tr"},
//        {"title": "Tatar", "value": "tt"},
//        {"title": "Tahitian", "value": "ty"},
//        {"title": "Uyghur", "value": "ug"},
//        {"title": "Ukrainian", "value": "uk"},
//        {"title": "Urdu", "value": "ur"},
//        {"title": "Uzbek (Latin)", "value": "uz"},
//        {"title": "Vietnamese", "value": "vi"},
//        {"title": "Xhosa", "value": "xh"},
//        {"title": "Yoruba", "value": "yo"},
//        {"title": "Yucatec Maya", "value": "yua"},
//        {"title": "Cantonese (Traditional)", "value": "yue"},
//        {"title": "Chinese Simplified", "value": "zh-Hans"},
//        {"title": "Chinese Traditional", "value": "zh-Hant"},
//        {"title": "Zulu", "value": "zu"}
//  ]
//}
//""";
//        }

//        public override ICommandResult SubmitForm(string payload)
//        {
//            var formInput = JsonNode.Parse(payload)?.AsObject();
//            Debug.WriteLine($"Form submitted with formInput: {formInput}");
//            if (formInput == null)
//                return CommandResult.GoHome();

//            string? fromLang = formInput["fromLang"]?.ToString();
//            string? toLang = formInput["toLang"]?.ToString();
//            string? text = formInput["Text"]?.ToString();

//            if (string.IsNullOrWhiteSpace(text))
//                return CommandResult.KeepOpen();

//            string translatedText = TranslateText(fromLang, toLang, text);

//            CopyToClipboard(translatedText);
//            ShowToast($"Translated text copied to clipboard:\n{translatedText}");

//            return CommandResult.KeepOpen();

//        }

//        private static string TranslateText(string? fromLang, string? toLang, string? text)
//        {
//            try
//            {
//                return Translator.Translate(fromLang, toLang, text).GetAwaiter().GetResult();
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine($"Translation error: {ex.Message}");
//                return "Sorry, there was a translation error";
//            }
//        }

//        private static void CopyToClipboard(string text)
//        {
//            TextCopy.ClipboardService.SetText(text);
//        }

//        private static void ShowToast(string message)
//        {
//            var toast = new ToastStatusMessage(message);
//            toast.Show();
//        }
//    }
//}



//         
