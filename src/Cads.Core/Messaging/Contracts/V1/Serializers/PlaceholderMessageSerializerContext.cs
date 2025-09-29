using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Cads.Core.Messaging.Contracts.V1.Serializers;

[ExcludeFromCodeCoverage]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    Converters = []
)]
[JsonSerializable(typeof(PlaceholderMessage))]
public partial class PlaceholderMessageSerializerContext : JsonSerializerContext
{
}