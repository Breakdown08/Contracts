namespace ContractsApi.Models
{
    public class ContractModel
    {
        public DateOnly? Date { get; set; }
        public UniversityModel? University { get; set; }
        public StudentModel? Student { get; set; }
        public PayerModel? Payer { get; set; }
        public string? Template { get; set; }
    }
}