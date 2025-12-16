using System.Threading;
using Cysharp.Threading.Tasks;
using HiddenTest.Level;

namespace HiddenTest.UI
{
    public interface ILevelScreen : IFragment<LevelScreenModel, LevelScreenView>
    {
        public void SetTimer(float timerSeconds);

        public UniTask ShowObjectAsync(int index, CancellationToken cancellationToken);

        public UniTask HideObjectAsync(int index, CancellationToken cancellationToken);
    }
}