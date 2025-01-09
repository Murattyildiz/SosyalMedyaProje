namespace SosyalMedya_Web.Models
{
    public class ApiDataResponse<T>
    {
        public T Data { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

    }
}
