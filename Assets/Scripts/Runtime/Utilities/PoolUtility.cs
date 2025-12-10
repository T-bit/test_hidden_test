using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Assertions;

namespace HiddenTest.Utilities
{
    [PublicAPI]
    public static class PoolUtility<T>
        where T : class, new()
    {
        private static readonly Stack<T> Stack = new();

        public static void Push(T value)
        {
            Assert.IsFalse(Stack.Contains(value));
            Stack.Push(value);
        }

        public static T Pull()
        {
            return Stack.Count > 0
                ? Stack.Pop()
                : new T();
        }
    }

    [PublicAPI]
    public static class PoolUtility
    {
        public static void PushList<T>(List<T> value)
        {
            PoolUtility<List<T>>.Push(value);
        }

        public static List<T> PullList<T>()
        {
            return PoolUtility<List<T>>.Pull();
        }
    }
}