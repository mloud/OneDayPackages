namespace PackageTools.Editor
{
    public interface IFilter<in TInput, TMask>
    {
        TMask Mask { get; set; }
        bool IsFiltered(TInput input);
    }
}