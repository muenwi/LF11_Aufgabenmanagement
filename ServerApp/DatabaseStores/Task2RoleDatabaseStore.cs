﻿using Microsoft.EntityFrameworkCore;
using ServerApp.AppContexts;
using ServerApp.DatabaseStores.Interfaces;
using ServerApp.Entities;

namespace ServerApp.DatabaseStores;

public class Task2RoleDatabaseStore : ITask2RoleDatabaseStore
{
    private readonly ServerAppDbContext _context;
    private DbSet<EntityTask2Role> _tasks2Roles => _context.Tasks2Roles;

    public Task2RoleDatabaseStore(ServerAppDbContext context) {
        _context = context;
    }
    public async Task CreateAsync(EntityTask2Role entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await _tasks2Roles.AddAsync(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(EntityTask2Role entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _tasks2Roles.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(EntityTask2Role entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _tasks2Roles.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<EntityTask2Role>> GetTasksByRoleAsync(string roleId, CancellationToken cancellationToken = default)
    {
        var taskIds = await _tasks2Roles
            .Where(x => x.RoleId == roleId)
            .Distinct()
            .ToListAsync();

        return taskIds;
    }

    public async Task<List<EntityTask2Role>> GetTask2RoleAsync(int taskId,CancellationToken cancellationToken = default)
    {
        var taskIds = await _tasks2Roles
            .Where(x => x.TaskId == taskId)
            .Distinct()
            .ToListAsync();

        return taskIds;
    }

    public async Task<List<EntityTask2Role>> GetTasks2RolesAsync(CancellationToken cancellationToken = default)
    {
        var taskIds = await _tasks2Roles
            .Distinct()
            .ToListAsync();

        return taskIds;
    }
}
