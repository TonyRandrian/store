namespace Store.Domain.Entities
{
    public class Image : File
    {
        private readonly string[] AllowedExtension = { "png", "jpg", "jpeg", "webp" };


        public override bool IsValidExtension()
        {
            return AllowedExtension.Contains(Extension);
        }
    }
}
