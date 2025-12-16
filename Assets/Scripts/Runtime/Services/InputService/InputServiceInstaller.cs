using System;
using HiddenTest.Input;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HiddenTest.Services
{
    [Serializable]
    public sealed class InputServiceInstaller : ServiceInstaller<InputService>
    {
        [SerializeField]
        private InputModule _inputModulePrefab;

        protected override void OnInstall(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(_inputModulePrefab, Lifetime.Scoped)
                   .UnderTransform(RootTransform);

            base.OnInstall(builder);
        }
    }
}