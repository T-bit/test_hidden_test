namespace HiddenTest.UI
{
    public sealed class LevelScreen : Fragment<LevelScreenModel,  LevelScreenView>, ILevelScreen
    {
        public LevelScreen(LevelScreenModel model, LevelScreenView view)
            : base(model, view)
        {
        }
    }
}