using System;
using System.Diagnostics;
using JetBrains.Annotations;
using UnityEngine;

namespace HiddenTest.Attributes
{
    [Conditional("UNITY_EDITOR")]
    [AttributeUsage(AttributeTargets.Field)]
    [PublicAPI]
    public sealed class SerializeReferencePickerAttribute : PropertyAttribute
    {
    }
}