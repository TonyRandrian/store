using Store.Domain.Entities;

namespace Store.Application.DTOs.Files.Images
{
    public class ImageResponse(MyFile file) : MyFileResponse(file)
    {
    }
}
