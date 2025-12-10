using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;

namespace HiddenTest.Extensions
{
    [PublicAPI]
    public static class SerializedPropertyExtensions
    {
        public static void SetManagedReferenceType(this SerializedProperty self, Type type)
        {
            var newValue = type == null ? null : Activator.CreateInstance(type);

            self.managedReferenceValue = newValue;
        }

        public static IEnumerable<SerializedProperty> GetChildProperties(this SerializedProperty self, int depth = 1)
        {
            var selfDepth = self.depth;

            foreach (var child in self)
            {
                if (child is not SerializedProperty childProperty ||
                    childProperty.depth > selfDepth + depth)
                {
                    continue;
                }

                yield return childProperty;
            }
        }
    }
}