namespace ContractsApi.Models
{
    public class UniversityModel
    {
        public RequisitesModel? Requisites { get; set; }
        public string? DirectorFullName { get; set; }
        public string? DirectorShortName { get; set; }
        public string? DirectorShortNameReverse { get; set; }
    }
}