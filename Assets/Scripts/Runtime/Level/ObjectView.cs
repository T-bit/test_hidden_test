using System.Threading;
using Cysharp.Threading.Tasks;
using HiddenTest.Extensions;
using HiddenTest.Input;
using LitMotion;
using UnityEngine;

namespace HiddenTest.Level
{
    public sealed class ObjectView : MonoBehaviour, IClickable
    {
        [SerializeField]
        // TODO: DisabledAttribute
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        // TODO: DisabledAttribute
        private Collider2D _collider;

        // That'll work for now
        public string Id => _spriteRenderer?.sprite?.name;

        public UniTask HideAsync(CancellationToken cancellationToken)
        {
            using var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, destroyCancellationToken);
            _collider.enabled = false;

            // TODO: Serialized operation provider
            return LMotion.Create(1f, 0f, 0.3f)
                          .WithEase(Ease.OutSine)
                          .Bind(value => _spriteRenderer.SetColorAlpha(value))
                          .ToUniTask(cancellationTokenSource.Token);
        }

        #region Unity

        private void OnValidate()
        {
            gameObject.SetSelfComponent(ref _spriteRenderer);
            gameObject.SetSelfComponent(ref _collider);
        }

        #endregion
    }
}