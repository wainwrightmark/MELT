using System;
using MELT;
using MELT.Xunit;
using Microsoft.Extensions.Logging;
using SampleLibrary;
using Xunit;

namespace SampleLibrary.LegacyTests
{
    public class SampleTest
    {
        [Fact]
        public void DoSomethingLogsMessage()
        {
            // Arrange
            var loggerFactory = MELTBuilder.CreateLoggerFactory();
            var logger = loggerFactory.CreateLogger<Sample>();
            var sample = new Sample(logger);

            // Act
            sample.DoSomething();

            // Assert
            var log = Assert.Single(loggerFactory.LogEntries);
            // Assert the message rendered by a default formatter
            Assert.Equal("The answer is 42", log.Message);
        }

        [Fact]
        public void DoSomethingLogsCorrectParameter()
        {
            // Arrange
            var loggerFactory = MELTBuilder.CreateLoggerFactory();
            var logger = loggerFactory.CreateLogger<Sample>();
            var sample = new Sample(logger);

            // Act
            sample.DoSomething();

            // Assert
            var log = Assert.Single(loggerFactory.LogEntries);
            // Assert specific parameters in the log entry
            LogValuesAssert.Contains("number", 42, log);
        }

        [Fact]
        public void DoSomethingLogsUsingCorrectFormat()
        {
            // Arrange
            var loggerFactory = MELTBuilder.CreateLoggerFactory();
            var logger = loggerFactory.CreateLogger<Sample>();
            var sample = new Sample(logger);

            // Act
            sample.DoSomething();

            // Assert
            var log = Assert.Single(loggerFactory.LogEntries);
            // Assert the the log format template
            Assert.Equal("The answer is {number}", log.Format);
        }

        [Fact]
        public void DoExceptionalLogsException()
        {
            // Arrange
            var loggerFactory = MELTBuilder.CreateLoggerFactory();
            var logger = loggerFactory.CreateLogger<Sample>();
            var sample = new Sample(logger);

            // Act
            sample.DoExceptional();

            // Assert
            var log = Assert.Single(loggerFactory.LogEntries);

            // Assert the message rendered by a default formatter
            Assert.Equal("There was a problem", log.Message);
            // Assert specific parameters in the log entry
            LogValuesAssert.Contains("error", "problem", log);

            // Assert the exception
            var exception = Assert.IsType<ArgumentNullException>(log.Exception);
            Assert.Equal("foo", exception.ParamName);
        }
    }
}
