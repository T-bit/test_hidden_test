using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace HiddenTest.UI
{
    public abstract class FragmentView : MonoBehaviour
    {
        public UniTask ShowAsync(CancellationToken cancellationToken)
        {
            // TODO: Operation provider
            return UniTask.CompletedTask;
        }

        public UniTask HideAsync(CancellationToken cancellationToken)
        {
            // TODO: Operation provider
            return UniTask.CompletedTask;
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