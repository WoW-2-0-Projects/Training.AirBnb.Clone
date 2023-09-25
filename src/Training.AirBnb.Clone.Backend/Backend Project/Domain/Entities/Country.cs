using Backend_Project.Domain.Common;

namespace Backend_Project.Domain.Entities
{
    public class Country : Entity
    {
        public string Name { get; set; }
        public Country(string name)
        {
            Name = name;
        }
    }
}
