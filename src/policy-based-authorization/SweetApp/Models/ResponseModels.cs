namespace SweetApp.Models
{
    public class ResponseModel
    {
        public string EndPoint { get; private set; }
        public string Message { get; private set; }

        public ResponseModel(string endPoint, string message) 
        {
            EndPoint = endPoint;
            Message = message;
        }
    }
}
