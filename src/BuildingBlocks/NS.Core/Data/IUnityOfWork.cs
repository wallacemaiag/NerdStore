using System.Threading.Tasks;

namespace NS.Core.Data
{
    public interface IUnityOfWork
    {
        Task<bool> Commit();
    }
}
