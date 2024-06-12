// SPDX-FileCopyrightText: 2023 Unity Technologies and the glTFast authors
// SPDX-License-Identifier: Apache-2.0

#if NEWTONSOFT_JSON

using System.Collections.Generic;

using GLTFast.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.Scripting;
using JsonWriter = GLTFast.Schema.JsonWriter;

namespace GLTFast.Newtonsoft.Schema
{
    public class Scene : GLTFast.Schema.Scene, IJsonObject, IJsonWritable
    {
        // public UnclassifiedData extras;
        public UnclassifiedData extensions;

        public Dictionary<string, object> extras;

        [JsonExtensionData]
        IDictionary<string, JToken> m_JsonExtensionData;

        [Preserve]
        public Scene() {}

        public bool TryGetValue<T>(string key, out T value)
        {
            if (m_JsonExtensionData != null
                && m_JsonExtensionData.TryGetValue(key, out var token))
            {
                value = token.ToObject<T>();
                return true;
            }

            value = default;
            return false;
        }

        public void Serialize(JsonWriter writer, string key)
        {
            if (extras!=null && extras.Count>0)
            {
                if (extras.TryGetValue(key, out object val))
                {
                    writer.AddArrayProperty(key, (float[])val );
                    writer.AddProperty(key);
                    writer.AddObject();
                    writer.Close();
                }
            }
        }
    }
}

#endif
