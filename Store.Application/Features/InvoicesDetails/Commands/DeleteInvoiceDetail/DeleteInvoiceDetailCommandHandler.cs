using MediatR;
using Store.Application.Interfaces;

namespace Store.Application.Features.InvoicesDetails.Commands.DeleteInvoiceDetail
{
    public class DeleteInvoiceDetailCommandHandler(IInvoiceDetailsRepository invoiceDetailsRepository)
        : IRequestHandler<DeleteInvoiceDetailCommand>
    {
        private readonly IInvoiceDetailsRepository _invoiceDetailsRepository = invoiceDetailsRepository;


        public async Task Handle(DeleteInvoiceDetailCommand request, CancellationToken cancellationToken)
        {
            await _invoiceDetailsRepository.DeleteAsync(request.Id);
        }
    }
}
