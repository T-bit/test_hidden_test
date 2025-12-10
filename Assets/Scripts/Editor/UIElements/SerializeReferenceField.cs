using System;
using System.Collections.Generic;
using HiddenTest.Extensions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace HiddenTest.UIElements
{
    public sealed class SerializeReferenceField : Foldout
    {
        private Toggle _toggle;
        private VisualElement _checkmark;

        private Toggle Toggle => _toggle ??= this.Q<Toggle>();

        public SerializeReferenceField(SerializedProperty property, List<Type> types, Action<Type> typeValueChangedCallback)
        {
            Assert.IsTrue(property.propertyType == SerializedPropertyType.ManagedReference);

            text = ObjectNames.NicifyVariableName(property.displayName);
            this.BindProperty(property);

            var typePopupField = new TypePopupField(types, typeValueChangedCallback);

            Toggle.Add(typePopupField);

            foreach (var childProperty in property.Copy().GetChildProperties())
            {
                var propertyField = new PropertyField(childProperty);

                contentContainer.Add(propertyField);
            }
        }
    }
}