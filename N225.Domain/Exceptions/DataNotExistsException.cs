
namespace N225.Domain.Exceptions
{
    /// <summary>
    /// データなし例外
    /// </summary>
    public sealed class DataNotExistsException : ExceptionBase
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        public DataNotExistsException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 区分
        /// </summary>
        public override ExceptionKind Kind => ExceptionKind.Info;
    }
}
