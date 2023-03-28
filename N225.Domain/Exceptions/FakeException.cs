using System;

namespace N225.Domain.Exceptions
{
    /// <summary>
    /// Fake例外
    /// </summary>
    public sealed class FakeException : ExceptionBase
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="exception">元になった例外</param>
        public FakeException(string message, Exception exception)
            : base(message, exception)
        {

        }

        /// <summary>
        /// 例外区分
        /// </summary>
        public override ExceptionKind Kind => ExceptionKind.Eorror;
    }
}
