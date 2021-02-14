using System.Threading.Tasks;

namespace UserGroups.Application.IntegrationTests
{
    public class Assert
    {
        public async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : class
        {
            return await Testing.FindAsync<TEntity>(keyValues);
        }
    }
}
