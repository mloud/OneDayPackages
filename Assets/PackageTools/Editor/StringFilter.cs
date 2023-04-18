namespace PackageTools.Editor
{
    public class StringFilter : IFilter<string, string>
    {
        public string Mask { get; set; }

        public bool IsFiltered(string input) =>
            string.IsNullOrEmpty(Mask) || input.Contains(Mask);
    }
}