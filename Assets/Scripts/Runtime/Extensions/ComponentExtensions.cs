using JetBrains.Annotations;
using UnityEngine;

namespace HiddenTest.Extensions
{
    [PublicAPI]
    public static class ComponentExtensions
    {
        public static void Destroy(this Component self)
        {
            Object.Destroy(self);
        }

        public static void Destroy(this Component self, bool gameObject)
        {
            if (gameObject)
            {
                self.gameObject.Destroy();
            }
            else
            {
                self.Destroy();
            }
        }
    }
}