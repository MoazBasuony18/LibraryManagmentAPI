using LibraryManagmentAPI.Common.Enums;

namespace LibraryManagmentAPI.Common.BaseResponse
{
    public class BaseCommandResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public ResponseCode responseCode {get;set;}
    }
}
