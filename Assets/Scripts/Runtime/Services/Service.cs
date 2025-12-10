using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HiddenTest.Services
{
    public abstract class Service : IService
    {
        protected IObjectResolver Container
        {
            get;
        }

        protected Transform RootTransform
        {
            get;
        }

        protected Service(Transform rootTransform, IObjectResolver container)
        {
            Container = container;
            RootTransform = rootTransform;
        }

        protected virtual UniTask OnStartAsync(CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        protected virtual void OnDispose()
        {
        }

        #region IAsyncStartable

        UniTask IAsyncStartable.StartAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return OnStartAsync(cancellationToken);
        }

        #endregion

        #region IDisposable

        void IDisposable.Dispose()
        {
            OnDispose();
        }

        #endregion
    }

    public class Service<TSettings> : Service
        where TSettings : IServiceSettings
    {
        protected TSettings Settings
        {
            get;
        }

        public Service(TSettings serviceSettings, Transform rootTransform, IObjectResolver container)
            : base(rootTransform, container)
        {
            Settings = serviceSettings;
        }
    }
}