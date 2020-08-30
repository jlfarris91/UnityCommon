namespace CommonLib
{
    using UnityEngine;

    public static class ViewboxUtility
    {
        private const float kEditorWindowTabHeight = 21.0f;
        private static Matrix4x4 _prevGuiMatrix;

        public static Rect Begin(float zoomScale, Rect screenCoordsArea)
        {
            UnityEngine.GUI.EndGroup();        // End the group Unity begins automatically for an EditorWindow to clip out the window tab. This allows us to draw outside of the size of the EditorWindow.

            Rect clippedArea = screenCoordsArea.Scale(1.0f / zoomScale, new Vector2(screenCoordsArea.xMin, screenCoordsArea.yMin));
            clippedArea.y += ViewboxUtility.kEditorWindowTabHeight;
            UnityEngine.GUI.BeginGroup(clippedArea);

            ViewboxUtility._prevGuiMatrix = UnityEngine.GUI.matrix;
            Matrix4x4 translation = Matrix4x4.TRS(new Vector2(clippedArea.xMin, clippedArea.yMin), Quaternion.identity, Vector3.one);
            Matrix4x4 scale = Matrix4x4.Scale(new Vector3(zoomScale, zoomScale, 1.0f));
            UnityEngine.GUI.matrix = translation * scale * translation.inverse * UnityEngine.GUI.matrix;

            return clippedArea;
        }

        public static void End()
        {
            UnityEngine.GUI.matrix = ViewboxUtility._prevGuiMatrix;
            UnityEngine.GUI.EndGroup();
            UnityEngine.GUI.BeginGroup(new Rect(0.0f, ViewboxUtility.kEditorWindowTabHeight, Screen.width, Screen.height));
        }
    }
}
