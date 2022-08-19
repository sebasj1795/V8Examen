namespace Security.Transversal.Common.Wapper
{
    public class LoginLockSetting
    {
        /// <summary>
        /// Tiempo de bloqueo en minutos
        /// </summary>
        public int LockTime { get; set; }

        /// <summary>
        /// Número de intentos fallidos permitidos
        /// </summary>
        public int AttemptsAllowed { get; set; }
    }
}
