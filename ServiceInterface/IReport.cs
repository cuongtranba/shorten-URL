using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace ServiceInterface
{
    public interface IReport
    {
        Task<List<DataPoint>> GetReport(int URLId);
    }
}
