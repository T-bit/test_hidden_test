using System.Threading;
using Cysharp.Threading.Tasks;
using HiddenTest.Level;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HiddenTest.UI
{
    public sealed class LevelObjectView : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        [SerializeField]
        private TMP_Text _text;

        public int Index
        {
            get;
            private set;
        }

        public void Set(ObjectSettings objectSettings, int index)
        {
            _image.sprite = objectSettings.Sprite;
            _text.text = objectSettings.Name;
            Index = index;
        }

        public UniTask ShowAsync(CancellationToken cancellationToken)
        {
            // TODO: Operation provider
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public UniTask HideAsync(CancellationToken cancellationToken)
        {
            // TODO: Operation provider
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}