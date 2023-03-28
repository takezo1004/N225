
namespace N225.Domain.ValueObjects
{
    //基底クラス　abstract
    /// <summary>
    /// /// <summary>
    /// ValueObjectの基底クラス
    /// </summary>
    /// <param name="value"></param>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var vo = obj as T;
            if (vo == null)
            {
                return false;
            }
            //値が同じときはtrueでvalueobjectは同じとする
            return EqualsCore(vo);

        }
        //operatorを使って==,!= ２つ作らないとエラーになる
        /// <summary>
        /// ==
        /// </summary>
        /// <param name="vo1">vo1</param>
        /// <param name="vo2">vo2</param>
        /// <returns></returns>
        public static bool operator ==(ValueObject<T> vo1, ValueObject<T> vo2)
        {
            return Equals(vo1, vo2);
        }
        public static bool operator !=(ValueObject<T> vo1, ValueObject<T> vo2)
        {
            return !Equals(vo1, vo2);
        }
        protected abstract bool EqualsCore(T other);
        public override string ToString()
        {
            return base.ToString();
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
