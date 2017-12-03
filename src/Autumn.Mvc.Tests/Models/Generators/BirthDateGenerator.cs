using System;

namespace Autumn.Mvc.Tests.Models.Generators
{
    public class BirthDateGenerator : NullableGenerator<DateTime>
    {

        public BirthDateGenerator() : base(9)
        {

        }

        protected override DateTime OnGenerate()
        {
            return DateTime.Now.AddDays(-Random().Next(1, 1000));
        }
    }
}