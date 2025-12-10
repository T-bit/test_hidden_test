using System;
using System.Collections.Generic;
using System.Linq;
using HiddenTest.Attributes;
using HiddenTest.Extensions;
using HiddenTest.UIElements;
using UnityEditor;
using UnityEngine.UIElements;

namespace HiddenTest.Drawers
{
    [CustomPropertyDrawer(typeof(SerializeReferencePickerAttribute))]
    public sealed class SerializeReferencePickerPropertyDrawer : PropertyDrawer
    {
        private readonly List<Type> _types = new();
        private SerializedProperty _property;

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            _property = property;

            var currentValue = _property.managedReferenceValue;

            GetTypesOnce();
            SortTypes(currentValue?.GetType());

            return currentValue == null
                ? new SerializeReferenceNullField(_property, _types, OnTypeValueChanged)
                : new SerializeReferenceField(_property, _types, OnTypeValueChanged);
        }

        private void GetTypesOnce()
        {
            if (_types.Count > 0)
            {
                return;
            }

            var fieldType = fieldInfo.FieldType;
            var baseType = fieldType.IsArray
                ? fieldType.GetElementType()
                : fieldType.IsGenericType && fieldType.GetGenericTypeDefinition() == typeof(List<>)
                    ? fieldType.GetGenericArguments()[0]
                    : fieldInfo.FieldType;

            _types.Add(null);

            if (IsSuitableType(fieldType))
            {
                _types.Add(fieldType);
            }

            var derivedTypes = TypeCache.GetTypesDerivedFrom(baseType)
                                        .Where(IsSuitableType);

            _types.AddRange(derivedTypes);
        }

        private void SortTypes(Type currentType)
        {
            _types.Sort(TypeComparison);

            return;

            int TypeComparison(Type type1, Type type2)
            {
                return type1 == currentType
                    ? -1
                    : type2 == currentType
                        ? 1
                        : string.Compare(type1?.FullName, type2?.FullName, StringComparison.Ordinal);
            }
        }

        private static bool IsSuitableType(Type type)
        {
            return type.IsSerializable &&
                   !type.IsAbstract &&
                   !type.IsInterface &&
                   !type.IsGenericType &&
                   !type.IsArray &&
                   !typeof(UnityEngine.Object).IsAssignableFrom(type);
        }

        private void OnTypeValueChanged(Type type)
        {
            _property.SetManagedReferenceType(type);
            _property.isExpanded = type != null;
            _property.serializedObject.ApplyModifiedProperties();

        }
    }
}