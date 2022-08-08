namespace TwitterWebAPI.Models
{
    public class Response<T>
    {
        public T? Result { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
