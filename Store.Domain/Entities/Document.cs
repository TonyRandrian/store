namespace Store.Domain.Entities
{
    public class Document : MyFile
    {
        public Guid ProductId
        {
            get;
            set;
        }
    }
}
