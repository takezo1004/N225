namespace N225.Domain.Repositories
{
    public interface ICommand
    {
        void Initial<T>(T entity);
        void Xepired(string orderId);
        void Oreder(string orderId);
        void Correction(string orderId);
        void Revocation(string orderId);
        void Contrct(string orderId);
    }
}
