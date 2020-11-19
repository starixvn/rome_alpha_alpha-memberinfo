namespace Alpha.MemberInfo.Tests.Fixtures
{
    using AutoFixture;

    public class AnyValueCreatorFixture
    {
        private readonly Fixture anyValueGenerator;

        public AnyValueCreatorFixture()
        {
            anyValueGenerator = new Fixture();
        }

        public T Create<T>()
        {
            return anyValueGenerator.Create<T>();
        }
    }
}
