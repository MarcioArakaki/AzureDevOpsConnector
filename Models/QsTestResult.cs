namespace AzureDevOpsConnector.Models
{
    public class QsTestResult
    {
        public string QueryString { get; set; }

        public string ResponseMessage { get; set; }

        public string UserFriendlyMessage { get; set; }
        public int Status { get; set; }
        
    }
}