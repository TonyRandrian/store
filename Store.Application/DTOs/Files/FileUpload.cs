namespace Store.Application.DTOs.Files
{
    public record FileUpload
    (
        Stream Content,
        string FileName,
        string ContentType,
        long Size
    );
}
