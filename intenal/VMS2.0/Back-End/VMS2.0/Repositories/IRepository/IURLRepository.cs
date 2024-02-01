using AutoMapper;
using VMS2._0.Data;

namespace VMS2._0.Repositories.IRepository
{
    public class IURLRepository
    {
        private readonly VMSDbContext _context;
        private readonly IMapper _mapper;

        public IURLRepository(VMSDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }//- Constructor








    }//- Repository end
}
