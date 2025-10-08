using System.Text.Json.Serialization;
using System.Text.Json;
using CmdPalCatPetExtension.Models;

namespace CmdPalCatPetExtension.Services
{
    [JsonSerializable(typeof(VirtualCat))]
    internal sealed partial class CatJsonContext : JsonSerializerContext
    {
    }
}
