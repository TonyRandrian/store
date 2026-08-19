using MediatR;
using Store.Application.Interfaces;

namespace Store.Application.Features.Invoices.Commands.DeleteInvoice
{
    internal class DeleteInvoiceCommandHandler(IInvoiceRepository invoiceRepository)
        : IRequestHandler<DeleteInvoiceCommand>
    {
        private readonly IInvoiceRepository _invoiceRepository = invoiceRepository;


        public async Task Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            await _invoiceRepository.DeleteAsync(request.Id);
        }
    }
}
