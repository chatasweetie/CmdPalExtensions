// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace CmdPalTransalationExtension;

internal static class AdaptiveCardTemplates
{
    public const string TranslationCard = """
        {
            "$schema": "http://adaptivecards.io/schemas/adaptive-card.json",
            "type": "AdaptiveCard",
            "version": "1.6",
            "body": [
                {
                    "type": "TextBlock",
                    "size": "Medium",
                    "weight": "Bolder",
                    "text": "🌐 Text Translator",
                    "horizontalAlignment": "Center",
                    "wrap": true,
                    "style": "heading"
                },
                {
                    "type": "ColumnSet",
                    "columns": [
                        {
                            "type": "Column",
                            "width": "stretch",
                            "items": [
                                {
                                    "type": "TextBlock",
                                    "text": "From Language",
                                    "size": "Small",
                                    "weight": "Bolder"
                                },
                                {
                                    "type": "Input.ChoiceSet",
                                    "id": "fromLang",
                                    "choices": "${languages}",
                                    "placeholder": "Source language (auto-detect if empty)",
                                    "value": "${defaultFromLang}"
                                }
                            ]
                        },
                        {
                            "type": "Column",
                            "width": "stretch",
                            "items": [
                                {
                                    "type": "TextBlock",
                                    "text": "To Language",
                                    "size": "Small",
                                    "weight": "Bolder"
                                },
                                {
                                    "type": "Input.ChoiceSet",
                                    "id": "toLang",
                                    "choices": "${languages}",
                                    "placeholder": "Target language",
                                    "value": "${defaultToLang}",
                                    "isRequired": true,
                                    "errorMessage": "Please select a target language"
                                }
                            ]
                        }
                    ]
                },
                {
                    "type": "Input.Text",
                    "label": "Text to Translate",
                    "id": "Text",
                    "isRequired": true,
                    "errorMessage": "Please enter text to translate",
                    "placeholder": "Enter the text you want to translate...",
                    "isMultiline": true,
                    "maxLength": 5000
                }
            ],
            "actions": [
                {
                    "type": "Action.Submit",
                    "title": "🔄 Translate",
                    "style": "positive"
                }
            ]
        }
        """;
}