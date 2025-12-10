using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace HiddenTest.StateControllers
{
    [Serializable]
    public abstract class StateController : IAsyncStartable, IDisposable
    {
        protected IObjectResolver Container
        {
            get;
        }

        protected Transform RootTransform
        {
            get;
        }

        protected StateController(Transform rootTransform, IObjectResolver container)
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
}