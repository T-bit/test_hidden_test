using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace HiddenTest.Extensions
{
    [PublicAPI]
    public static class UniTaskExtensions
    {
        public static UniTask WhenAll(this IEnumerable<UniTask> self)
        {
            return UniTask.WhenAll(self);
        }
    }
}