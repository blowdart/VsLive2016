namespace MultipleAuthentication.Models
{
    public class NameSchemePair
    {
        public NameSchemePair()
        {
        }

        public NameSchemePair(string name, string authenticationType)
        {
            Name = name;
            AuthenticationType = authenticationType;
        }

        public string Name { get; set; }
        public string AuthenticationType { get; set; }
    }
}
