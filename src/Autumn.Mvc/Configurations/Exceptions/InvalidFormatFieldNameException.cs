namespace Autumn.Mvc.Configurations.Exceptions
{
    public class InvalidFormatFieldNameException : OptionBuilderException
    {
        public InvalidFormatFieldNameException(string fieldName, string value) : base(
            string.Format("Field identifier {1} : {0} does not respect the expected format", fieldName, value))
        {

        }
    }
}