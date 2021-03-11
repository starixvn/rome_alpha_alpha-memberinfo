namespace Alpha.Products.AccountInfo.Tests.Utility
{
    using System.Collections;
    using System.Collections.Generic;

    public class ActorAndSearchAccountLevelTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 4, 3 };
            yield return new object[] { 4, 2 };
            yield return new object[] { 4, 1 };
            yield return new object[] { 3, 2 };
            yield return new object[] { 3, 1 };
            yield return new object[] { 2, 1 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ActorAndDirectDownlineSearchAccountLevelTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 4, 3 };
            yield return new object[] { 3, 2 };
            yield return new object[] { 2, 1 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ActorAndCrossDownlineSearchAccountLevelTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 4, 2 };
            yield return new object[] { 4, 1 };
            yield return new object[] { 3, 1 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ActorIsNotSuperOrSearchAccountIsNotAgentLevelTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 4, 3 };
            yield return new object[] { 4, 1 };
            yield return new object[] { 3, 2 };
            yield return new object[] { 3, 1 };
            yield return new object[] { 2, 1 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ActorLevelTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 4 };
            yield return new object[] { 3 };
            yield return new object[] { 2 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}