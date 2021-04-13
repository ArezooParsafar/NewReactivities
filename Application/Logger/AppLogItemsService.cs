using Domain.Logger;
using Infrastructure.Enumerations;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Logger
{
    public class AppLogItemsService : IAppLogItemsService
    {
        private readonly DbSet<AppLogItem> _appLogItems;
        private readonly ApplicationDbContext _context;

        public AppLogItemsService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _appLogItems = _context.Set<AppLogItem>();

        }

        public Task DeleteAllAsync(string logLevel = "")
        {
            if (string.IsNullOrWhiteSpace(logLevel))
            {
                _appLogItems.RemoveRange(_appLogItems);
            }
            else
            {
                var query = _appLogItems.Where(l => l.LogLevel == logLevel);
                _appLogItems.RemoveRange(query);
            }

            return _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int logItemId)
        {
            var itemToRemove = await _appLogItems.FirstOrDefaultAsync(x => x.Id.Equals(logItemId));
            if (itemToRemove != null)
            {
                _appLogItems.Remove(itemToRemove);
                await _context.SaveChangesAsync();
            }
        }

        public Task DeleteOlderThanAsync(DateTime cutoffDateUtc, string logLevel = "")
        {
            if (string.IsNullOrWhiteSpace(logLevel))
            {
                var query = _appLogItems.Where(l => l.CreatedDateTime < cutoffDateUtc);
                _appLogItems.RemoveRange(query);
            }
            else
            {
                var query = _appLogItems.Where(l => l.CreatedDateTime < cutoffDateUtc && l.LogLevel == logLevel);
                _appLogItems.RemoveRange(query);
            }

            return _context.SaveChangesAsync();
        }

        public Task<int> GetCountAsync(string logLevel = "")
        {
            return string.IsNullOrWhiteSpace(logLevel) ?
                            _appLogItems.CountAsync() :
                            _appLogItems.Where(l => l.LogLevel == logLevel).CountAsync();
        }

        public async Task<PagedAppLogItemsViewModel> GetPagedAppLogItemsAsync(
            int pageNumber,
            int pageSize,
            SortOrder sortOrder,
            string logLevel = "")
        {
            var offset = (pageSize * pageNumber) - pageSize;

            var query = string.IsNullOrWhiteSpace(logLevel) ?
                             _appLogItems :
                             _appLogItems.Where(l => l.LogLevel == logLevel);

            query = sortOrder == SortOrder.Descending ? query.OrderByDescending(x => x.Id) : query.OrderBy(x => x.Id);

            return new PagedAppLogItemsViewModel
            {
                Paging =
                {
                    TotalItems = await query.CountAsync()
                },
                AppLogItems = await query.Skip(offset).Take(pageSize).ToListAsync()
            };
        }
    }
}