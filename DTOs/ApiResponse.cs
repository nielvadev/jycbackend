namespace jycbackend.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; } = default!;
        public string Message { get; set; } = string.Empty;

        public ApiResponse() { }

        public ApiResponse(bool success, T data, string message)
        {
            Success = success; 
            Data = data;
            Message = message;
        }
    }
}
