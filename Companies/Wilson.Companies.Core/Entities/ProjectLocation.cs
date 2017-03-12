namespace Wilson.Companies.Core.Entities
{
    public class ProjectLocation : Entity, IValueObject<ProjectLocation>
    {
        public string Country { get; set; }

        public string PostCode { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string Note { get; set; }

        public int? StreetNumber { get; set; }

        public bool Equals(ProjectLocation other)
        {
            if (this.City.Equals(other.City))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
