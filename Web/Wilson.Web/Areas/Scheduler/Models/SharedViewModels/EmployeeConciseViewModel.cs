namespace Wilson.Web.Areas.Scheduler.Models.SharedViewModels
{
    public class EmployeeConciseViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }
    }
}
