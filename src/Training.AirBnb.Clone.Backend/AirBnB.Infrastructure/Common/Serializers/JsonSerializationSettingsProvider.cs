using AirBnB.Application.Common.Serializers;
using Force.DeepCloner;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AirBnB.Infrastructure.Common.Serializers;

/// <summary>
/// Implementation of the IJsonSerializationSettingsProvider interface.
/// Provides JSON serialization settings with default configurations.
/// </summary>
public class JsonSerializationSettingsProvider : IJsonSerializationSettingsProvider
{
    private readonly JsonSerializerSettings _jsonSerializerSettings = new()
    {
        // Configures the output JSON formatting for readability
        Formatting = Formatting.Indented,
        
        // Configures reference loops when serializing objects with circular references
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        
        // Configures the contract resolver to use camelCase for property names
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        
        //Ignores null values during serialization
        NullValueHandling = NullValueHandling.Ignore
    };

    public JsonSerializerSettings Get(bool clone = false) =>
        clone ? _jsonSerializerSettings.DeepClone() : _jsonSerializerSettings;
}