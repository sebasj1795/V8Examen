﻿namespace Security.Transversal.Common
{
    public class Response<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
    }

    public class Response
    {
        public dynamic Data { get; set; }
        public string Message { get; set; }
        public int Code { get; set; }
    }

}
