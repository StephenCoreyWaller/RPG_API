namespace WebAPI_RPG.Models
{
    public class ServiceWrapper<T>
    {
        public T Data { get; set; }
        public bool DidSend { get; set; } = true; 
        public string Message { get; set; } = null; 
    }
}