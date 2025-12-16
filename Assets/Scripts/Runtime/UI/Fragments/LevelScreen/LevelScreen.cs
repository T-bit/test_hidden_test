using JetBrains.Annotations;

namespace HiddenTest.UI
{
    [UsedImplicitly]
    public sealed class LevelScreen : Fragment<LevelScreenModel,  LevelScreenView>, ILevelScreen
    {
        public LevelScreen(LevelScreenModel model, LevelScreenView view)
            : base(model, view)
        {
        }
    }
}