namespace StaffUI.Models
{
    public class Staff
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public List<int> ProjectName { get; set; }
    }
}
