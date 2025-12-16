using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace HiddenTest.UI
{
    public abstract class Fragment : IFragment
    {
        private readonly FragmentModel _model;
        private readonly FragmentView _view;

        private event Action<IFragment> BeforeShow;
        private event Action<IFragment> AfterHide;

        protected Fragment(FragmentModel model, FragmentView  view)
        {
            _model = model;
            _view = view;
        }

        #region IFragment

        event Action<IFragment> IFragment.BeforeShow
        {
            add => BeforeShow += value;
            remove => BeforeShow -= value;
        }

        event Action<IFragment> IFragment.AfterHide
        {
            add => AfterHide += value;
            remove => AfterHide -= value;
        }

        FragmentModel IFragment.Model => _model;
        FragmentView IFragment.View => _view;

        async UniTask IFragment.ShowAsync(CancellationToken cancellationToken)
        {
            BeforeShow?.Invoke(this);
            await _view.ShowAsync(cancellationToken);
        }

        async UniTask IFragment.HideAsync(CancellationToken cancellationToken)
        {
            await _view.HideAsync(cancellationToken)
                       .SuppressCancellationThrow();
            AfterHide?.Invoke(this);
            cancellationToken.ThrowIfCancellationRequested();
        }

        #endregion
    }

    public abstract class Fragment<TFragmentModel, TFragmentView> : Fragment, IFragment<TFragmentModel, TFragmentView>
        where TFragmentModel : FragmentModel
        where TFragmentView : FragmentView<TFragmentModel>
    {
        protected readonly TFragmentModel Model;
        protected readonly TFragmentView View;

        protected Fragment(TFragmentModel model, TFragmentView view)
            : base(model, view)
        {
            Model = model;
            View = view;
        }

        #region IFragment<TFragmentModel, TFragmentView>

        TFragmentModel IFragment<TFragmentModel, TFragmentView>.Model => Model;
        TFragmentView IFragment<TFragmentModel, TFragmentView>.View => View;

        #endregion
    }
}