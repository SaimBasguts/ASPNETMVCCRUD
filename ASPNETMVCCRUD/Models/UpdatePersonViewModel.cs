namespace ASPNETMVCCRUD.Models
{
	public class UpdatePersonViewModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public DateTime DateOfBirth { get; set; }
	}
}
