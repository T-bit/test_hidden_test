using System.Threading;
using Cysharp.Threading.Tasks;
using HiddenTest.Extensions;
using LitMotion;
using TMPro;
using UnityEngine;

namespace HiddenTest.UI
{
    public sealed class MessageWindowView : FragmentView<MessageWindowModel>
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        private TMP_Text _messageText;

        protected override UniTask OnShowAsync(CancellationToken cancellationToken)
        {
            _messageText.text = Model.Message;

            return LMotion.Create(0f, 1f, 0.3f)
                          .WithEase(Ease.InSine)
                          .Bind(value => _canvasGroup.alpha = value)
                          .ToUniTask(cancellationToken);
        }

        protected override UniTask OnHideAsync(CancellationToken cancellationToken)
        {
            return LMotion.Create(1f, 0f, 0.3f)
                          .WithEase(Ease.OutSine)
                          .Bind(value => _canvasGroup.alpha = value)
                          .ToUniTask(cancellationToken);
        }

        #region Unity

        private void OnValidate()
        {
            gameObject.SetSelfComponent(ref _canvasGroup);
        }

        #endregion
    }
}