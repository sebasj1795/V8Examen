namespace Security.Transversal.Common
{
    public static class Messages
    {
        public static readonly string OperationSuccess =
            "Se realizó la operación exitosamente.";
        public static readonly string ErrorControlled =
            "Error no controlado, contacte con el departamento de informatica.";
        public static readonly string ErrorCritical =
            "Error al efectuar la operación, por favor reintente. \n Si no es la primera vez, contáctese con el personal de Informática.";

        public static class Login
        {

            public const string UserNotExist = "El usuario ingresado no existe.";
            public const string UserBlocked = "El usuario se encuentra bloqueado temporalmente por intentos fallidos de acceso.";
            public const string UserBlockedWarning = "Las credenciales son incorrectas, en el próximo intento fallido, el usuario se bloqueará.";
            public const string CredentialIncorrect = "Las credenciales son incorrectas.";
            public const string ChangePasswordNotAllowed = "Cambio de contraseña no permitido.";
            public const string CredentialsDifferent = "Las nuevas credenciales deben ser diferentes.";
            public const string ChangePasswordSuccess = "Se cambió la contraseña correctamente.";
            public const string AppNotExist = "El código de aplicación no existe";
        }

        public static class Employee {

            public const string EndDate_DateNow_Invalid = "La fecha fin no puede ser menor a la fecha actual.";
            public const string EndDate_BeginDate_Invalid = "La fecha fin no puede ser menor a la fecha de Inicio.";

        }

    }
}
