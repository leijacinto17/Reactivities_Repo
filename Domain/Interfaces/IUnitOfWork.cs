namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IActivitiesRepository Activities { get; }
        Task<bool> SaveChangesAsync();
    }
}