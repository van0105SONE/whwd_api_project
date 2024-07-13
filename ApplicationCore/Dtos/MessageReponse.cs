namespace ApplicationCore.Dtos
{
    public class MessageReponse<T>
    {
        public bool isSuccess { get; set; }
        public String message { get; set; }

        public T data { get; set; }
    }
}
