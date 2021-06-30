using System.Text.Json.Serialization;

namespace ArrearsApi.V1.Domain
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TargetType
    {
        estate, block, tenure
    }
}
