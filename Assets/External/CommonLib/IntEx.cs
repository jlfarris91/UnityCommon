namespace CommonLib
{
    public static class IntEx
    {
        public static bool IsEven(this int @this)
        {
            return @this % 2 == 0;
        }

        public static bool IsOdd(this int @this)
        {
            return !@this.IsEven();
        }
    }
}
