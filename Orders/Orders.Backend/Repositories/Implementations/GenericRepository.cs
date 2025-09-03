using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Backend.Repositories.Interfaces;
using Orders.shared.Responses;

namespace Orders.Backend.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DataContext _context;
    private readonly DbSet<T> _entity;

    public GenericRepository(DataContext context)
    {
        _context = context;
        _entity = context.Set<T>();
    }

    public virtual async Task<ActionResponses<T>> AddAsync(T entity)
    {
        _context.Add(entity);

        try
        {
            await _context.SaveChangesAsync();
            return new ActionResponses<T>
            {
                WasSuccess = true,
                Result = entity
            };
        }
        catch (DbUpdateException)
        {
            return DbUpdateExceptionActionResponse();
        }
        catch (Exception exception)
        {
            return ExceptionActionResponse(exception);
        }
    }

    public virtual async Task<ActionResponses<T>> DeteleAsync(int id)
    {
        var row = await _entity.FindAsync(id);
        if (row == null)
        {
            return new ActionResponses<T>
            {
                WasSuccess = false,
                Message = "Register Not Found"
            };
        }
        try
        {
            _entity.Remove(row);
            await _context.SaveChangesAsync();
            return new ActionResponses<T>
            {
                WasSuccess = true,
            };
        }
        catch
        {
            return new ActionResponses<T>
            {
                WasSuccess = false,
                Message = "not delete the redgister."
            };
        }
    }

    public virtual async Task<ActionResponses<T>> GetAsync(int id)
    {
        var row = await _entity.FindAsync(id);
        if (row != null)
        {
            return new ActionResponses<T>
            {
                WasSuccess = true,
                Result = row
            };
        }
        return new ActionResponses<T>
        {
            WasSuccess = false,
            Message = "Register Not Found"
        };
    }

    public virtual async Task<ActionResponses<IEnumerable<T>>> GetAsync()
    {
        return new ActionResponses<IEnumerable<T>>
        {
            WasSuccess = true,
            Result = await _entity.ToListAsync()
        };
    }

    public virtual async Task<ActionResponses<T>> UpdateAsync(T entity)
    {
        try
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return new ActionResponses<T>
            {
                WasSuccess = true,
                Result = entity
            };
        }
        catch (DbUpdateException)
        {
            return DbUpdateExceptionActionResponse();
        }
        catch (Exception exception)
        {
            return ExceptionActionResponse(exception);
        }
    }

    private ActionResponses<T> ExceptionActionResponse(Exception exception)
    {
        return new ActionResponses<T>
        {
            WasSuccess = false,
            Message = exception.Message
        };
    }

    private ActionResponses<T> DbUpdateExceptionActionResponse()
    {
        return new ActionResponses<T>
        {
            WasSuccess = false,
            Message = "The Register trying to create exists."
        };
    }
}