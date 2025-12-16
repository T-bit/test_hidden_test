using System;
using HiddenTest.Attributes;
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

        [SerializeReference]
        [SerializeReferencePicker]
        private IFragmentInstaller[]  _fragmentInstallers;

        protected override void OnInstall(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(_uiModulePrefab, Lifetime.Scoped)
                   .UnderTransform(RootTransform);

            foreach (var installer in _fragmentInstallers)
            {
                installer.Install(builder);
            }

            base.OnInstall(builder);
        }
    }
}