using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Security.Transversal.Common.JsonConverter
{
    public class TrimStringConverter : JsonConverter<string>
    {
        private static readonly Regex SpaceRemover = new(@"\s+", RegexOptions.Compiled);

        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return TrimInputField(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }

        public string TrimInputField(string input)
        {
            if (!string.IsNullOrEmpty(input))
                input = input.Trim();

            return input;
        }

    }
}
