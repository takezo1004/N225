namespace N225.Domain.Repositories
{
    public interface IOrder
    {
        SendOrderEntity CreateOrderFiled(SendOrderEntity entity);
    }
}
