public class ExecpetionObject : Exception
{
    public string Mensagem { get; set; }
    public object Data { get; set; }
    public int StatusCode { get; set; }

    public ExecpetionObject(string mensagem, object data = null, int statusCode = 0) 
        : base(mensagem)
    {
        Mensagem = mensagem;
        Data = data;
        StatusCode = statusCode;
    }
}