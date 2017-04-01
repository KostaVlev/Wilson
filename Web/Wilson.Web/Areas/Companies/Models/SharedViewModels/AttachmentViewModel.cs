namespace Wilson.Web.Areas.Companies.Models.SharedViewModels
{
    public class AttachmentViewModel
    {
        public string Id { get; set; }

        public string FileName { get; set; }

        public string Extention { get; set; }

        public override string ToString()
        {
            return this.FileName + "." + this.Extention;
        }
    }
}
