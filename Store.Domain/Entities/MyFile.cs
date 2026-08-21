namespace Store.Domain.Entities
{
    public abstract class MyFile
    {
        private string _fileName;
        private string _originalFileName;
        private string _path;
        private string _extension;
        private Product _product;

        public MyFile(string fileName, string originalFileName, string path, string extension, long size)
        {
            FileName = fileName;
            OriginalFileName = originalFileName;
            Path = path;
            Extension = extension;
            Size = size;
        }

        public MyFile()
        { }


        public abstract bool IsValidExtension();


        public Guid Id
        {
            get;
            set;
        }

        public string FileName
        {
            get => _fileName;
            set
            {
                ArgumentException.ThrowIfNullOrEmpty(value, "Cannot create a file with an empty file name");
                _fileName = value;
            }
        }

        public string OriginalFileName
        {
            get => _originalFileName;
            set
            {
                ArgumentException.ThrowIfNullOrEmpty(value, "Cannot create a file with an emty original file name");
                _originalFileName = value;
            }
        }

        public string Path
        {
            get => _path;
            set
            {
                ArgumentException.ThrowIfNullOrEmpty(value, "Cannot create a file with an empty url");
                _path = value;
            }
        }

        public string Extension
        {
            get => _extension;
            set
            {
                ArgumentException.ThrowIfNullOrEmpty(value, "Cannot create a file with an empty extension");
                _extension = value;
            }
        }

        public long Size
        {
            get;
            set;
        }

        public Product Product
        {
            get => _product;
            set
            {
                ArgumentNullException.ThrowIfNull(value, "A file must be linked to a product");
                _product = value;
            }
        }
    }
}
