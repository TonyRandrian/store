namespace Store.Domain.Entities
{
    public class Document : MyFile
    {
        private readonly string[] AllowedExtension = { "txt", "doc", "docx", "pdf"};


        public Guid ProductId
        {
            get;
            set;
        }

        public override bool IsValidExtension()
        {
            return AllowedExtension.Contains(this.Extension);
        }
    }
}
