namespace CommonLib
{
    using System.IO;

    public static class FileEx
    {
        public static bool IsFileLocked(string filepath)
        {
            if (!File.Exists(filepath))
            {
                return false;
            }

            FileStream stream = null;

            try
            {
                FileInfo file = new FileInfo(filepath);
                stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
    }
}
