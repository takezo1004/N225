using Codeplex.Data;
using N225.Domain.Exceptions;
using System.Runtime.Serialization;


namespace N225.Domain.Elements
{

    [DataContract]
    public class RegisterElement
    {
        [DataMember(Name = "Symbol")]
        public string Symbol { get; set; }

        [DataMember(Name = "Exchange")]
        public string Exchange { get; set; }

    }

    public class RegisterResult
    {
        private static object RegisterMultipleDimToArray(dynamic objectJson)
        {
            var jsonArray = (dynamic[])objectJson.RegistList;

            if (jsonArray.Length == 0)
                return null;

            object[,] array = new object[jsonArray.Length, 2];
            int row = 0;

            foreach (RegisterElement item in jsonArray)
            {
                array[row, 0] = item.Symbol;
                array[row, 1] = item.Exchange;
                row++;
            }

            return array;
        }

        public static object RegisterCheck(string value)
        {
            var objectJson = DynamicJson.Parse(value);
            object ret;
            if (objectJson.IsDefined("Code"))
            {
                string error = "Eoor Code:" + objectJson["Code"] + " " + objectJson["Message"];

                throw new APIResponsesException(error);
            }

            ret = RegisterMultipleDimToArray(objectJson);
            return ret;
        }
    }
}
