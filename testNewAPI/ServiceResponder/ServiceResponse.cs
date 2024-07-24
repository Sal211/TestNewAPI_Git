namespace testNewAPI.ServicesResponse
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; } = true;
        public string? Message { get; set; } 
        public string? Error { get; set; }
        public T? Data { get; set; }
        public ServiceResponse()
        {
            Success = false;    
            Message = string.Empty; 
            Error = string.Empty;    
            Data = default(T); 
        }
    }
}
