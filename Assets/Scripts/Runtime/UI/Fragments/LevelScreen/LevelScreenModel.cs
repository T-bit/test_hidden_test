using System.Collections.Generic;
using HiddenTest.Level;
using JetBrains.Annotations;

namespace HiddenTest.UI
{
    [UsedImplicitly]
    public sealed class LevelScreenModel : FragmentModel
    {
        public float Timer { get; set; }

        public IReadOnlyList<ObjectSettings>  Objects { get; set; }
    }
}