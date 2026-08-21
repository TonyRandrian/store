using Store.Domain.Entities;

namespace Store.Application.DTOs.Files.Images
{
    public class MyFileResponse(MyFile file)
    {
        public Guid Id { get; set; } = file.Id;
        public string FileName { get; set; } = file.FileName;
        public string OriginalFileName { get; set; } = file.OriginalFileName;
        public string Path { get; set; } = file.Path;
        public string Extension { get; set; } = file.Extension;
        public Guid ProductId { get; set; } = file.Product.Id;
    }
}
