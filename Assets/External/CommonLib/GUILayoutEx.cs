namespace CommonLib
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public static class GUILayoutEx
    {
        private static readonly Stack<RectOffset> paddingStack = new Stack<RectOffset>();

        public static void DrawSprite(Sprite sprite, params GUILayoutOption[] options)
        {
            Rect controlRect = GUILayoutUtility.GetRect(GUIContent.none, new GUIStyle(), options);
            GUIEx.DrawSprite(sprite, controlRect);
        }

        public static bool SpriteButton(Sprite sprite, bool respectRatio = false, params GUILayoutOption[] options)
        {
            GUIStyle style = UnityEngine.GUI.skin.button;
            Rect controlRect = GUILayoutUtility.GetRect(GUIContent.none, style, options);
            return GUIEx.SpriteButton(sprite, controlRect, style, respectRatio);
        }

        public static bool SpriteButton(Sprite sprite,
                                        GUIStyle style,
                                        bool respectRatio = false,
                                        params GUILayoutOption[] options)
        {
            Rect controlRect = GUILayoutUtility.GetRect(GUIContent.none, style, options);
            return GUIEx.SpriteButton(sprite, controlRect, style, respectRatio);
        }

        public static void PushPadding(int padding)
        {
            PushPadding(new RectOffset(padding, padding, padding, padding));
        }

        public static void PushPadding(RectOffset padding)
        {
            if (!paddingStack.Any())
            {
                paddingStack.Push(new RectOffset());
            }

            paddingStack.Push(padding);

            GUILayout.BeginHorizontal();
            GUILayout.Space(padding.left);
            GUILayout.BeginVertical();
            GUILayout.Space(padding.top);
        }

        public static void PopPadding()
        {
            RectOffset padding = new RectOffset();

            if (paddingStack.Count > 1)
            {
                padding = paddingStack.Pop();
            }

            GUILayout.Space(padding.bottom);
            GUILayout.EndVertical();
            GUILayout.Space(padding.right);
            GUILayout.EndHorizontal();
        }

        public static bool ImageButton(Texture2D image, GUIStyle style, params GUILayoutOption[] options)
        {
            Color color = Color.black;
            GUIStyle labelStyle = new GUIStyle(style);

            Rect labelRect = GUILayoutUtility.GetRect(new GUIContent(image), style, options);

            if (GUIEx.IsHovering(labelRect))
            {
                color = Color.blue;
            }

            GUIEx.PushColor(color);
            UnityEngine.GUI.DrawTexture(labelRect, image, ScaleMode.ScaleToFit);
            GUIEx.PopColor();

            return GUIEx.WasClicked(labelRect, 0);
        }

        public static bool LabelButton(string label, GUIStyle style)
        {
            Color color = Color.black;
            GUIStyle labelStyle = new GUIStyle(style);

            Rect labelRect = GUILayoutUtility.GetRect(new GUIContent(label), style);

            if (GUIEx.IsHovering(labelRect))
            {
                color = Color.blue;
            }

            Rect underlineRect = labelRect;
            underlineRect.y = underlineRect.yMax - 2.0f;
            underlineRect.height = 1.0f;

            labelStyle.normal.textColor = color;
            UnityEngine.GUI.Label(labelRect, label, labelStyle);
            GUIEx.DrawBox(underlineRect, color);

            return false;
        }
    }
}