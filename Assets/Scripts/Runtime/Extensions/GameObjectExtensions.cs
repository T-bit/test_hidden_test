using JetBrains.Annotations;
using UnityEngine;

namespace HiddenTest.Extensions
{
    [PublicAPI]
    public static class GameObjectExtensions
    {
        public static void Destroy(this GameObject self)
        {
            Object.Destroy(self);
        }
    }
}