﻿namespace E_Library.Data.DTOS.Response
{
    public class ResponseDto<T>
    {
        public string DisplayMessage { get; set; }
        public int StatusCode { get; set; }

        public T Result { get; set; }
    }
}
