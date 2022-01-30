namespace Babylon.Transactions.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static byte[] FromStringToByteArray(this string stringToConvert)
        {
            // From string to byte array
            var buffer = System.Text.Encoding.UTF8.GetBytes(stringToConvert);

            return buffer;
        }

        public static string FromByteArrayToString(this byte[] arrayToStringify)
        {
            // From byte array to string
            var stringToReturn = System.Text.Encoding.UTF8.GetString(arrayToStringify);

            return stringToReturn;
        }
    }
}