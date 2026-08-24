namespace Store.Application.DTOs.Files
{
    public record CreateProductFile
    (
        Stream Content,
        string FileName,
        string ContentType,
        long Size
    );
}
