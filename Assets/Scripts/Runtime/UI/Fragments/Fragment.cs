using System.Threading;
using Cysharp.Threading.Tasks;

namespace HiddenTest.UI
{
    public abstract class Fragment : IFragment
    {
        protected abstract UniTask OnShowAsync();
        protected abstract UniTask OnHideAsync();

        #region IFragment

        UniTask IFragment.ShowAsync(CancellationToken cancellationToken)
        {
            return OnShowAsync();
        }

        UniTask IFragment.HideAsync(CancellationToken cancellationToken)
        {
            return OnHideAsync();
        }

        #endregion
    }

    public abstract class Fragment<TFragmentModel, TFragmentView> : Fragment
        where TFragmentModel : FragmentModel
        where TFragmentView : FragmentView<TFragmentModel>
    {
        protected readonly TFragmentModel Model;
        protected readonly TFragmentView View;

        protected Fragment(TFragmentModel model, TFragmentView  view)
        {
            Model = model;
            View = view;
        }

        protected override UniTask OnShowAsync()
        {
            throw new System.NotImplementedException();
        }

        protected override UniTask OnHideAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}