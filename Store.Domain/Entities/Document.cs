namespace Store.Domain.Entities
{
    public class Document : File
    {
        private readonly string[] AllowedExtension = { "txt", "doc", "docx", "pdf"};


        public override bool IsValidExtension()
        {
            return AllowedExtension.Contains(this.Extension);
        }
    }
}
