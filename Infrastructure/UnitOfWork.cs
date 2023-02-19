﻿using Domain.Interfaces;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private class and Constructor
        private readonly DataContext _context;
        private IActivitiesRepository _activitiesRepository;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        #endregion

        public IActivitiesRepository Activities => _activitiesRepository
           ?? new ActivitiesRepository(_context);

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
