using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace HiddenTest.UI
{
    public abstract class FragmentView : MonoBehaviour
    {
        protected virtual UniTask OnShowAsync(CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        protected virtual UniTask OnHideAsync(CancellationToken cancellationToken)
        {
            return UniTask.CompletedTask;
        }

        public UniTask ShowAsync(CancellationToken cancellationToken)
        {
            // TODO: Serialized operation provider
            return OnShowAsync(cancellationToken);
        }

        public UniTask HideAsync(CancellationToken cancellationToken)
        {
            // TODO: Serialized operation provider
            return OnHideAsync(cancellationToken);
        }
    }

    public abstract class FragmentView<TFragmentModel> : FragmentView
        where TFragmentModel : FragmentModel
    {
        protected TFragmentModel Model
        {
            get;
            private set;
        }

        [Inject]
        private void Initialize(TFragmentModel model)
        {
            Model = model;
        }
    }
}