using System.Collections.Generic;
using HiddenTest.Level;
using JetBrains.Annotations;
using UnityEngine.Scripting;

namespace HiddenTest.UI
{
    [UsedImplicitly]
    [Preserve]
    public sealed class LevelScreenModel : FragmentModel
    {
        public float Timer { get; set; }

        public IReadOnlyList<ObjectSettings>  Objects { get; set; }
    }
}