using System.Collections.Generic;
using System.Linq;
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
        private readonly List<IFragment> _fragments;
        private UIModule _uiModule;

        private RectTransform EnabledContainer => _uiModule.EnabledContainer;
        private RectTransform DisabledContainer => _uiModule.DisabledContainer;

        [UnityEngine.Scripting.Preserve]
        public UIService(UIModule uiModule, IEnumerable<IFragment> fragments, Transform rootTransform, IObjectResolver container)
            : base(rootTransform, container)
        {
            _uiModule = uiModule;
            _fragments = new List<IFragment>(fragments);

            foreach (var fragment in _fragments)
            {
                fragment.BeforeShow += ShowFragment;
                fragment.AfterHide += HideFragment;
                HideFragment(fragment);
            }
        }

        protected override UniTask OnStartAsync(CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        protected override void OnDispose()
        {
            foreach (var fragment in _fragments)
            {
                fragment.BeforeShow -= ShowFragment;
                fragment.AfterHide -= HideFragment;
                HideFragment(fragment);
            }

            _fragments.Clear();
            _uiModule = null;
        }

        private void ShowFragment(IFragment fragment)
        {
            fragment.View.transform.SetParent(EnabledContainer, false);
        }

        private void HideFragment(IFragment fragment)
        {
            fragment.View.transform.SetParent(DisabledContainer, false);
        }

        #region IUIService

        TFragment IUIService.GetFragment<TFragment>()
        {
            return _fragments.OfType<TFragment>()
                             .Single();
        }

        #endregion
    }
}