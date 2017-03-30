namespace GameStore.Services.Interfaces
{
    public interface IDomainEntityService<out TDto, in TKey>
    {
        TDto Get(TKey id);
    }
}
