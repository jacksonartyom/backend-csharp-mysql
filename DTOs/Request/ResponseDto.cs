public class ResponseDto<T>
{
    public string Message { get; set; } = string.Empty;
    public T? Result { get; set; }

}