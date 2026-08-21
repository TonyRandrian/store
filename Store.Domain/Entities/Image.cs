namespace Store.Domain.Entities
{
    public class Image : MyFile
    {
        private readonly string[] AllowedExtension = { "png", "jpg", "jpeg", "webp" };


        public override bool IsValidExtension()
        {
            return AllowedExtension.Contains(Extension);
        }
    }
}
