namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IActivitiesRepository Activities { get; }
        IUserRepository Users { get; }
        Task<bool> SaveChangesAsync();
    }
}