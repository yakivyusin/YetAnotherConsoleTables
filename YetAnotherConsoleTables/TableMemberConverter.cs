using System;

namespace YetAnotherConsoleTables
{
    /// <summary>
    /// Converts an object to string.
    /// This class must have public parameterless constructor.
    /// </summary>
    public abstract class TableMemberConverter
    {
        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool CanConvert(Type objectType);

        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        /// <param name="value">The value.</param>
        public abstract string Convert(object value);
    }

    /// <summary>
    /// Converts an object to string.
    /// It class must have public parameterless constructor.
    /// </summary>
    public abstract class TableMemberConverter<T> : TableMemberConverter
    {
        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        /// <param name="value">The value.</param>
        public abstract string Convert(T value);

        /// <summary>
        /// Returns the string representation of the object.
        /// </summary>
        /// <param name="value">The value.</param>
        public sealed override string Convert(object value)
        {
            if (!(value != null ? value is T : IsNullable(typeof(T))))
            {
                throw new ArgumentException("Converter cannot convert specified value to string.");
            }
            return Convert((T)value);
        }

        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        /// 	<c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public sealed override bool CanConvert(Type objectType) => typeof(T).IsAssignableFrom(objectType);

        private bool IsNullable(Type type) => !type.IsValueType || Nullable.GetUnderlyingType(type) != null;
    }
}
