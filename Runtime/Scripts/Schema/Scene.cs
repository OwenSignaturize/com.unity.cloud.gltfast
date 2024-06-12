// SPDX-FileCopyrightText: 2023 Unity Technologies and the glTFast authors
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace GLTFast.Schema
{

    /// <summary>
    /// Scene, the top level hierarchy object.
    /// </summary>
    [System.Serializable]
    public class Scene : NamedObject, IJsonWritable
    {

        /// <summary>
        /// The indices of all root nodes
        /// </summary>
        public uint[] nodes;

        public Dictionary<string, object> extras; //MUST BE FLOAT ARRAY

        internal void GltfSerialize(JsonWriter writer)
        {
            writer.AddObject();
            GltfSerializeName(writer);
            writer.AddArrayProperty("nodes", nodes);
            
            if (extras != null && extras.Count > 0)
            {
                writer.AddProperty("extras");
                writer.AddObject();
                foreach (var kvp in extras)
                {
                    if (extras.TryGetValue(kvp.Key, out object val))
                    {
                        writer.AddArrayProperty(kvp.Key, (float[]) val);
                    }
                }
                writer.Close();
            }
            writer.Close();
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
