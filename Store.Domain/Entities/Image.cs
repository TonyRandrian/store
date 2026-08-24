using System.Drawing;

namespace Store.Domain.Entities
{
    public class Image : MyFile
    {

        public Image(
            string fileName,
            string originalFileName,
            string path,
            string extension,
            long size,
            Product product) : base(fileName,
                                    originalFileName,
                                    path,
                                    extension,
                                    size,
                                    product)
        {

        }

        public Image() { }
    }
}
