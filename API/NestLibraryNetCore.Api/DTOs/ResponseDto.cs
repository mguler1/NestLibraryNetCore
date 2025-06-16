using System.Net;

namespace NestLibraryNetCore.Api.DTOs
{
    public record ResponseDto<T>
    {
        public T? Data { get; set; }

        public List<string>? Errors { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public static ResponseDto<T> Success(T data, HttpStatusCode status)
        {
            return new ResponseDto<T> { Data = data, HttpStatusCode = status };
        }

        public static ResponseDto<T> Fail(List<string> errors, HttpStatusCode status)
        {
            return new ResponseDto<T> { Errors=errors, HttpStatusCode = status };
        }
    }
}