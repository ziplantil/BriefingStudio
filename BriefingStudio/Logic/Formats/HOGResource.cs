namespace BriefingStudio.Logic.Formats
{
    internal class HOGResource
    {
        public string Name;
        public long Offset;
        public int Length;

        public HOGResource(string name, long offset, int length)
        {
            this.Name = name;
            this.Offset = offset;
            this.Length = length;
        }
    }
}