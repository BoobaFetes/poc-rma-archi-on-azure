using ARO.Risk.Rma.Fopi.Domain.Common.Interface;

namespace ARO.Risk.Rma.Fopi.Domain.Common
{
    class ActualObject : IHaveFopiId, IHaveError
    {
        public int FopiId { get; set; }

        public Dictionary<string, ISet<string>> Error { get; set; } = new Dictionary<string, ISet<string>>();

        public string Property1 { get; set; } = string.Empty;
    }

    public class ErrorManagerTests
    {
        [Fact(DisplayName = "should add many errors to property when errors are flags")]
        public void Should_AddManyErrorsToProperty_When_ErrorsAreFlags()
        {
            ActualObject actual = new();
            var manager = new ErrorManager<ActualObject>(actual);

            manager.AddPropertyError("Property1", ErrorCode.ApiLayer | ErrorCode.NotLinked);

            Assert.True(actual.Error.ContainsKey("Property1"));
            Assert.Collection<string>(
                     actual.Error["Property1"],
                     (error) => Assert.Equal(ErrorCode.NotLinked.ToString(), error),
                     (error) => Assert.Equal(ErrorCode.ApiLayer.ToString(), error)
                 );
        }

        [Fact(DisplayName = "should throws when property not existing in type")]
        public void Should_Throw_When_PropertyNotExistsInType()
        {
            ActualObject actual = new();
            var manager = new ErrorManager<ActualObject>(actual);

            manager.AddPropertyError("Property1", ErrorCode.ApiLayer | ErrorCode.NotLinked);

            Assert.Throws<InvalidOperationException>(() => manager.AddPropertyError("unexpectedProperty", ErrorCode.Unkow));
        }
    }
}
