namespace CommonLib
{
    using System;

    public static class AppEnvironment
    {
        public static readonly string MyDocumentsPath;
        public static readonly string GameDataFolder;
        public static readonly string AppName = "Hunting Party";

        static AppEnvironment()
        {
            AppEnvironment.MyDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            AppEnvironment.GameDataFolder = PathEx.Combine(AppEnvironment.MyDocumentsPath, AppEnvironment.AppName);
        }
    }
}
