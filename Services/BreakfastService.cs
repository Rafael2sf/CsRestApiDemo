using BuberBreakfast.Data;
using BuberBreakfast.Models;
using BuberBreakfast.Services;
using Microsoft.EntityFrameworkCore;

namespace BuberBreakfast.Services;

public class BreakfastService : IBreakfastService
{
    private readonly ApplicationDbContext _db;

    public BreakfastService(ApplicationDbContext db)
    {
        _db = db;
    }

    public Breakfast? CreateUnique(CreateBreakfastDto createBreakfastDto)
    {
        try
        {
            var instance = _db.Breakfast.Add(new Breakfast(
                0,
                createBreakfastDto.Name,
                createBreakfastDto.Description,
                createBreakfastDto.Duration,
                DateTime.UtcNow
            )).Entity;
            _db.SaveChanges();
            return instance;
        }
        catch
        {
            return null;
        }
    }

    public bool DeleteUnique(int id)
    {
        var b = _db.Breakfast.SingleOrDefault(x => x.Id == id);
        if (b is null)
            return false;
        _db.Breakfast.Remove(b);
        _db.SaveChanges();
        return true;
    }

    public IEnumerable<Breakfast> GetMany(SearchBreakfastDto searchBreakfastDto)
    {
        return _db.Breakfast
            .Where(
            x =>
                (searchBreakfastDto.Name == null || x.Name.Contains(searchBreakfastDto.Name)
                && x.Duration <= searchBreakfastDto.MaxDuration)
        )
        .OrderBy(searchBreakfastDto => searchBreakfastDto.Name)
        .Take(searchBreakfastDto.Limit);
    }

    public Breakfast? GetUnique(int id)
    {
        return _db.Breakfast.Find(id);
    }

    public Breakfast? UpdateUnique(int id, UpdateBreakfastDto updateBreakfastDto)
    {
        try
        {
            var instance = this.GetUnique(id);
            if (instance is null)
                return null;
            var updated = _db.Update(instance);
            updated.Entity.Name = updateBreakfastDto.Name;
            updated.Entity.Description = updateBreakfastDto.Description;
            updated.Entity.Duration = updateBreakfastDto.Duration;
            updated.Entity.LastModifiedDate = DateTime.UtcNow;
            _db.SaveChanges();
            return updated.Entity;
        }
        catch
        {
            return null;
        }
    }
}
