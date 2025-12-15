using System.Threading;
using Cysharp.Threading.Tasks;
using HiddenTest.UI;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;

namespace HiddenTest.Services
{
    [UsedImplicitly]
    public sealed class UIService : Service, IUIService
    {
        private UIModule _uiModule;

        public UIService(UIModule uiModule, Transform rootTransform, IObjectResolver container)
            : base(rootTransform, container)
        {
            _uiModule = uiModule;
        }

        protected override UniTask OnStartAsync(CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        protected override void OnDispose()
        {

        }
    }
}