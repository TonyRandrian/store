using Store.Domain.Entities;

namespace Store.Application.DTOs.Files.Images
{
    public class DocumentResponse(MyFile file) : MyFileResponse(file)
    {
    }
}
