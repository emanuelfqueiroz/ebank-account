using CQRSHelper._Common;

namespace Application.TransferCommands.Transfer
{
    public class TrasnferCompletedResponse : Response
    {
        public string TrasnferNumber { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
