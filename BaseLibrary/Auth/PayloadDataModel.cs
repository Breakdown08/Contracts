namespace ContractsApi.BaseLibrary.Auth
{
    public class PayloadDataModel
    {
        public List<string> role { get; set; }
        public List<string> permission { get; set; }
        public ulong iat { get; set; }
        public ulong exp { get; set; }
    }
}