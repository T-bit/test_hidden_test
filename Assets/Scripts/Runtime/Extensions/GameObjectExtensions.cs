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

        public static void SetSelfComponent<TComponent>(this GameObject self, ref TComponent component)
            where TComponent : Component
        {
            if (component != null)
            {
                return;
            }

            component =  self.GetComponent<TComponent>();

            if (component == null)
            {
                Debug.LogError($"Missing component of type {typeof(TComponent)} on {self.name}");
            }
        }
    }
}