namespace N225.Domain.Exceptions
{
    public class KabuException : ExceptionBase
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="exception">元になった例外</param>
        public KabuException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// 例外区分
        /// </summary>
        public override ExceptionKind Kind => ExceptionKind.Eorror;
    }
}
