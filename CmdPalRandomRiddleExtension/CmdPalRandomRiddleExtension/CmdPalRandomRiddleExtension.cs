// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices; 
using System.Threading;
using Microsoft.CommandPalette.Extensions;

namespace CmdPalRandomRiddleExtension;

[Guid("a12abc99-3f75-4056-b2e1-7e8bf2705649")]
public sealed partial class CmdPalRandomRiddleExtension : IExtension, IDisposable
{
    private readonly ManualResetEvent _extensionDisposedEvent;

    private readonly CmdPalRandomRiddleExtensionCommandsProvider _provider = new();

    public CmdPalRandomRiddleExtension(ManualResetEvent extensionDisposedEvent)
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
