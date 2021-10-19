namespace Ivas.Common.Extensions
{
    public static class ObjectExtension
    {
        /// <summary>
        /// Evaluate if the object is Null.
        /// </summary>
        /// <param name="obj">The object to evaluate.</param>
        /// <returns>True if the object is null.</returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        /// <summary>
        /// Evaluate if the object is Not Null.
        /// </summary>
        /// <param name="obj">The object to evaluate.</param>
        /// <returns>True if its not null.</returns>
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }
    }
}
