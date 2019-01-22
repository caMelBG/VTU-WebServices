namespace Models.Converters.Interface
{
    public interface IModelConverter<TSource, TDestination>
    {
        TDestination ConvertTo(TSource source);

        TSource ConvertTo(TDestination destination);
    }
}
