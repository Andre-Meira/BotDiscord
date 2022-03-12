public class ExecpetionObject : Exception
{
    public string Mensagem { get; set; }
    public object DataResponse { get; set; }
    public int StatusCode { get; set; }

    public ExecpetionObject(string mensagem, object dataResponse = null, int statusCode = 0) 
        : base(mensagem)
    {
        Mensagem = mensagem;
        DataResponse = dataResponse;
        StatusCode = statusCode;
    }
}