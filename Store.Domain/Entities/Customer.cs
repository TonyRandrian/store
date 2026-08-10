namespace Store.Domain.Entities
{
    internal class Customer
    {

        public Customer(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id
        {
            get;

            set;
        }

        public string Name
        {
            get;

            set
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value, "Cannot create a customer with a null or empty name");
                field = value;
            }
        }
    }
}
