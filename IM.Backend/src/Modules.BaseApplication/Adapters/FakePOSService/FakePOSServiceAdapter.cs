using Application.Services.POSService;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace Application.Adapters.FakePOSService;

public class FakePOSServiceAdapter : IPOSService
{
    public Task Pay(string invoiceNo, decimal price)
    {
        Random random = new();
        bool result = Convert.ToBoolean(random.Next(2));
        if (!result)
            throw new BusinessException("Payment is not successful.");
        return Task.CompletedTask;
    }
}