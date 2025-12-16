using JetBrains.Annotations;

namespace HiddenTest.UI
{
    [UsedImplicitly]
    public sealed class MessageWindowModel : FragmentModel
    {
        public string Message { get; set; }
    }
}