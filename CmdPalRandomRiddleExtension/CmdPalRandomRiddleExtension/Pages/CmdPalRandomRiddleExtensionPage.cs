// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Resources;
using System.Text.Json;
using System.Threading.Tasks;

namespace CmdPalRandomRiddleExtension;

internal sealed partial class CmdPalRandomRiddleExtensionPage : ContentPage
{
    internal static readonly HttpClient Client = new();
    internal static readonly JsonSerializerOptions Options = new() { PropertyNameCaseInsensitive = true };
    private Riddle? _currentRiddle;

    public CmdPalRandomRiddleExtensionPage()
    {
        Icon = new("\uF142"); //❓
        Title = "Your Random Riddle";

        _currentRiddle ??= GetRiddleAsync().GetAwaiter().GetResult();

        var Riddle = _currentRiddle;

        ConfirmationArgs confirmArgs = new()
        {
            PrimaryCommand = new AnonymousCommand(
                () => { _currentRiddle = null; RaiseItemsChanged(); })
            {
                Name = "Done",
                Result = CommandResult.GoHome(),
            },
            Title = "Answer",
            Description = Riddle.Answer,
        };

        Commands = [
            new CommandContextItem(
            title: "Do thing",
            name: "Press Enter for answer",
            result: CommandResult.Confirm(confirmArgs),
            action: () => { _currentRiddle = null; })
        ];

    }
    public override IContent[] GetContent()
    {
        // If _currentRiddle is null, fetch a new one synchronously
        if (_currentRiddle == null)
        {
            _currentRiddle = GetRiddleAsync().GetAwaiter().GetResult();
        }

        var Riddle = _currentRiddle;

        ConfirmationArgs confirmArgs = new()
        {
            PrimaryCommand = new AnonymousCommand(
                () =>
                {
                    _currentRiddle = null;
                    RaiseItemsChanged();
                })
            {
                Name = "Done",
                Result = CommandResult.GoHome(),
            },
            Title = "Answer",
            Description = Riddle.Answer,

        };

        Commands = [
            new CommandContextItem(
                title: "Do thing",
                name: "Press Enter for answer",
                result: CommandResult.Confirm(confirmArgs),
                action: () => { 
                    _currentRiddle = null;
                }),
        ];


        return [
            new MarkdownContent($"{Riddle.Text}"),
            new MarkdownContent("\n"),
            new MarkdownContent("Press Enter for the answer "),
        ];
    }

    [RequiresUnreferencedCode("Calls System.Text.Json.JsonSerializer.Deserialize<TValue>(String, JsonSerializerOptions)")]
    [RequiresDynamicCode("Calls System.Text.Json.JsonSerializer.Deserialize<TValue>(String, JsonSerializerOptions)")]
    private static async Task<Riddle?> GetRiddleAsync()
    {
        try
        {
            Client.DefaultRequestHeaders.Accept.Clear();

            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Make a GET request to the uselessfacts.jsph.pl API
            var response = await Client
                .GetAsync("https://riddles-api.vercel.app/random");
            response.EnsureSuccessStatusCode();

            // Read and deserialize the response JSON into a Fact object
            var responseBody = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseBody);
            var root = doc.RootElement;
            string text = root.GetProperty("riddle").GetString() ?? "";
            string answer = root.GetProperty("answer").GetString() ?? "";

            var riddle = new Riddle
            {
                Text = text,
                Answer = answer
            };

            return riddle;

        }
        catch (Exception e)
        {
            Debug.WriteLine($"An error occurred: {e.Message}");
            return new Riddle()
            {
                Text = "An error occurred while fetching the riddle. Please try again later.",
                Answer = string.Empty
            };
        }
    }
}

