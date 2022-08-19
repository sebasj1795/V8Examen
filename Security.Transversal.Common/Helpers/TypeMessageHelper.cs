using Security.Transversal.Common.Enum;

namespace Security.Transversal.Common.Helpers
{
    public static class TypeMessageHelper
    {

        public static Response<T> Message<T>(Response response) where T : new()
        {
            return new Response<T>
            {
                Code = response.Code,
                Message = response.Message
            };
        }

        public static Response<T> MessageSuccess<T>(bool instanciarData = false) where T : new()
        {
            return new Response<T>
            {
                Code = (int)CodeResponseEnum.Success,
                Message = Messages.OperationSuccess,
                Data = instanciarData ? new T() : default(T) //default(T) para una clase es lo mismo que nulo
            };
        }

        public static Response MessageSuccess(string message) {
            Response response = new Response();
            response.Code = (int)CodeResponseEnum.Success;
            response.Message = message;
            return response;
        }

        public static Response MessageSuccess()
        {
            Response response = new Response();
            response.Code = (int)CodeResponseEnum.Success;
            response.Message = Messages.OperationSuccess;
            return response;
        }

        public static Response MessageWarning(string message)
        {
            Response response = new Response();
            response.Code = (int)CodeResponseEnum.ErrorControlled;
            response.Message = message;
            response.Data = null;
            return response;
        }

        public static Response<T> MessageWarning<T>(string message) where T : new()
        {
            return new Response<T>
            {
                Code = (int)CodeResponseEnum.ErrorControlled,
                Message = message,
            };
        }

        public static Response MessageError(string message)
        {
            Response response = new Response();
            response.Code = (int)CodeResponseEnum.ErrorCritical;
            response.Message = message;
            return response;
        }
    }
}
