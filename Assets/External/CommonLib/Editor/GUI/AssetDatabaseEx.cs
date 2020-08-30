namespace CommonLib.Editor.GUI
{
    using System;
    using UnityEditor;
    using Object = UnityEngine.Object;

    public static class AssetDatabaseEx
    {
        public static bool AssetExists(string path, Type type)
        {
            return AssetDatabase.LoadAssetAtPath(path, type) != null;
        }

        /// <summary>
        /// Replaces an existing asset with a new one, if the asset exists.
        /// </summary>
        /// <param name="path">The path to an existing asset.</param>
        /// <param name="asset">The asset to replace the existing asset with.</param>
        /// <returns>True if the asset was replaced, false otherwise.</returns>
        public static bool ReplaceAsset(string path, Object asset)
        {
            Object existingAsset = AssetDatabase.LoadAssetAtPath(path, asset.GetType());

            if (existingAsset != null)
            {
                EditorUtility.CopySerialized(asset, existingAsset);
                AssetDatabase.SaveAssets();

                // Asset was replaced
                return true;
            }

            // No asset existed, so nothing happened
            return false;
        }
    }
}
