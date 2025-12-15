using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace HiddenTest.UI
{
    public interface IFragment
    {
        event Action<IFragment> BeforeShow;
        event Action<IFragment> AfterHide;

        FragmentModel Model { get; }
        FragmentView View { get; }

        UniTask ShowAsync(CancellationToken cancellationToken);
        UniTask HideAsync(CancellationToken cancellationToken);
    }

    public interface IFragment<out TFragmentModel, out TFragmentView> : IFragment
        where TFragmentModel : FragmentModel
        where TFragmentView : FragmentView<TFragmentModel>
    {
        new TFragmentModel Model { get; }
        new TFragmentView View { get; }
    }
}