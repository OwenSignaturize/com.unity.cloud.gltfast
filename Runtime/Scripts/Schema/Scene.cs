// SPDX-FileCopyrightText: 2023 Unity Technologies and the glTFast authors
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace GLTFast.Schema
{

    /// <summary>
    /// Scene, the top level hierarchy object.
    /// </summary>
    [System.Serializable]
    public class Scene : NamedObject
    {

        /// <summary>
        /// The indices of all root nodes
        /// </summary>
        public uint[] nodes;

        public Dictionary<string, object> extras;

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
                    writer.AddProperty(kvp.Key, kvp.Value.ToString());
                }
                writer.Close();
            }
            writer.Close();
        }
    }
}
