using System.Threading;
using Cysharp.Threading.Tasks;
using HiddenTest.UI;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HiddenTest.Services
{
    [UsedImplicitly]
    public sealed class UIService : Service<UIServiceSettings>, IUIService
    {
        private UIModule _uiModule;

        public UIService(UIServiceSettings settings, Transform rootTransform, IObjectResolver container)
            : base(settings, rootTransform, container)
        {
        }

        protected override UniTask OnStartAsync(CancellationToken cancellationToken)
        {
            _uiModule = Container.Instantiate(Settings.UIModulePrefab, RootTransform);

            return UniTask.CompletedTask;
        }

        protected override void OnDispose()
        {

        }
    }
}