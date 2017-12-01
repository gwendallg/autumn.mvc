using System;

namespace Autumn.Mvc.Configurations.Exceptions
{
    public class OptionBuilderException : AutumnException
    {
        /// <summary>
        /// class initializer
        /// </summary>
        protected OptionBuilderException()
        {
        }

        /// <summary>
        /// class initializer
        /// <param name="message">message exception</param>
        /// </summary>
        protected OptionBuilderException(string message) : base(message)
        {
        }

        /// <summary>
        /// class initializer
        /// <param name="message">message exception</param>
        /// <param name="innerException">inner exception</param>
        /// </summary>
        protected OptionBuilderException(string message, Exception innerException) : base(message,
            innerException)
        {
        }

        /// <summary>
        /// class initializer
        /// <param name="innerException">message exception</param>
        /// </summary>
        protected OptionBuilderException(Exception innerException) : base(innerException)
        {
        }
    }
}