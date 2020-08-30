namespace CommonLib.Collections
{
    using System;

    internal static class TreeExtensions
    {
        public static T FirstOrDefault<T>(this ITreeNode<T> @this, Func<T, bool> predicate)
        {
            return @this.FirstOrDefault(predicate, TreeTraverseOrder.BreadthFirst, default(T));
        }

        public static T FirstOrDefault<T>(this ITreeNode<T> @this, Func<T, bool> predicate, TreeTraverseOrder order, T @default)
        {
            T result = @default;

            TreeExtensions.TryFindFirstParentFirst(@this, predicate, order, ref result);
            
            return result;
        }

        private static bool TryFindFirstParentFirst<T>(ITreeNode<T> root, Func<T, bool> predicate, TreeTraverseOrder order, ref T result)
        {
            if (predicate(root.Data))
            {
                result = root.Data;
                return true;
            }

            ITreeNode<T> child = root.FirstChild;

            while (child != null)
            {
                if (TreeExtensions.TryFindFirstParentFirst(child, predicate, order, ref result))
                {
                    return true;
                }

                child = child.NextSibling;
            }

            return false;
        }
    }
}