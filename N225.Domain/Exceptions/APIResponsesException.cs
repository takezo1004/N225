namespace N225.Domain.Exceptions
{
    public sealed class APIResponsesException : ExceptionBase
    {
        public APIResponsesException(string message)
            : base(message)
        {

        }
        /// <summary>
        /// 例外区分
        /// </summary>
        public override ExceptionKind Kind => ExceptionKind.Eorror;
    }
}
