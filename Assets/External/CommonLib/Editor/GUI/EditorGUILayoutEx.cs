namespace CommonLib.Editor.GUI
{
    using System;
    using System.Reflection;
    using global::CommonLib;
    using UnityEditor;
    using UnityEngine;
    using Object = UnityEngine.Object;

    public static class EditorGUILayoutEx
    {
        public static void DrawHeader(string label)
        {
            EditorGUILayoutEx.DrawHeader(new GUIContent(label));
        }

        public static void DrawHeader(GUIContent label)
        {
            Rect controlRect = GUILayoutUtility.GetRect(label, EditorStyles.boldLabel, GUILayout.Height(24.0f));
            controlRect.y += 8;
            GUI.Label(controlRect, label, EditorStyles.boldLabel);
        }

        public static void BeginTitlebar(string label)
        {
            GUIStyle style = new GUIStyle(GUI.skin.box);
            style.normal.background = EditorStyles.toolbar.normal.background;

            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal(style);
            GUILayout.Space(2.0f);
            GUILayout.Label(label);
        }

        public static void EndTitlebar()
        {
            GUILayout.EndHorizontal();
            GUILayout.Space(2.0f);
            GUILayout.EndVertical();
        }

        public static object ExposeGenericObject(object value, Type type, GUIContent label, params GUILayoutOption[] options)
        {
            Rect position = EditorGUILayout.GetControlRect(true, EditorGUIUtility.singleLineHeight, EditorStyles.numberField, options);
            return EditorGUIEx.ExposeGenericObject(position, value, type, label);
        }

        public static bool ExposeProperty(object context, PropertyInfo property, bool showHeader, params GUILayoutOption[] options)
        {
            Type type = property.PropertyType;

            EditorGUI.BeginChangeCheck();

            GUIContent label = showHeader ? new GUIContent(property.Name) : GUIContent.none;

            if (!property.CanWrite || ReflectionEx.HasAttribute<ReadOnlyAttribute>(property))
            {
                GUI.enabled = false;
            }

            EditorGUILayout.BeginHorizontal();
            if (type == typeof(int))
            {
                int oldValue = (int)property.GetValue(context, null);
                int newValue = EditorGUILayout.IntField(label, oldValue, options);
                if (oldValue != newValue)
                {
                    property.SetValue(context, newValue, null);
                }
            }
            else if (type == typeof(float))
            {
                float oldValue = (float)property.GetValue(context, null);
                float newValue = EditorGUILayout.FloatField(label, oldValue, options);
                if (!Mathf.Approximately(oldValue, newValue))
                {
                    property.SetValue(context, newValue, null);
                }
            }
            else if (type == typeof(bool))
            {
                bool oldValue = (bool)property.GetValue(context, null);
                bool newValue = EditorGUILayout.Toggle(label, oldValue, options);
                if (oldValue != newValue)
                {
                    property.SetValue(context, newValue, null);
                }
            }
            else if (type == typeof(string))
            {
                string oldValue = (string)property.GetValue(context, null);
                string newValue = EditorGUILayout.TextField(label, oldValue, options);
                if (oldValue != newValue)
                {
                    property.SetValue(context, newValue, null);
                }
            }
            else if (type == typeof(Vector2))
            {
                Vector2 oldValue = (Vector2)property.GetValue(context, null);
                Vector2 newValue = EditorGUILayout.Vector2Field(label, oldValue, options);
                if (oldValue != newValue)
                {
                    property.SetValue(context, newValue, null);
                }
            }
            else if (type == typeof(Vector3))
            {
                Vector3 oldValue = (Vector3)property.GetValue(context, null);
                Vector3 newValue = EditorGUILayout.Vector3Field(label, oldValue, options);
                if (oldValue != newValue)
                {
                    property.SetValue(context, newValue, null);
                }
            }
            else if (type.IsEnum)
            {
                Enum oldValue = (Enum)property.GetValue(context, null);
                Enum newValue = EditorGUILayout.EnumPopup(label, oldValue, options);
                if (!object.Equals(oldValue, newValue))
                {
                    property.SetValue(context, newValue, null);
                }
            }
            else if (type == typeof(Color))
            {
                Color oldValue = (Color)property.GetValue(context, null);
                Color newValue = EditorGUILayout.ColorField(label, oldValue, options);
                if (oldValue != newValue)
                {
                    property.SetValue(context, newValue, null);
                }
            }
            else if (ReflectionEx.IsSameOrSubclassOf(type, typeof(Object)))
            {
                Object oldValue = (Object)property.GetValue(context, null);
                Object newValue = EditorGUILayout.ObjectField(label, oldValue, type, true, options);
                if (oldValue != newValue)
                {
                    property.SetValue(context, newValue, null);
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Unsupported dependency property type.", MessageType.Warning);
            }

            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;

            return EditorGUI.EndChangeCheck();
        }
    }
}