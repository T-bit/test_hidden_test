using System.Threading;
using Cysharp.Threading.Tasks;
using HiddenTest.Extensions;
using HiddenTest.Level;
using HiddenTest.Services;
using LitMotion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace HiddenTest.UI
{
    public sealed class LevelObjectView : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _imageContainer;

        [SerializeField]
        private Image _image;

        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private CanvasGroup _canvasGroup;

        public int Index
        {
            get;
            private set;
        }

        [Inject]
        private void Initialize(LevelObjectViewSettings settings)
        {
            _text.gameObject.SetActive(settings.ShowName);
            _imageContainer.gameObject.SetActive(settings.ShowImage);
        }

        public void Set(ObjectSettings objectSettings, int index)
        {
            _image.sprite = objectSettings.Sprite;
            _text.text = objectSettings.Name;
            Index = index;
        }

        public UniTask ShowAsync(CancellationToken cancellationToken)
        {
            using var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, destroyCancellationToken);

            // TODO: Serialized operation provider
            return LMotion.Create(0f, 1f, 0.3f)
                          .WithEase(Ease.InSine)
                          .Bind(value => _canvasGroup.alpha = value)
                          .ToUniTask(cancellationTokenSource.Token);
        }

        public UniTask HideAsync(CancellationToken cancellationToken)
        {
            using var cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, destroyCancellationToken);

            // TODO: Serialized operation provider
            return LMotion.Create(1f, 0f, 0.3f)
                          .WithEase(Ease.OutSine)
                          .Bind(value => _canvasGroup.alpha = value)
                          .ToUniTask(cancellationTokenSource.Token);
        }

        #region Unity

        private void OnValidate()
        {
            gameObject.SetSelfComponent(ref _canvasGroup);
        }

        #endregion
    }
}