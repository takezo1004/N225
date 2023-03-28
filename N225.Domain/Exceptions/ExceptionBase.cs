using System;

namespace N225.Domain.Exceptions
{
    /// <summary>
    /// 例外規定クラス
    /// </summary>
    public abstract class ExceptionBase : Exception
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="message">メッセージ</param>
        public ExceptionBase(string message) : base(message)
        {
        }

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="exception">元になった例外</param>
        public ExceptionBase(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        /// 例外区分
        /// </summary>
        public abstract ExceptionKind Kind { get; }

        /// <summary>
        /// 例外区分
        /// </summary>
        public enum ExceptionKind
        {
            Info,
            Warning,
            Eorror,
        }
    }
}
