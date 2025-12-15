using System;
using HiddenTest.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HiddenTest.Services
{
    [Serializable]
    public sealed class UIServiceInstaller : ServiceInstaller<UIService>
    {
        [SerializeField]
        private UIModule _uiModulePrefab;

        protected override void OnInstall(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(_uiModulePrefab, Lifetime.Scoped)
                   .UnderTransform(RootTransform);

            base.OnInstall(builder);
        }
    }
}