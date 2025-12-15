using System.Collections.Generic;
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

        public UIService(UIModule uiModule, IEnumerable<IFragment> fragments, Transform rootTransform, IObjectResolver container)
            : base(rootTransform, container)
        {
            _uiModule = uiModule;

            foreach (var fragment in fragments)
            {
                Debug.Log(fragment.GetType().Name);
            }
        }

        protected override UniTask OnStartAsync(CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        protected override void OnDispose()
        {

        }

        public TFragment GetFragment<TFragment>()
            where TFragment : IFragment
        {
            Debug.Log($"GetFragment {typeof(TFragment).Name})");

            return default;
        }
    }
}