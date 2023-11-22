using ServerApp.Entities;

namespace ServerApp.DatabaseStores.Interfaces;

public interface ITask2RoleDatabaseStore
{
    public Task CreateAsync(EntityTask2Role entity, CancellationToken cancellationToken = default);
    public Task UpdateAsync(EntityTask2Role entity, CancellationToken cancellationToken = default);
    public Task DeleteAsync(EntityTask2Role entity, CancellationToken cancellationToken = default);
    public Task<IList<int>> GetTasksByRoleAsync(int roleId, CancellationToken cancellationToken = default);
}
