using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using CmdPalCatPetExtension.Models;
using CmdPalCatPetExtension.Services;

namespace CmdPalCatPetExtension.Pages
{
    internal sealed partial class CreateCatPage : ContentPage
    {
        private readonly CatCreationForm sampleForm = new();

        public override IContent[] GetContent() => [sampleForm];
        public CreateCatPage()
        {
            Icon = new("😺");
            Title = "Create your cat pet";
            Name = "Name your cat";
        }

    }
    internal sealed partial class CatCreationForm : FormContent
    {
        public CatCreationForm()
        {
            TemplateJson = $$"""
{
    "$schema": "http://adaptivecards.io/schemas/adaptive-card.json",
    "type": "AdaptiveCard",
    "version": "1.6",
    "body": [
        {
            "type": "TextBlock",
            "size": "medium",
            "weight": "bolder",
            "text": " Name your new cat",
            "horizontalAlignment": "center",
            "wrap": true,
            "style": "heading"
        },
        {
            "type": "Input.Text",
            "label": "Your cat's name",
            "style": "text",
            "id": "catName",
            "isRequired": true,
            "errorMessage": "A name is required",
            "placeholder": "Enter name of your new cat"
        }
    ],
    "actions": [
        {
            "type": "Action.Submit",
            "title": "Submit",
            "data": {
                "id": "1234567890"
            }
        }
    ]
}
""";

        }

        public override CommandResult SubmitForm(string payload)
        {
            var formInput = JsonNode.Parse(payload)?.AsObject();
            if (formInput == null)
            {
                return CommandResult.GoHome();
            }
            string? name = formInput["catName"]?.ToString();

            ConfirmationArgs confirmArgs = new()
            {
                PrimaryCommand = new AnonymousCommand(
                () =>
                {
                    var rnd = new Random();
                    int energy = rnd.Next(70, 101);    // 70-100
                    int hunger = rnd.Next(0, 31);       // 0-30 (lower is better)
                    int happiness = rnd.Next(60, 101); // 60-100
                    var cat = new VirtualCat(name ?? "Unnamed", energy, hunger, happiness);
                    CatRepository.Save(cat);
                    ToastStatusMessage t = new($"Created cat \"{cat.Name}\"");
                    t.Show();
                })
                {
                    Name = "Adopt",
                    Result = CommandResult.GoBack(),
                },
                Title = "Adoption",
                Description = "You were able to name your new cat " + (name ?? "Unnamed"),
            };
            return CommandResult.Confirm(confirmArgs);
        }
    }
}
