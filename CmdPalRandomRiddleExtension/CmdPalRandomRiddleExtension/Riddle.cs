using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CmdPalRandomRiddleExtension;

public sealed class Riddle
{
    [JsonPropertyName("riddle")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("answer")]
    public string Answer { get; set; } = string.Empty;
}
