namespace API.DTOs.Response
{
    public class BaseResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public object? Data { get; set; } = null;

        public BaseResponse()
        {
        }

        public BaseResponse(int StatusCode, string Message, object? Data = null)
        {
            this.StatusCode = StatusCode;
            this.Message = Message;
            this.Data = Data;
        }

        public BaseResponse SetStatusCode(int code) 
        {
            StatusCode = code; 
            return this;
        }

        public BaseResponse SetMessage(string message)
        {
            Message = message;
            return this;
        }

        public BaseResponse SetData(object data)
        {
            Data = data;
            return this;
        }
    }
}
