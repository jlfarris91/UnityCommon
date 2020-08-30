using System.IO;
using System.Linq;

namespace CommonLib
{
    public static class PathEx
    {
        public static string RemoveExtension(this string @this)
        {
            return
                Path.Combine(Path.GetDirectoryName(@this), Path.GetFileNameWithoutExtension(@this))
                    .Replace("\\", "/")
                    .Trim('/');
        }

        public static string Combine(params string[] strings)
        {
            return strings.Aggregate(string.Empty, Path.Combine);
        }
    }
}
