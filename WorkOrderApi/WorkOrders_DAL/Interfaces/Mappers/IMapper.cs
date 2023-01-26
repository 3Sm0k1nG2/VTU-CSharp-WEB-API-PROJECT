namespace WorkOrders_DAL.Interfaces.Mappers
{
    public interface IMapper<TFirst, TSecond>
    {
        TFirst Map(TSecond second);
        TSecond Map(TFirst second);
        IList<TFirst> Map(IList<TSecond> second);
        IList<TSecond> Map(IList<TFirst> second);
    }
}
