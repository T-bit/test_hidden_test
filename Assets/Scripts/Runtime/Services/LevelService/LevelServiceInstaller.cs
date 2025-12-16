using System;
using HiddenTest.Level;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HiddenTest.Services
{
    [Serializable]
    public sealed class LevelServiceInstaller : ServiceInstaller<LevelService, LevelServiceSettings>
    {
        [SerializeField]
        private LevelModule _levelModulePrefab;

        [SerializeField]
        private LevelObjectViewSettings  _levelObjectViewSettings;

        protected override void OnInstall(IContainerBuilder builder)
        {
            builder.RegisterComponentInNewPrefab(_levelModulePrefab, Lifetime.Scoped)
                   .UnderTransform(RootTransform);
            builder.RegisterInstance(_levelObjectViewSettings);

            base.OnInstall(builder);
        }
    }
}