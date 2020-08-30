using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommonLib;
using UnityEditor;
using UnityEngine;

public class ExportPackages
{
    [MenuItem("File/Export/Packages")]
    private static void TryExportPackages()
    {
        try
        {
            string assetSubDirPath = "External";
            string assetsFolderPath = Application.dataPath;
            string scriptsFolderPath = assetsFolderPath + $"/{assetSubDirPath}";
            string projectFolderPath = Application.dataPath.Remove("Assets").Trim('/');

            Debug.Log("Assets path: " + assetsFolderPath);
            Debug.Log("Project path: " + projectFolderPath);

            string[] assetRelativeLibDirectories = Directory.EnumerateDirectories(scriptsFolderPath)
                .Select(dir => dir.Remove(projectFolderPath).Replace("\\", "/").Trim('/'))
                .ToArray();

            int currentPackage = 1;

            foreach (string libDirectory in assetRelativeLibDirectories)
            {
                Debug.Log("Exporting folder " + libDirectory);

                string libDirectoryName = libDirectory.Remove($"Assets/{assetSubDirPath}");

                string packageFilePath = $"{projectFolderPath}{libDirectoryName}.unitypackage".Replace("\\", "/").Trim('/');

                float progress = (float)currentPackage / assetRelativeLibDirectories.Length;
                bool cancel = EditorUtility.DisplayCancelableProgressBar("Exporting packages", packageFilePath, progress);

                if (cancel)
                {
                    Debug.Log("Export cancelled.");
                    return;
                }

                Debug.Log(libDirectory + " -> " + packageFilePath);
                AssetDatabase.ExportPackage(libDirectory, packageFilePath, ExportPackageOptions.Recurse);

                currentPackage++;
            }

            string fullPackageFilePath = Path.Combine(projectFolderPath, "CommonAll").Replace("\\", "/").Trim('/');
            fullPackageFilePath = Path.ChangeExtension(fullPackageFilePath, "unitypackage");

            EditorUtility.DisplayProgressBar("Exporting packages", fullPackageFilePath, 1.0f);

            Debug.Log("All -> " + fullPackageFilePath);
            AssetDatabase.ExportPackage(assetRelativeLibDirectories, fullPackageFilePath, ExportPackageOptions.Recurse);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        EditorUtility.ClearProgressBar();
    }
}
