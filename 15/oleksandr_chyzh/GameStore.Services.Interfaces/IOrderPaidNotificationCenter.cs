namespace GameStore.Services.Interfaces
{
    public interface IOrderPaidNotificationCenter
    {
        void NotifyManagers(int orderId);
    }
}