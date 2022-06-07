namespace API.DTOs.Response
{
    public class BaseResponse
    {
        private int statusCode;
        private string message;
        private object data;

        public BaseResponse StatusCode(int statusCode)
        {
            this.statusCode = statusCode;
            return this;
        }

        public int StatusCode ()
        {
            return statusCode;
        }

        public BaseResponse Message(string message)
        {
            this.message = message;
            return this;
        }

        public string Message()
        {
            return message;
        }

        public BaseResponse Data(object data)
        {
            this.data = data;
            return this;
        }

        public object Data()
        {
            return data;
        }
    }
}
