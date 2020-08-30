namespace CommonLib
{
    public class UniqueId
    {
        private int id = -1;

        public int Next
        {
            get { return ++this.id; }
        }

        public static int Invalid
        {
            get { return -1; }
        }

        public UniqueId() { }
        public UniqueId(int start)
        {
            this.id = start;
        }
    }
}
