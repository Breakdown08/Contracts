using System.IO;

namespace ContractsApi.Models
{
    public class RequisitesModel
    {
        public string? MainOrganizationName { get; set; }
        public string? MainAddress { get; set; }
        public string? ChildOrganizationName { get; set; }
        public string? ChildAddress { get; set; }
        public string? ChildPhone { get; set; }
        public string? ChildEmail { get; set; }
        public string? ChildFax { get; set; }
        public string? ChildINN { get; set; }
        public string? ChildKPP { get; set; }
        public string? ChildUFK { get; set; }
        public string? ChildBIK { get; set; }
        public string? ChildKS { get; set; }
        public string? ChildEKS { get; set; }
        public string? ChildOKVED { get; set; }
        public string? ChildOKPO { get; set; }
        public string? ChildPurposePayment { get; set; }
    }
}