using JetBrains.Annotations;
using UnityEngine;

namespace HiddenTest.Extensions
{
    [PublicAPI]
    public static class SpriteRendererExtensions
    {
        public static void SetColorAlpha(this SpriteRenderer self, float alpha)
        {
            self.color = self.color.SetAlpha(alpha);
        }
    }
}