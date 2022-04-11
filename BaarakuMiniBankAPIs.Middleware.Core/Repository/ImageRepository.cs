using BaarakuMiniBankAPIs.Middleware.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BaarakuMiniBankAPIs.Middleware.Core.Repository
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
