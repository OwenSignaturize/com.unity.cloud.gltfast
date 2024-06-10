// SPDX-FileCopyrightText: 2023 Unity Technologies and the glTFast authors
// SPDX-License-Identifier: Apache-2.0

namespace GLTFast.Schema
{

    /// <summary>
    /// glTF root extensions
    /// </summary>
    [System.Serializable]
    public class RootExtensions
    {

        /// <inheritdoc cref="LightsPunctual"/>
        // ReSharper disable once InconsistentNaming
        public LightsPunctual KHR_lights_punctual;

        /// <inheritdoc cref="MaterialsVariantsRootExtension"/>
        // ReSharper disable once InconsistentNaming
        public MaterialsVariantsRootExtension KHR_materials_variants;

        internal void GltfSerialize(JsonWriter writer)
        {
            writer.AddObject();
            if (KHR_lights_punctual != null)
            {
                writer.AddProperty("KHR_lights_punctual");
                KHR_lights_punctual.GltfSerialize(writer);
            }
            if (KHR_materials_variants != null)
            {
                writer.AddProperty("KHR_materials_variants");
                KHR_materials_variants.GltfSerialize(writer);
            }
            writer.Close();
        }

        /// <summary>
        /// Cleans up invalid parsing artifacts created by <see cref="GltfJsonUtilityParser"/>.
        /// </summary>
        /// <returns>True if element itself still holds value. False if it can be safely removed.</returns>
        public virtual bool JsonUtilityCleanup()
        {
            if (KHR_lights_punctual != null && !KHR_lights_punctual.JsonUtilityCleanup())
            {
                KHR_lights_punctual = null;
            }

            if (KHR_materials_variants != null && !KHR_materials_variants.JsonUtilityCleanup())
            {
                KHR_materials_variants = null;
            }

            return KHR_lights_punctual != null
                || KHR_materials_variants != null;
        }
    }
}
