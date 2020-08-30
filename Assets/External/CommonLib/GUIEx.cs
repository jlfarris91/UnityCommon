namespace CommonLib
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public static class GUIEx
    {
        public static readonly Color DisabledColor = Color.grey;

        public const int Margin = 4;
        public const int HeaderHeight = 24;
        public const int TabWidth = 80;

        private static Texture2D white;

        private static Stack<Color> colorStack = new Stack<Color>();
        private static Stack<Color> backgroundColorStack = new Stack<Color>();
        private static Stack<Color> contentColorStack = new Stack<Color>();
        private static Stack<string> tooltipStack = new Stack<string>();
        private static Stack<Matrix4x4> matrixStack = new Stack<Matrix4x4>();

        public static Texture2D White
        {
            get
            {
                if (GUIEx.white == null)
                {
                    GUIEx.white = GUIEx.MakeTex(2, 2, Color.white);
                }
                return GUIEx.white;
            }
        }

        public static bool IsLayoutEvent
        {
            get { return Event.current != null && Event.current.type == EventType.Layout; }
        }

        public static bool IsRepaintEvent
        {
            get { return Event.current != null && Event.current.type == EventType.Repaint; }
        }

        public static bool WasClicked(Rect rect)
        {
            return GUIEx.IsHovering(rect) && Event.current.type == EventType.MouseDown;
        }

        public static bool WasClicked(Rect rect, int button)
        {
            var clicked = GUIEx.IsHovering(rect)
                          && Event.current.type == EventType.MouseDown
                          && Event.current.button == button;
            if (clicked)
            {
                Event.current.Use();
            }
            return clicked;
        }

        public static bool IsHovering(Rect rect)
        {
            return Event.current != null && rect.Contains(Event.current.mousePosition);
        }

        public static void PushBackgroundColor(Color color)
        {
            if (GUIEx.backgroundColorStack.Count == 0)
            {
                GUIEx.backgroundColorStack.Push(UnityEngine.GUI.backgroundColor);
            }

            GUIEx.backgroundColorStack.Push(color);
            UnityEngine.GUI.backgroundColor = color;
        }

        public static void PopBackgroundColor()
        {
            if (GUIEx.backgroundColorStack.Count > 1)
            {
                GUIEx.backgroundColorStack.Pop();
            }

            Color color = Color.red;

            if (GUIEx.backgroundColorStack.Count > 0)
            {
                color = GUIEx.backgroundColorStack.Peek();
            }

            UnityEngine.GUI.backgroundColor = color;
        }

        public static void PushColor(Color color)
        {
            if (GUIEx.colorStack.Count == 0)
            {
                GUIEx.colorStack.Push(UnityEngine.GUI.color);
            }

            GUIEx.colorStack.Push(color);
            UnityEngine.GUI.color = color;
        }

        public static void PopColor()
        {
            if (GUIEx.colorStack.Count > 1)
            {
                GUIEx.colorStack.Pop();
            }

            Color color = Color.red;

            if (GUIEx.colorStack.Count > 0)
            {
                color = GUIEx.colorStack.Peek();
            }

            UnityEngine.GUI.color = color;
        }

        public static void PushContentColor(Color color)
        {
            if (GUIEx.contentColorStack.Count == 0)
            {
                GUIEx.contentColorStack.Push(UnityEngine.GUI.contentColor);
            }

            GUIEx.contentColorStack.Push(color);
            UnityEngine.GUI.contentColor = color;
        }

        public static void PopContentColor()
        {
            if (GUIEx.contentColorStack.Count > 1)
            {
                GUIEx.contentColorStack.Pop();
            }

            Color color = Color.red;

            if (GUIEx.contentColorStack.Count > 0)
            {
                color = GUIEx.contentColorStack.Peek();
            }

            UnityEngine.GUI.contentColor = color;
        }

        public static void PushTooltip(string tooltip)
        {
            if (!GUIEx.tooltipStack.Any())
            {
                GUIEx.tooltipStack.Push(UnityEngine.GUI.tooltip);
            }

            GUIEx.tooltipStack.Push(tooltip);
            UnityEngine.GUI.tooltip = tooltip;
        }

        public static void PopTooltip()
        {
            if (GUIEx.tooltipStack.Count > 1)
            {
                GUIEx.tooltipStack.Pop();
            }

            string tooltip = string.Empty;

            if (GUIEx.tooltipStack.Count > 0)
            {
                tooltip = GUIEx.tooltipStack.Peek();
            }

            UnityEngine.GUI.tooltip = tooltip;
        }

        public static void PushMatrix(Matrix4x4 matrix)
        {
            if (!GUIEx.matrixStack.Any())
            {
                GUIEx.matrixStack.Push(UnityEngine.GUI.matrix);
            }

            GUIEx.matrixStack.Push(matrix);
            UnityEngine.GUI.matrix = matrix;
        }

        public static void PopMatrix()
        {
            if (GUIEx.matrixStack.Count > 1)
            {
                GUIEx.matrixStack.Pop();
            }

            Matrix4x4 matrix = Matrix4x4.identity;

            if (GUIEx.matrixStack.Count > 0)
            {
                matrix = GUIEx.matrixStack.Peek();
            }

            UnityEngine.GUI.matrix = matrix;
        }

        private static Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            Texture2D result = new Texture2D(width, height);
            result.SetPixels(pix);
            //result.alphaIsTransparency = true;
            result.filterMode = FilterMode.Point;
            result.Apply();
            return result;
        }

        private static Texture2D MakeBorderTex(Color border, Color background)
        {
            var w = 19;
            var pix = new Color[w * w];
            for (int i = 0; i < pix.Length; ++i)
            {
                int r = i / w;
                int c = i % w;
                pix[i] = (r == 0 || r == w - 1 || c == 0 || c == w - 1) ? border : background;
            }

            var result = new Texture2D(w, w);
            result.SetPixels(pix);
            //result.alphaIsTransparency = true;
            result.filterMode = FilterMode.Point;
            result.Apply();
            return result;
        }

        public static void DrawVerticalLine(Vector2 a, Vector2 b, float width)
        {
            UnityEngine.GUI.Box(new Rect(a.x - width / 2.0f, a.y, width, Mathf.Abs(a.y - b.y)), String.Empty);
        }

        public static void DrawHorizontalLine(Vector2 a, Vector2 b, float width)
        {
            UnityEngine.GUI.Box(new Rect(a.x, a.y - width / 2.0f, Mathf.Abs(a.x - b.x), width), String.Empty);
        }

        public static void DrawBorder(Rect area, float left, float right, float top, float bottom, Color color)
        {
            GUIEx.PushColor(color);
            // Top
            UnityEngine.GUI.DrawTexture(new Rect(area.xMin + top, area.yMin, area.width - top * 2.0f, top), GUIEx.White);
            // Bottom
            UnityEngine.GUI.DrawTexture(new Rect(area.xMin + bottom, area.yMax - bottom, area.width - bottom * 2.0f, bottom),
                GUIEx.White);
            // Left
            UnityEngine.GUI.DrawTexture(new Rect(area.xMin, area.yMin, left, area.height), GUIEx.White);
            // Right
            UnityEngine.GUI.DrawTexture(new Rect(area.xMax - right, area.yMin, right, area.height), GUIEx.White);
            GUIEx.PopColor();
        }

        public static void DrawBorder(Rect area, float border, Color color)
        {
            GUIEx.DrawBorder(area, border, border, border, border, color);
        }

        public static void DrawBorder(Rect area, RectOffset border, Color color)
        {
            GUIEx.DrawBorder(area, border.left, border.right, border.top, border.bottom, color);
        }

        public static void DrawBox(Rect area, Color backgroundColor)
        {
            GUIEx.PushColor(backgroundColor);
            UnityEngine.GUI.DrawTexture(area, GUIEx.White);
            GUIEx.PopColor();
        }

        public static void DrawBox(Rect area, float borderSize, Color borderColor, Color backgroundColor)
        {
            GUIEx.DrawBox(area, backgroundColor);
            GUIEx.DrawBorder(area, borderSize, borderColor);
        }

        public static void DrawLabelAtCursor(string text, Color background)
        {
            GUIEx.DrawLabelAtCursor(new GUIContent(text), UnityEngine.GUI.skin.label, background);
        }

        public static void DrawLabelAtCursor(GUIContent content, GUIStyle style, Color background)
        {
            GUIEx.PushBackgroundColor(background);
            var area = new Rect(Event.current.mousePosition, Vector2.zero);

            float width;

            style.CalcMinMaxWidth(content, out width, out width);
            float height = style.CalcHeight(content, width);

            area.width = width;
            area.height = height;

            UnityEngine.GUI.Label(area, content, style);
            GUIEx.PopBackgroundColor();
        }

        public static void DrawSprite(Sprite sprite, Rect position)
        {
            if (sprite == null)
            {
                return;
            }

            Texture texture = sprite.texture;
            Rect tr = sprite.textureRect;

            var frameRect = new Rect(
                tr.x / texture.width,
                tr.y / texture.height,
                tr.width / texture.width,
                tr.height / texture.height);

            UnityEngine.GUI.DrawTextureWithTexCoords(position, texture, frameRect);
        }

        public static bool SpriteButton(Sprite sprite, Rect position, bool respectRatio = false)
        {
            return GUIEx.SpriteButton(sprite, position, UnityEngine.GUI.skin.button, respectRatio);
        }

        public static bool SpriteButton(Sprite sprite, Rect position, GUIStyle style, bool respectRatio = false)
        {
            bool pressed = UnityEngine.GUI.Button(position, GUIContent.none, style);

            position = position.Expand(-style.border.horizontal, -style.border.vertical);

            if (respectRatio)
            {
                float ratio = sprite.textureRect.height / sprite.textureRect.width;
                if (sprite.textureRect.width > sprite.textureRect.height)
                {
                    position.height *= ratio;
                }
                else
                {
                    position.width *= ratio;
                }
            }

            GUIEx.DrawSprite(sprite, position);
            return pressed;
        }

        public static void DrawAtWorldSpacePoint(
            GUIContent content,
            Vector3 worldSpacePoint,
            Vector2 screenSpaceOffset,
            GUIStyle style)
        {
            Camera camera = Camera.current;

            Vector2 screenSpacePoint = camera.WorldToScreenPoint(worldSpacePoint);
            screenSpacePoint += screenSpaceOffset;

            Vector2 controlSize = style.CalcSize(content) * 2.0f;

            var rect = new Rect(screenSpacePoint.x, screenSpacePoint.y, controlSize.x, controlSize.y);

            UnityEngine.GUI.Label(rect, content);
        }

        public static void DrawLines(Vector2[] points, Color color, float width)
        {
            DrawLines(points, Enumerable.Repeat(color, points.Length).ToArray(), width);
        }

        public static void DrawLines(Vector2[] points, Color[] colors, float width)
        {
            ThrowIf.ArgumentIsNull(points, "points");
            ThrowIf.ArgumentIsNull(colors, "colors");
            ThrowIf.False(points.Length == colors.Length, "Points and colors array must be the same length.");

            for (int i = 0; i < points.Length; ++i)
            {
                Vector2 start = points[i];
                Vector2 end = points[MathEx.Wrap(i + 1, 0, points.Length - 1)];
                Color color = colors[i];
                
                if (Mathf.Abs(start.x - end.x) > Mathf.Abs(start.y - end.y))
                {
                    GUIEx.DrawBox(new Rect(start.x, start.y, end.x - start.x, width), color);
                }
                else
                {
                    GUIEx.DrawBox(new Rect(start.x, start.y, width, end.y - start.y), color);
                }
                GUIEx.PopColor();
            }
        }
    }
}