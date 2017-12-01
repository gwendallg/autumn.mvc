namespace Autumn.Mvc.Configurations.Exceptions
{
    public class AlreadyFieldNameUsedException: OptionBuilderException
    {
        public AlreadyFieldNameUsedException(string fieldName, string value) : base(
            string.Format("Field identifier {1} : {0} does not respect the expected format", fieldName, value))
        {

        }
    }
}