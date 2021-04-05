using Microsoft.EntityFrameworkCore;
using PokerApi.Repositories.Entities;
using PokerApi.Repositories.Interfaces;
using Serilog;
using System;
using System.Threading.Tasks;

namespace PokerApi.Repositories.Concretes
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly ILogger _logger;
        private readonly IRepository<Card> _cardRepository;
        private readonly IRepository<Hand> _handRepository;
        private readonly IRepository<Player> _playerRepository;

        public UnitOfWork(
            ILogger logger,
            DbContext context,
            IRepository<Card> cardRepository,
            IRepository<Hand> handRepository,
            IRepository<Player> playerRepository)
        {
            _logger = logger.ForContext("SourceContext", nameof(UnitOfWork));
            _context = context;
            _cardRepository = cardRepository;
            _handRepository = handRepository;
            _playerRepository = playerRepository;
        }

        public async Task<int> Commit()
        {
            var result = await _context.SaveChangesAsync();
            _logger.Debug("UOW committed");
            return result;
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (typeof(TEntity) == typeof(Card))
            {
                _logger.Debug("Returning Card Repository");
                return _cardRepository as IRepository<TEntity>;
            }
            if (typeof(TEntity) == typeof(Hand))
            {
                _logger.Debug("Returning Hand Repository");
                return _handRepository as IRepository<TEntity>;
            }
            if (typeof(TEntity) == typeof(Player))
            {
                _logger.Debug("Returning Player Repository");
                return _playerRepository as IRepository<TEntity>;
            }
            _logger.Warning("Entity:{Entity} does not have repository", typeof(TEntity).FullName);
            throw new Exception($"Entity:{typeof(TEntity).FullName} does not have repository");
        }

        #region Disposable pattern
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
