using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Cads.Core.Messaging.Contracts.Serializers;

[ExcludeFromCodeCoverage]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.Unspecified,
    Converters = []
)]
[JsonSerializable(typeof(SnsEnvelope))]
public partial class SnsEnvelopeSerializerContext : JsonSerializerContext
{
}