using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Invoices.Commands.Delete;

public class DeletedInvoiceResponse : IDto
{
    public int Id { get; set; }
}