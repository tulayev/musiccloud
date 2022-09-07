using Application.Repository.IRepository;
using AutoMapper;
using Data;
using Models;

namespace Application.Repository
{
    public class FileRepository : Repository<AppFile>, IFileRepository
    {
        public FileRepository(DataContext ctx, IMapper mapper) : base(ctx, mapper)
        {
        }
    }
}