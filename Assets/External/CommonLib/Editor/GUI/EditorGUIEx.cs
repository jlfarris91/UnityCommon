namespace CommonLib.Editor.GUI
{
    using System;
    using System.Reflection;
    using global::CommonLib;
    using UnityEditor;
    using UnityEngine;
    using Object = UnityEngine.Object;

    public static class EditorGUIEx
    {
        public static bool CanExposeGenericObject(Type type)
        {
            if (type == typeof(int) ||
                type == typeof(float) ||
                type == typeof(bool) ||
                type == typeof(string) ||
                type == typeof(Vector2) ||
                type == typeof(Vector3) ||
                type == typeof(Color) ||
                type.IsEnum ||
                ReflectionEx.IsSameOrSubclassOf(type, typeof(Object))
                )
            {
                return true;
            }

            return false;
        }

        public static void ExposeObject(Rect rect, object value)
        {
            if (value == null)
            {
                EditorGUI.HelpBox(rect, "Object is null", MessageType.None);
                return;
            }

            Type valueType = value.GetType();
            PropertyInfo[] properties = valueType.GetProperties();

            const float lineHeight = 20.0f;

            StackPanelUtility.Begin(rect, Axis.Vertical);

            foreach (PropertyInfo propInfo in properties)
            {
                Rect propertArea = StackPanelUtility.Reserve(lineHeight, rect);

                GUI.enabled = propInfo.CanWrite;

                object currentValue = propInfo.GetValue(value, null);

                object newValue = EditorGUIEx.ExposeGenericObject(propertArea, currentValue,
                    propInfo.PropertyType, new GUIContent(propInfo.Name));

                if (propInfo.CanWrite)
                {
                    propInfo.SetValue(value, newValue, null);
                }
            }

            GUI.enabled = true;

            StackPanelUtility.End();
        }

        public static object ExposeGenericObject(Rect rect, object value, Type type, GUIContent label)
        {
            if (type == typeof(int))
            {
                int oldValue = (int) value;
                int newValue = EditorGUI.IntField(rect, label, oldValue);
                if (oldValue != newValue)
                {
                    value = newValue;
                }
            }
            else if (type == typeof(float))
            {
                float oldValue = (float)value;
                float newValue = EditorGUI.FloatField(rect, label, oldValue);
                if (!Mathf.Approximately(oldValue, newValue))
                {
                    value = newValue;
                }
            }
            else if (type == typeof(bool))
            {
                bool oldValue = (bool)value;
                bool newValue = EditorGUI.Toggle(rect, label, oldValue);
                if (oldValue != newValue)
                {
                    value = newValue;
                }
            }
            else if (type == typeof(string))
            {
                string oldValue = (string)value;
                string newValue = EditorGUI.TextField(rect, label, oldValue);
                if (oldValue != newValue)
                {
                    value = newValue;
                }
            }
            else if (type == typeof(Vector2))
            {
                Vector2 oldValue = (Vector2)value;
                Vector2 newValue = EditorGUI.Vector2Field(rect, label, oldValue);
                if (oldValue != newValue)
                {
                    value = newValue;
                }
            }
            else if (type == typeof(Vector3))
            {
                Vector3 oldValue = (Vector3)value;
                Vector3 newValue = EditorGUI.Vector3Field(rect, label, oldValue);
                if (oldValue != newValue)
                {
                    value = newValue;
                }
            }
            else if (type.IsEnum)
            {
                Enum oldValue = (Enum)value;
                Enum newValue = EditorGUI.EnumPopup(rect, label, oldValue);
                if (!object.Equals(oldValue, newValue))
                {
                    value = newValue;
                }
            }
            else if (type == typeof(Color))
            {
                Color oldValue = (Color)value;
                Color newValue = EditorGUI.ColorField(rect, label, oldValue);
                if (oldValue != newValue)
                {
                    value = newValue;
                }
            }
            else if (ReflectionEx.IsSameOrSubclassOf(type, typeof(Object)))
            {
                Object oldValue = (Object)value;
                Object newValue = EditorGUI.ObjectField(rect, label, oldValue, type, true);
                if (oldValue != newValue)
                {
                    value = newValue;
                }
            }
            else if (type == typeof(GUIContent))
            {
                GUI.enabled = false;
                EditorGUI.TextField(rect, label, StringEx.Stringify((GUIContent)value));
                GUI.enabled = true;
            }
            else
            {
                GUI.enabled = false;
                EditorGUI.TextField(rect, label, StringEx.Stringify(value));
                GUI.enabled = true;
            }

            return value;
        }

        public static bool ExposeProperty(Rect rect, object context, PropertyInfo property, bool showHeader)
        {
            Type type = property.PropertyType;

            EditorGUI.BeginChangeCheck();

            GUIContent label = showHeader ? new GUIContent(property.Name) : GUIContent.none;

            if (type == typeof(int))
            {
                int oldValue = (int)property.GetValue(context, null);
                int newValue = EditorGUI.IntField(rect, label, oldValue);
                if (oldValue != newValue)
                {
                    property.SetValue(context, newValue, null);
                }
            }
            else if (type == typeof(float))
            {
                float oldValue = (float)property.GetValue(context, null);
                float newValue = EditorGUI.FloatField(rect, label, oldValue);
                if (!Mathf.Approximately(oldValue, newValue))
                {
                    property.SetValue(context, newValue, null);
                }
            }
            else if (type == typeof(bool))
            {
                bool oldValue = (bool)property.GetValue(context, null);
                bool newValue = EditorGUI.Toggle(rect, label, oldValue);
                if (oldValue != newValue)
                {
                    property.SetValue(context, newValue, null);
                }
            }
            else if (type == typeof(string))
            {
                string oldValue = (string)property.GetValue(context, null);
                string newValue = EditorGUI.TextField(rect, label, oldValue);
                if (oldValue != newValue)
                {
                    property.SetValue(context, newValue, null);
                }
            }
            else if (type == typeof(Vector2))
            {
                Vector2 oldValue = (Vector2)property.GetValue(context, null);
                Vector2 newValue = EditorGUI.Vector2Field(rect, label, oldValue);
                if (oldValue != newValue)
                {
                    property.SetValue(context, newValue, null);
                }
            }
            else if (type == typeof(Vector3))
            {
                Vector3 oldValue = (Vector3)property.GetValue(context, null);
                Vector3 newValue = EditorGUI.Vector3Field(rect, label, oldValue);
                if (oldValue != newValue)
                {
                    property.SetValue(context, newValue, null);
                }
            }
            else if (type.IsEnum)
            {
                Enum oldValue = (Enum)property.GetValue(context, null);
                Enum newValue = EditorGUI.EnumPopup(rect, label, oldValue);
                if (!object.Equals(oldValue, newValue))
                {
                    property.SetValue(context, newValue, null);
                }
            }
            else if (type == typeof(Color))
            {
                Color oldValue = (Color)property.GetValue(context, null);
                Color newValue = EditorGUI.ColorField(rect, label, oldValue);
                if (oldValue != newValue)
                {
                    property.SetValue(context, newValue, null);
                }
            }
            else if (ReflectionEx.IsSameOrSubclassOf(type, typeof(Object)))
            {
                Object oldValue = (Object)property.GetValue(context, null);
                Object newValue = EditorGUI.ObjectField(rect, label, oldValue, type, true);
                if (oldValue != newValue)
                {
                    property.SetValue(context, newValue, null);
                }
            }
            else
            {
                EditorGUI.HelpBox(rect, "Unsupported dependency property type.", MessageType.Warning);
            }

            return EditorGUI.EndChangeCheck();
        }
    }
}