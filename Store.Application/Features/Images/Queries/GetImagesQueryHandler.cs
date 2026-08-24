using MediatR;
using Store.Application.Commons;
using Store.Application.DTOs.Files.Images;
using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;

namespace Store.Application.Features.Images.Queries
{
    public class GetImagesQueryHandler(IImageRepository imageRepository)
        : IRequestHandler<GetImagesQuery, PagedResult<ImageResponse>>
    {
        private readonly IImageRepository _imageRepository = imageRepository;


        public async Task<PagedResult<ImageResponse>> Handle(GetImagesQuery request, CancellationToken cancellationToken)
        {
            PagedResult<Image> images = await _imageRepository.GetAllAsync(request.PageNumber, request.PageSize);
            PagedResult<ImageResponse> responses = new()
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalRecords = images.TotalRecords
            };

            foreach (Image image in images.Data)
            {
                responses.Data.Add(new ImageResponse(image));
            }

            return responses;
        }
    }
}
