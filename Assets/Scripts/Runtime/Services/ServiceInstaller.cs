using System;
using HiddenTest.Attributes;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HiddenTest.Services
{
    [Serializable]
    public abstract class ServiceInstaller : IServiceInstaller
    {
        [NonSerialized]
        private Transform _rootTransform;

        protected Transform RootTransform => _rootTransform;

        protected abstract void OnInstall(IContainerBuilder builder);

        #region IServiceInstaller

        void IServiceInstaller.SetRootTransform(Transform rootTransform)
        {
            _rootTransform = rootTransform;
        }

        #endregion

        #region IInstaller

        void IInstaller.Install(IContainerBuilder builder)
        {
            OnInstall(builder);
        }

        #endregion
    }

    [Serializable]
    public abstract class ServiceInstaller<TService> : ServiceInstaller
      where TService : Service
    {
        protected override void OnInstall(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<TService>(Lifetime.Scoped)
                   .WithParameter(RootTransform);
        }
    }

    [Serializable]
    public abstract class ServiceInstaller<TService, TServiceSettings> : ServiceInstaller<TService>
        where TService : Service
        where TServiceSettings : ServiceSettings<TService>
    {
        [SerializeReference]
        [SerializeReferencePicker]
        private TServiceSettings _serviceSettings;

        protected override void OnInstall(IContainerBuilder builder)
        {
            builder.RegisterInstance(_serviceSettings).As(_serviceSettings.GetType());

            base.OnInstall(builder);
        }
    }
}