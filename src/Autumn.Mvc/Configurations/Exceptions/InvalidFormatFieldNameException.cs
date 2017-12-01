namespace Autumn.Mvc.Configurations.Exceptions
{
    public class InvalidFormatFieldNameException : OptionBuilderException
    {
        public InvalidFormatFieldNameException(string fieldName, string value) : base(
            string.Format("Field name {1} already used by {0}", fieldName, value))
        {

        }
    }
}