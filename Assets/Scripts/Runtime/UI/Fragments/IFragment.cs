using System.Threading;
using Cysharp.Threading.Tasks;

namespace HiddenTest.UI
{
    public interface IFragment
    {
        UniTask ShowAsync(CancellationToken cancellationToken);
        UniTask HideAsync(CancellationToken cancellationToken);
    }
}