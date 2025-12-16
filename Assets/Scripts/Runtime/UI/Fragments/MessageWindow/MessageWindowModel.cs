using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace HiddenTest.UI
{
    [UsedImplicitly]
    [Preserve]
    public sealed class MessageWindowModel : FragmentModel
    {
        public string Message { get; set; }
    }
}