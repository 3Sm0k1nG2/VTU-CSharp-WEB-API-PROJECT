namespace WorkOrders_BAL.Interfaces
{
    public interface IDto
    {
        Guid Id { get; }
        void SetInitId(Guid id);
    }
}
