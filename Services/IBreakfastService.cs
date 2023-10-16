using BuberBreakfast.Models;
namespace BuberBreakfast.Services;

public interface IBreakfastService
{
    public IEnumerable<Breakfast> GetMany(SearchBreakfastDto searchBreakfastDto);
    public Breakfast? GetUnique(int id);
    public bool DeleteUnique(int id);
    public Breakfast? CreateUnique(CreateBreakfastDto createBreakfastDto);
    public Breakfast? UpdateUnique(int id, UpdateBreakfastDto updateBreakfastDto);
}
