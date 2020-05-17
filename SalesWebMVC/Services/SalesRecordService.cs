using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMVC.Services {
    public class SalesRecordService {
        private readonly SalesWebMVCContext _context;

        public SalesRecordService(SalesWebMVCContext context) {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate) {
            var result = _context.SalesRecord.AsNoTracking();
            if (minDate.HasValue) result = result.Where(s => s.Date >= minDate);
            if (maxDate.HasValue) result = result.Where(s => s.Date <= maxDate);
            return await result.Include(x => x.Seller).Include(x => x.Seller.Department).OrderByDescending(x => x.Date).ToListAsync();
        }
    }
}
