using N225.Domain.CommonConst;
using N225.Domain.Exceptions;
using N225.Domain.StaticVlues;
using System;
using System.Linq;

namespace N225.Domain.Modules.Utils
{
    public abstract class Validate
    {

        /// <summary>
        /// トークンチェック
        /// </summary>
        public static string ValidateToken(string password, string port)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");
            //return ResultMessage.NoPasswordEntered;

            if (password.ToString().Length > 15)
                throw new ArgumentException(ResultMessage.OutofRangeLength);
            //return ResultMessage.OutofRangeLength;

            // ポートチェック
            if (string.IsNullOrEmpty(port))
                throw new ArgumentException(ResultMessage.NoPortEntered);
            //return ResultMessage.NoPortEntered;

            if (!int.TryParse(port, out _))
                throw new ArgumentException(ResultMessage.PortIsNotNumeric);
            //return ResultMessage.PortIsNotNumeric;

            if (port.Length > 5)
                throw new ArgumentException(ResultMessage.OutofRangeLength);
            //return ResultMessage.OutofRangeLength;

            return null;
        }

        public static void ValidateSendOrder(int selectedOrder, int tradeType,
                                    string side, string symbol, double qty,
                                    double price, double stopPrice, string excutionId = "")
        {
            if (selectedOrder > 3)
            {
                throw new ArgumentException(ResultMessage.OutofRangeLength);
            }

            if (side == Side.Sell || side == Side.Buy)
            { }
            else
                throw new KabuException("Sideが違います");

            if (string.IsNullOrEmpty(symbol))
            {
                throw new KabuException("Symbolが違います");
            }
            if (qty <= 0)
            {
                throw new ArgumentException("注文枚数を入力してください");
                //throw new KabuException("注文枚数を入力してください");
            }

            if (selectedOrder == 2)      //指値
            {
                if (price == 0)
                {
                    throw new KabuException("注文価格を入力してください");
                }
            }
            if (selectedOrder == 3)      //指値
            {
                if (stopPrice == 0)
                {
                    throw new KabuException("注文価格を入力してください");
                }
            }
            if (tradeType == TradeType.ExitOrder)
            {
                if (string.IsNullOrEmpty(excutionId))
                {
                    throw new KabuException("ExcutionIdがありません");
                }
                double leaveQty = PositionsCache.GetLeaveQty(excutionId);
                if (qty > leaveQty)
                {
                    throw new KabuException("返済枚数が違います");
                }
            }
        }

        /// <summary>
        /// 発注制御チェック
        /// </summary>
        public static string ValidateOrderCancel(string value1, string value2)
        {

            //if (!(CustomRibbon._orderPressed))
            //    return ResultMessage.OrderIsNotValid;

            string result = ValidateCommon();
            if (string.IsNullOrEmpty(result))
            {

                if (string.IsNullOrEmpty(value1.ToString()))
                    return ResultMessage.NotEntered;

                if (string.IsNullOrEmpty(value2.ToString()))
                    return ResultMessage.OutofRangeLength;
            }

            return result;
        }

        /// <summary>
        /// 引数が両方入力されている場合のみOK
        /// </summary>
        public static string ValidateRequired(string value1, string value2)
        {

            string result = ValidateCommon();

            if (string.IsNullOrEmpty(result))
                if (string.IsNullOrEmpty(value1.ToString()) || string.IsNullOrEmpty(value2.ToString()))
                    return ResultMessage.BadRequest;

            return result;
        }

        /// <summary>
        /// 引数が両方Nullまたは両方入力されている場合のみOK
        /// </summary>
        public static string ValidateRequired2(string value1, string value2)
        {

            string result = ValidateCommon();

            if (string.IsNullOrEmpty(result))
                if (string.IsNullOrEmpty(value1.ToString()) ^ string.IsNullOrEmpty(value2.ToString()))
                    return ResultMessage.BadRequest;


            return result;
        }

        /// <summary>
        /// 引数がすべて入力されている場合のみOK（入力が正しい場合はnullを返す）
        /// ※params string[] は Excel-DNA で実行時エラーになるので、代わりに params object[] としている
        /// Initialization [Error] Method not registered - unsupported signature, abstract or generic:
        /// </summary>
        public static string ValidateRequiredAll(params object[] values)
        {
            // ValidateCommon()の結果がnullではない → チェックでエラー発生
            string result = ValidateCommon();
            if (!string.IsNullOrEmpty(result)) return result;

            foreach (var v in values)
            {
                if (v == null || v.ToString() == "")
                {
                    return ResultMessage.BadRequest;
                }
            }

            return result;
        }

        /// <summary>
        /// 入力チェック（単項目）
        /// </summary>
        public static string ValidateMultiple(object[] values)
        {
            string result = ValidateCommon();

            if (string.IsNullOrEmpty(result))
            {
                if (values.All(x => string.IsNullOrEmpty(x.ToString())))
                    return ResultMessage.BadRequest;
            }

            return result;
        }

        /// <summary>
        /// REGISTER系の範囲選択チェック
        /// </summary>
        public static string ValidateRegister(object[,] array)
        {

            string result = ValidateCommon();

            if (string.IsNullOrEmpty(result))
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        if (array.GetLength(1) != 2)
                            return ResultMessage.BadRequest;

                        else if (string.IsNullOrEmpty(array[i, j].ToString()))
                            return ResultMessage.BadRequest;

                        else if (array[i, j].ToString() == "ExcelDna.Integration.ExcelMissing")
                            return ResultMessage.BadRequest;

                        else if (array[i, j].ToString() == "ExcelDna.Integration.ExcelEmpty")
                            return ResultMessage.BadRequest;

                        else if (!int.TryParse(array[i, j].ToString(), out _))
                            return ResultMessage.BadRequest;
                    }
                }

            return result;
        }

        /// <summary>
        /// PUSH配信チェック
        /// </summary>
        public static string ValidateRtdBoard(bool WebsocketStart, string symbol, string exchange, string itemName)
        {

            string result = null;
            if (!WebsocketStart)
                result = ValidateCommon();

            if (string.IsNullOrEmpty(symbol))
                return ResultMessage.BadRequest;

            if (string.IsNullOrEmpty(exchange))
                return ResultMessage.BadRequest;

            if (!int.TryParse(exchange, out _))
                return ResultMessage.BadRequest;

            if (string.IsNullOrEmpty(itemName))
                return ResultMessage.BadRequest;

            return result;
        }

        /// <summary>
        /// 共通チェック
        /// </summary>
        public static string ValidateCommon()
        {

            //// トークン取得チェック
            //if (string.IsNullOrEmpty(CustomRibbon._token))
            //    return ResultMessage.TokenNotIssued;

            //// ポートチェック
            //if (string.IsNullOrEmpty(CustomRibbon._port))
            //    return ResultMessage.NoPortEntered;

            //if (!int.TryParse(CustomRibbon._port, out _))
            //    return ResultMessage.PortIsNotNumeric;

            //if (CustomRibbon._port.Length > 5)
            //    return ResultMessage.OutofRangeLength;

            return null;
        }
    }


}
