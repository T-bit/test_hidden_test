using JetBrains.Annotations;
using UnityEngine;

namespace HiddenTest.Extensions
{
    [PublicAPI]
    public static class ColorExtensions
    {
        public static Color SetAlpha(this Color self, float alpha)
        {
            self.a = alpha;
            return self;
        }
    }
}