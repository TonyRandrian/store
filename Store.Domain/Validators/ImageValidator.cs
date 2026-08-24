namespace Store.Domain.Validators
{
    public class ImageValidator
    {

        public static string ValidateAndGetExtension(string fileName, IEnumerable<string> allowedExtensions)
        {
            string extension = Path.GetExtension(fileName).TrimStart('.').ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                throw new ArgumentException($"Extension {extension} not valid");
            }

            return extension;
        }
    }
}
