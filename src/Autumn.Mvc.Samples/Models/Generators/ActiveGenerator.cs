namespace Autumn.Mvc.Samples.Models.Generators
{
    public class ActiveGenerator : NullableGenerator<bool>
    {
        public ActiveGenerator() : base(reverse: true)
        {

        }

        protected override bool OnGenerate()
        {
            return Random().Next(0, 2) % 2 == 0;
        }
    }
}