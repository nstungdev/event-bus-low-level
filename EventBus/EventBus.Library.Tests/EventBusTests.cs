using System;
using Xunit;
using FluentAssertions;
using EventBus.Library.Models;
using EventBus.Library.Utils;
using Moq;
using System.Threading.Tasks;

namespace EventBus.Library.Tests
{
    public class EventBusTests
    {
        private readonly EventBus _eventBus;
        private readonly Mock<ITokenUtils> _itokenUtils;

        public EventBusTests()
        {
            _itokenUtils = new Mock<ITokenUtils>();
            _eventBus = new EventBus(_itokenUtils.Object);
        }

        [Fact]
        public void Subscribe_HappyPath()
        {
            // Arrange
            _itokenUtils.SetupSequence(x => x.GenerateNewToken()).Returns(new Token() { TokenId = "xyz" }).Returns(new Token() { TokenId = "abc" });

            // Act
            var token1 = _eventBus.Subscribe<BaseEvent>(async (evnt, args) => {  });
            var token2 = _eventBus.Subscribe<BaseEvent>(async (evnt, args) => { });

            // Assert
            token1.TokenId.Should().Be("xyz");
            token2.TokenId.Should().Be("abc");
        }

        [Fact]
        public async Task Publish_HappyPath()
        {
            // Arrange
            _itokenUtils.SetupSequence(x => x.GenerateNewToken()).Returns(new Token() { TokenId = "xyz" }).Returns(new Token() { TokenId = "abc" });
            var token1 = _eventBus.Subscribe<BaseEvent>(async (evnt, args) => { });

            // Act

            await _eventBus.Publish<BaseEvent>(null, null);
        }

        [Fact]
        public void UnSubscribe_HappyPath()
        {
            // Arrange
            _itokenUtils.SetupSequence(x => x.GenerateNewToken()).Returns(new Token() { TokenId = "xyz" }).Returns(new Token() { TokenId = "abc" });
            var token1 = _eventBus.Subscribe<BaseEvent>(async (evnt, args) => { });

            // Act
            _eventBus.UnSubscribe<BaseEvent>(token1.TokenId);
        }
    }
}
