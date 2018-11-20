namespace Autumn.Mvc.Configurations.Exceptions
{
    public class AlreadyFieldNameUsedException: OptionBuilderException
    {
        public AlreadyFieldNameUsedException(string fieldName, string value) : base(
            string.Format("Field name {1} already used by {0}", fieldName, value))
        {

        }
    }
}