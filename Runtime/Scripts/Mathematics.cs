// SPDX-FileCopyrightText: 2023 Unity Technologies and the glTFast authors
// SPDX-License-Identifier: Apache-2.0

using System;
using UnityEngine;
using static Unity.Mathematics.math;

namespace GLTFast
{

    using Unity.Mathematics;

    /// <summary>
    /// Mathematics helper methods
    /// </summary>
    public static class Mathematics
    {
        /// <summary>
        /// Decomposes a 4x4 TRS matrix into separate transforms (translation * rotation * scale)
        /// Matrix may not contain skew
        /// </summary>
        /// <param name="m">Input matrix</param>
        /// <param name="translation">Translation</param>
        /// <param name="rotation">Rotation</param>
        /// <param name="scale">Scale</param>
        public static void Decompose(
            this Matrix4x4 m,
            out Vector3 translation,
            out Quaternion rotation,
            out Vector3 scale
            )
        {
            translation = new Vector3(m.m03, m.m13, m.m23);
            var mRotScale = new float3x3(
                m.m00, m.m01, m.m02,
                m.m10, m.m11, m.m12,
                m.m20, m.m21, m.m22
                );
            mRotScale.Decompose(out var mRotation, out var mScale);
            rotation = mRotation;
            scale = new Vector3(mScale.x, mScale.y, mScale.z);
        }

        /// <summary>
        /// Decomposes a 4x4 TRS matrix into separate transforms (translation * rotation * scale)
        /// Matrix may not contain skew
        /// </summary>
        /// <param name="m">Input matrix</param>
        /// <param name="translation">Translation</param>
        /// <param name="rotation">Rotation</param>
        /// <param name="scale">Scale</param>
        public static void Decompose(
            this float4x4 m,
            out float3 translation,
            out quaternion rotation,
            out float3 scale
            )
        {
            var mRotScale = new float3x3(
                m.c0.xyz,
                m.c1.xyz,
                m.c2.xyz
                );
            mRotScale.Decompose(out rotation, out scale);
            translation = m.c3.xyz;
        }

        /// <summary>
        /// Decomposes a 3x3 matrix into rotation and scale
        /// </summary>
        /// <param name="m">Input matrix</param>
        /// <param name="rotation">Rotation quaternion values</param>
        /// <param name="scale">Scale</param>
        static void Decompose(this float3x3 m, out quaternion rotation, out float3 scale)
        {
            var lenC0 = length(m.c0);
            var lenC1 = length(m.c1);
            var lenC2 = length(m.c2);

            float3x3 rotationMatrix;
            rotationMatrix.c0 = m.c0 / lenC0;
            rotationMatrix.c1 = m.c1 / lenC1;
            rotationMatrix.c2 = m.c2 / lenC2;

            scale.x = lenC0;
            scale.y = lenC1;
            scale.z = lenC2;

            if (rotationMatrix.IsNegative())
            {
                rotationMatrix *= -1f;
                scale *= -1f;
            }

            // Inlined normalize(rotationMatrix)
            rotationMatrix.c0 = math.normalize(rotationMatrix.c0);
            rotationMatrix.c1 = math.normalize(rotationMatrix.c1);
            rotationMatrix.c2 = math.normalize(rotationMatrix.c2);

            rotation = new quaternion(rotationMatrix);
        }

        static bool IsNegative(this float3x3 m)
        {
            var cross = math.cross(m.c0, m.c1);
            return math.dot(cross, m.c2) < 0f;
        }

        /// <summary>
        /// Normalizes a vector
        /// </summary>
        /// <param name="input">Input vector</param>
        /// <param name="output">Normalized output vector</param>
        /// <returns>Length/magnitude of input vector</returns>
        public static float Normalize(float2 input, out float2 output)
        {
            var len = math.length(input);
            output = input / len;
            return len;
        }

        /// <inheritdoc cref="Decompose(float4x4,out float3,out quaternion,out float3)"/>
        [Obsolete("Use Decompose overload with rotation parameter of type quaternion.")]
        public static void Decompose(
            this float4x4 m,
            out float3 translation,
            out float4 rotation,
            out float3 scale
        )
        {
            m.Decompose(out translation, out quaternion rotationQuaternion, out scale);
            rotation = rotationQuaternion.value;
        }
    }
}
