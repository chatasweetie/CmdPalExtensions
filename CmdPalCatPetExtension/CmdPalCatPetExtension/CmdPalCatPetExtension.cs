// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.CommandPalette.Extensions;

namespace CmdPalCatPetExtension;

[Guid("96cfc015-d39e-485d-8d1d-e5eb82ba6e86")]
public sealed partial class CmdPalCatPetExtension : IExtension, IDisposable
{
    private readonly ManualResetEvent _extensionDisposedEvent;

    private readonly CmdPalCatPetExtensionCommandsProvider _provider = new();

    public CmdPalCatPetExtension(ManualResetEvent extensionDisposedEvent)
    {
        this._extensionDisposedEvent = extensionDisposedEvent;
    }

    public object? GetProvider(ProviderType providerType)
    {
        return providerType switch
        {
            ProviderType.Commands => _provider,
            _ => null,
        };
    }

    public void Dispose() => this._extensionDisposedEvent.Set();
}
