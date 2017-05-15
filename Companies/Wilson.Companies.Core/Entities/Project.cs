namespace Wilson.Companies.Core.Entities
{
    public class Project : Entity
    {
        private Project()
        {
        }

        public string Name { get; private set; }

        public bool IsActive { get; private set; }

        public string Location { get; private set; }

        public string CustomerId { get; private set; }

        public string ContractId { get; private set; }        

        public virtual Company Customer { get; private set; }

        public virtual CompanyContract Contract { get; private set; }

        public void SetProjectLocation(Location location)
        {
            this.Location = location;
        }

        public void AddContract(string htmlContent, Employee createdBy)
        {
            var contract = CompanyContract.Create(htmlContent, createdBy, this);
            this.Contract = contract;
            this.ContractId = contract.Id;
        }
    }
}
