namespace CommonLib.Collections
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;
    using UnityEngine;
    using UnityEngine.Assertions;
    using Object = UnityEngine.Object;

    [Serializable]
    public class DynamicSerializationTable : SafeDictionary<string, object>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private string json;

        [SerializeField]
        private StringToUnityObjectDictionary unityObjects = new StringToUnityObjectDictionary();

        public void OnBeforeSerialize()
        {
            this.unityObjects.Clear();
            this.json = "[]";

            try
            {
                StringWriter jsonBlobStringWriter = new StringWriter();
                using (JsonWriter jsonBlobWriter = new JsonTextWriter(jsonBlobStringWriter))
                {
                    jsonBlobWriter.Formatting = Formatting.None;

                    jsonBlobWriter.WriteStartArray();

                    foreach (KeyValuePair<string, object> propertyValuePair in this)
                    {
                        string propertyName = propertyValuePair.Key;
                        object propertyValue = propertyValuePair.Value;

                        if (propertyValue == null)
                        {
                            this.unityObjects.Add(propertyName, null);
                            continue;
                        }
                        else if (propertyValue is Object)
                        {
                            this.unityObjects.Add(propertyName, (Object)propertyValue);
                            continue;
                        }

                        jsonBlobWriter.WriteStartObject();

                        jsonBlobWriter.WritePropertyName("Type");
                        jsonBlobWriter.WriteValue(propertyValue.GetType().AssemblyQualifiedName);

                        jsonBlobWriter.WritePropertyName(propertyName);

                        if (propertyValue is string)
                        {
                            jsonBlobWriter.WriteValue((string) propertyValue);
                        }
                        else
                        {
                            jsonBlobWriter.WriteValue(JsonUtility.ToJson(propertyValue));
                        }

                        jsonBlobWriter.WriteEndObject();
                    }

                    jsonBlobWriter.WriteEndArray();
                }

                this.json = jsonBlobStringWriter.ToString();
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        public void OnAfterDeserialize()
        {
            this.Clear();

            if (this.json == "[]")
            {
                return;
            }

            try
            {
                var jsonBlobStringReader = new StringReader(this.json);
                using (JsonTextReader jsonBlobReader = new JsonTextReader(jsonBlobStringReader))
                {
                    this.ReadAndAssert(jsonBlobReader, JsonToken.StartArray);

                    while (jsonBlobReader.Read())
                    {
                        if (jsonBlobReader.TokenType == JsonToken.EndArray)
                        {
                            break;
                        }

                        Assert.AreEqual(jsonBlobReader.TokenType, JsonToken.StartObject);

                        this.ReadAndAssert(jsonBlobReader, JsonToken.PropertyName);
                        Assert.AreEqual((string)jsonBlobReader.Value, "Type");

                        string typeName = jsonBlobReader.ReadAsString();
                        Type valueType = ReflectionEx.GetTypeFromAllAssemblies(typeName);
                        Assert.IsNotNull(valueType);

                        this.ReadAndAssert(jsonBlobReader, JsonToken.PropertyName);

                        string propertyName = (string)jsonBlobReader.Value;

                        string propertyValueAsJson = jsonBlobReader.ReadAsString();

                        if (string.Equals(propertyValueAsJson, "null", StringComparison.OrdinalIgnoreCase))
                        {
                            this.Add(propertyName, null);
                        }
                        else
                        {
                            try
                            {
                                if (valueType == typeof(string))
                                {
                                    this.Add(propertyName, propertyValueAsJson);
                                }
                                else
                                {
                                    object propertyValue = JsonUtility.FromJson(propertyValueAsJson, valueType);
                                    this.Add(propertyName, propertyValue);
                                }
                            }
                            catch (Exception ex)
                            {
                                Debug.LogException(ex);
                            }
                        }

                        this.ReadAndAssert(jsonBlobReader, JsonToken.EndObject);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }

            foreach (KeyValuePair<string, Object> pair in this.unityObjects)
            {
                this.Add(pair.Key, pair.Value);
            }

            this.json = "[]";
        }

        private void ReadAndAssert(JsonTextReader reader, JsonToken token)
        {
            reader.Read();

            Assert.AreEqual(
                reader.TokenType,
                token,
                string.Format(
                    "Json is malformed at line {0} pos {1}",
                    reader.LineNumber,
                    reader.LinePosition));
        }
    }
}