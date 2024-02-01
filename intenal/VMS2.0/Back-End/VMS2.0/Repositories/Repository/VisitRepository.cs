using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VMS2._0.Data;
using VMS2._0.DTO;
using VMS2._0.Models;
using VMS2._0.Repositories.IRepository;

namespace VMS2._0.Repositories.Repository
{
    public class VisitRepository : IVisitRepository
    {
        private readonly VMSDbContext _context;
        private readonly IMapper _mapper;

        public VisitRepository(VMSDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // Generic methods to add the object to the DB
        public async Task AddAndSaveAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }//- AddAndSaveAsync

        public async Task UpdateAndSaveAsync<T>(T entity) where T : class {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }//- UpdateAndSaveAsync




        // Host related methods
        public async Task<UserDTO> GetHostDetails(string userID)
        {

            var user = await _context.Users.FindAsync(userID);
            if (user == null)
            {
                throw new ApplicationException("User not found");
            }
            var userDTO = new UserDTO
            {
                UserID = user.UserID,
                UserName = user.UserName,
                UserEmail = user.UserEmail
            };
            return userDTO;
        }//- GetHostDetails


        public async Task<VisitDTO> GetVisitDetailsByID(string visitID) {
            var visit = await _context.Visits.FindAsync(visitID);
            if (visit == null)
            {
                throw new ApplicationException("Visit not found");
            }

            return _mapper.Map<VisitDTO>(visit);
        }//- GetVisitDetailsByID



    
        // Visitor Related Methods
        public async Task<VisitorDTO> GetVisitorDetails(InitiateVisitDTO initiateVisitDTO)
        {
            var visitor = await _context.Visitors
                .FirstOrDefaultAsync(v => v.VisitorEmail == initiateVisitDTO.VisitorEmail);

            if (visitor == null)
            {
                var secondaryInfo = await _context.SecondaryInfos
                    .FirstOrDefaultAsync(s => s.VisitorID == initiateVisitDTO.VisitorEmail);

                if (secondaryInfo == null)
                {
                    var visitorDTO = new VisitorDTO
                    {
                        VisitorID = Guid.NewGuid().ToString(),
                        VisitorEmail = initiateVisitDTO.VisitorEmail,
                        VisitorNumber = initiateVisitDTO.VisitorNumber,
                        VisitorName = initiateVisitDTO.VisitorName
                    };
                    // When the user is created then we need to add it to the DB. 
                    var visitorEntity = _mapper.Map<Visitor>(visitorDTO);
                    await AddAndSaveAsync(visitorEntity);
                    return visitorDTO;
                }
            }

            return new VisitorDTO
            {
                VisitorID = visitor.VisitorID,
                VisitorName = visitor.VisitorName,
                VisitorEmail = visitor.VisitorEmail,
                VisitorNumber = visitor.VisitorNumber,
                VisitorAddress = visitor.VisitorAddress,
                IdentityType = visitor.IdentityType,
                IdentityNumber = visitor.IdentityNumber,
                Image = visitor.Image
            };
        }//- GetVisitorDetails

        public async Task<VisitorDTO> GetVisitorDetailsByVisitID(string visitID)
        {
            var visit = await _context.Visits
                                      .Include(v => v.Visitor)
                                      .FirstOrDefaultAsync(v => v.VisitID == visitID);

            if (visit == null || visit.Visitor == null)
            {
                throw new Exception("Visit or associated Visitor not found.");
            }

            return _mapper.Map<VisitorDTO>(visit.Visitor);
        } //- GetVisitorDetailsByVisitorID

    }//- Repository end
}
