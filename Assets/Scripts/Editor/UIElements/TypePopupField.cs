using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace HiddenTest.UIElements
{
    public sealed class TypePopupField : PopupField<Type>
    {
        private readonly Action<Type> _valueChangedCallback;

        public TypePopupField(List<Type> types, Action<Type> valueChangedCallback, string label = null)
            : base(label, types, 0, GetTypeName, GetTypeName)
        {
            _valueChangedCallback = valueChangedCallback;
            this.RegisterValueChangedCallback(OnValueChanged);
            style.width = Length.Percent(50);
        }

        private void OnValueChanged(ChangeEvent<Type> changeEvent)
        {
            _valueChangedCallback?.Invoke(changeEvent.newValue);
        }

        private static string GetTypeName(Type type)
        {
            return type == null ? "Null" : type.FullName;
        }
    }
}