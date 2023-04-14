namespace Modules.BaseApplication.Services.POSService;

public interface IPOSService
{
    Task Pay(string invoiceNo, decimal price); //todo: credit card etc.
}