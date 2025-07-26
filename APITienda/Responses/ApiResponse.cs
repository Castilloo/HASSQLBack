namespace APITienda.Responses;

public class ApiResponse<T>
{
    public bool Exito { get; set; } = true;
    public string Mensaje { get; set; } = string.Empty;
    public T? Data { get; set; }

    public ApiResponse() {}

    public ApiResponse(T data)
    {
        Data = data;
    }

    public ApiResponse(string mensaje, bool exito = true)
    {
        Exito = exito;
        Mensaje = mensaje;
    }

    public ApiResponse(string mensaje, T data, bool exito = true)
    {
        Exito = exito;
        Mensaje = mensaje;
        Data = data;
    }
}