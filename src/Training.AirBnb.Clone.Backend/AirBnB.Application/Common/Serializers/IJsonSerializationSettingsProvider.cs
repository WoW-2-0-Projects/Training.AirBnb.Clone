using Newtonsoft.Json;

namespace AirBnB.Application.Common.Serializers;

public interface IJsonSerializationSettingsProvider
{
    JsonSerializerSettings Get(bool clone = false);
}