using System.Threading;
using Cysharp.Threading.Tasks;
using HiddenTest.Input;
using UnityEngine;

namespace HiddenTest.Level
{
    public sealed class ObjectView : MonoBehaviour, IClickable
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        // That'll work for now
        public string Id => _spriteRenderer?.sprite?.name;

        public UniTask HideAsync(CancellationToken cancellationToken)
        {
            //TODO
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}