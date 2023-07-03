namespace ASPNETMVCCRUD.Models.Domain
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
