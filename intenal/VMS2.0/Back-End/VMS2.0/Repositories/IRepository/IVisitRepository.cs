using VMS2._0.Models;
using System.Threading.Tasks;
using VMS2._0.DTO;

namespace VMS2._0.Repositories.IRepository
{
    public interface IVisitRepository
    {
        Task AddAndSaveAsync<T>(T entity) where T : class;
        Task<VisitorDTO> GetVisitorDetails(InitiateVisitDTO initiateVisitDTO);
        Task<UserDTO> GetHostDetails(string userID);
        Task<VisitDTO> GetVisitDetailsByID(string visitID);
        Task<VisitorDTO> GetVisitorDetailsByVisitID(string visitID);
        Task UpdateAndSaveAsync<T>(T entity) where T : class;
    }
}
