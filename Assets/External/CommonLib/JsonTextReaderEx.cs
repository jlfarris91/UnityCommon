using System;
using Newtonsoft.Json;
using UnityEngine.Assertions;

namespace Utility
{
    public static class JsonTextReaderEx
    {
        public static void AssertTokenType(this JsonTextReader reader, JsonToken token)
        {
            Assert.AreEqual(
                reader.TokenType,
                token,
                string.Format(
                    "Json is malformed at line {0} pos {1}",
                    reader.LineNumber,
                    reader.LinePosition));

        }

        public static string ReadProperty(this JsonTextReader reader, string propertyName)
        {
            reader.ReadAndAssert(JsonToken.PropertyName);
            string actualPropertyName = (string) reader.Value;

            if (!string.Equals(actualPropertyName, propertyName))
            {
                throw new JsonException(string.Format("Expected property name '{0}' but found '{1}' instead", propertyName,
                    actualPropertyName));
            }

            return reader.ReadAsString();
        }

        public static T ReadProperty<T>(this JsonTextReader reader, string propertyName, T defaultValue)
        {
            string propertyValueAsString = reader.ReadProperty(propertyName);

            object propertyValue = defaultValue;

            Type valueType = typeof(T);

            if (valueType.IsEnum)
            {
                try
                {
                    propertyValue = Enum.Parse(typeof(T), propertyValueAsString);
                }
                catch (Exception ex)
                {
                    throw new JsonException(string.Format("Could not convert value of property {0} to {1}",
                        propertyName, valueType.FullName), ex);
                }
            }
            else if (valueType == typeof(string))
            {
                propertyValue = propertyValueAsString;
            }
            else if (valueType == typeof(int))
            {
                int value;
                if (int.TryParse(propertyValueAsString, out value))
                {
                    propertyValue = value;
                }
            }
            else if (valueType == typeof(float))
            {
                float value;
                if (float.TryParse(propertyValueAsString, out value))
                {
                    propertyValue = value;
                }
            }

            else if (valueType == typeof(double))
            {
                double value;
                if (double.TryParse(propertyValueAsString, out value))
                {
                    propertyValue = value;
                }
            }
            else if (valueType == typeof(bool))
            {
                bool value;
                if (bool.TryParse(propertyValueAsString, out value))
                {
                    propertyValue = value;
                }
            }

            return (T)propertyValue;
        }

        public static void ReadAndAssert(this JsonTextReader reader, JsonToken token)
        {
            reader.Read();
            JsonTextReaderEx.AssertTokenType(reader, token);
        }

        public static void ReadAndAssertPropertyName(this JsonTextReader reader, string propertyName)
        {
            reader.ReadAndAssert(JsonToken.PropertyName);
            Assert.AreEqual(reader.Value, propertyName);
        }
    }
}
