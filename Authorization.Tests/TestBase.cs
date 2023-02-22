﻿#region Usings

using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

#endregion

namespace Authorization.Tests
{
    public class TestBase
    {
        protected Mock<ILogger> Logger { get; set; }

        [SetUp]
        public virtual void Setup()
        {
            Logger = new Mock<ILogger>();
        }
    }
}