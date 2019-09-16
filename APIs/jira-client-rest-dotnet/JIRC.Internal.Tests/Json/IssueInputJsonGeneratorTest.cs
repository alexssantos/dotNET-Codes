// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IssueInputJsonGeneratorTest.cs" company="David Bevin">
//   Copyright (c) David Bevin.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using JIRC.Domain.Input;
using JIRC.Internal.Json.Gen;

using NUnit.Framework;

using ServiceStack.Text;

namespace JIRC.Internal.Tests.Json
{
    [TestFixture]
    public class IssueInputJsonGeneratorTest
    {
        [Test]
        public void GenerateBasic()
        {
            var dict = new Dictionary<string, object> { { "string", "string" }, { "integer", 1 }, { "long", 1L }, { "complex", ComplexIssueInputFieldValue.With("test", "id") } };
            var fields = new List<FieldInput>
            {
                new FieldInput("string", "String value"),
                new FieldInput("integer", 1),
                new FieldInput("long", 1L),
                new FieldInput("complex", new ComplexIssueInputFieldValue(dict))
            };

            var issueInput = IssueInput.CreateWithFields(fields);
            var expected = JsonResource.IssueInput_Valid;

            var actual = IssueInputJsonGenerator.Generate(issueInput);

            Assert.AreEqual(expected, actual.ToJson());
        }

        [Test]
        public void GenerateWithEmptyInput()
        {
            var issueInput = new IssueInput(new Dictionary<string, FieldInput>());
            var expected = JsonResource.IssueInput_Empty;

            var actual = IssueInputJsonGenerator.Generate(issueInput);

            Assert.AreEqual(expected, actual.ToJson());
        }

        [Test]
        public void GenerateWithNullInput()
        {
            var expected = JsonResource.IssueInput_Empty;

            var actual = IssueInputJsonGenerator.Generate(null);

            Assert.AreEqual(expected, actual.ToJson());
        }
    }
}
