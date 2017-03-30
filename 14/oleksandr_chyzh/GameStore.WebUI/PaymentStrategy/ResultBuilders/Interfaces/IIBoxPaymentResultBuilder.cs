namespace GameStore.WebUI.PaymentStrategy.ResultBuilders.Interfaces
{
    public interface IIBoxPaymentResultBuilder
    {
        string GetResult(string customerId, int orderId, decimal sum);
    }
}
