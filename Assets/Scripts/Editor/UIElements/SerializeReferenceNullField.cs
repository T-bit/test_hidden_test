using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace HiddenTest.UIElements
{
    public sealed class SerializeReferenceNullField : VisualElement
    {
        private const int CompensationMargin = 12;

        public SerializeReferenceNullField(SerializedProperty property, List<Type> types, Action<Type> typeValueChangedCallback)
        {
            Assert.IsTrue(property.propertyType == SerializedPropertyType.ManagedReference);

            style.flexDirection = FlexDirection.Row;
            style.marginLeft = -CompensationMargin;

            var propertyField = new PropertyField(property);
            var typePopupField = new TypePopupField(types, typeValueChangedCallback);


            propertyField.style.flexGrow = 1;
            propertyField.style.marginLeft = CompensationMargin;

            Add(propertyField);
            Add(typePopupField);
        }
    }
}