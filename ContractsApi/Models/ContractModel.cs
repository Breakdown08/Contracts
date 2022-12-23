namespace ContractsApi.Models
{
    public class ContractModel
    {
        public DateOnly? Date { get; set; }
        public UniversityModel? University { get; set; }
        public StudentModel? Student { get; set; }
        public PayerModel? Payer { get; set; }
        public string? CurrentStudyYear { get; set; }
        public string? NextStudyYear { get; set; }
        public string? CurrentAmount { get; set; }
        public string? TotalAmount { get; set; }
        public DateOnly? DateRectorOrder { get; set; }
        public string? Template { get; set; }
        public string? Template { get; set; }
    }
}