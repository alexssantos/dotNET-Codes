// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IssueInputJsonGeneratorTest.cs" company="David Bevin">
//   Copyright (c) David Bevin.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JIRC.Domain.Input;
using JIRC.Internal.Json.Gen;

using NUnit.Framework;

using ServiceStack.Text;
using JIRC.Domain;
using JIRC.Clients;

namespace JIRC.Internal.Tests.Json
{
    [TestFixture]
    class IssueEditMetaJsonGeneratorTest
    {
        /// <summary>
        /// Custom field name used in tests.
        /// </summary>
        private const string TestCustomFieldName = "customfield_1234";

        /// <summary>
        /// Custom field name used in tests.
        /// </summary>
        private const string TestRegisteredCustomFieldName = "customfield_5678";

        /// <summary>
        /// Custom field value used in tests.
        /// </summary>
        private const string TestCustomFieldStringValue = "Hello, World!";

        /// <summary>
        /// Custom field value used in tests.
        /// </summary>
        private const int TestCustomFieldIntValue = 546;

        /// <summary>
        /// Initializes the text fixture.
        /// </summary>
        [TestFixtureSetUp]
        public void TestClassInitialize()
        {
            // Create an instance of JiraRestClient to cause all static constructors in JIRC to be executed.
            // We need this so that JsonDataContractDeserializer will call JIRC's custom parsing handlers when parsing Issue from string.
            var client = JiraRestClientFactory.CreateWithBasicHttpAuth(
                new Uri("http://mock"),
                "mock",
                "mock");

            // Register a converter for custom Issue field type.
            Func<string, object> customTypeParser = (string json) =>
            {
                JsonObject parsed = JsonObject.Parse(json);

                CustomType custom = new CustomType();
                custom.CustomStringProperty = parsed["customstring"];
                custom.CustomIntProperty = int.Parse(parsed["customint"]);

                return custom;
            };

            Func<object, string> customTypeSerializer = (object obj) =>
            {
                JsonObject serialized = new JsonObject();
                serialized.Add("customstring", (obj as CustomType).CustomStringProperty);
                serialized.Add("customint", (obj as CustomType).CustomIntProperty.ToString());

                return serialized.ToJson();
            };

            CustomFieldTypes.Register(TestRegisteredCustomFieldName, customTypeParser, customTypeSerializer);
        }

        /// <summary>
        /// Tests that <see cref="Issue"/> parser registered with Service Stack
        /// correctly handles null custom fields with no converter.
        /// </summary>
        [Test]
        public void TestParseDefaultNull()
        {
            var issue = JsonResource.Issue_NullCustomField.FromJson<Issue>();

            Assert.IsTrue(issue.CustomFields.Exists(TestCustomFieldName), "Custom field was not parsed correctly.");
            Assert.IsNull(issue.CustomFields[TestCustomFieldName], "Null custom field without a converter was not parsed correctly.");
        }

        /// <summary>
        /// Tests that <see cref="Issue"/> parser registered with Service Stack
        /// correctly handles string custom fields with no converter.
        /// </summary>
        [Test]
        public void TestParseDefaultString()
        {
            var issue = JsonResource.Issue_StringCustomField.FromJson<Issue>();

            Assert.AreEqual(TestCustomFieldStringValue, issue.CustomFields[TestCustomFieldName], "String custom field without a converter was not parsed correctly.");
        }

        /// <summary>
        /// Tests that <see cref="Issue"/> parser registered with Service Stack
        /// correctly handles custom types with a main "value" property in custom fields with no converter.
        /// </summary>
        [Test]
        public void TestParseDefaultSimpleCustomType()
        {
            var issue = JsonResource.Issue_SimpleTypeCustomField.FromJson<Issue>();

            Assert.AreEqual(TestCustomFieldStringValue, issue.CustomFields[TestCustomFieldName], "Simple type custom field without a converter was not parsed correctly.");
        }

        /// <summary>
        /// Tests that <see cref="Issue"/> parser registered with Service Stack
        /// correctly handles unknown custom types in custom fields with no converter.
        /// </summary>
        [Test]
        public void TestParseDefaultUnknownCustomType()
        {
            var issue = JsonResource.Issue_CustomTypeCustomField.FromJson<Issue>();

            Assert.AreEqual(null, issue.CustomFields[TestCustomFieldName], "Simple type custom field without a converter was not parsed correctly.");
        }

        /// <summary>
        /// Tests that <see cref="Issue"/> parser registered with Service Stack
        /// correctly handles custom fields that have a converter registered with <see cref="CustomFieldTypes"/>.
        /// </summary>
        [Test]
        public void TestParseConvertedType()
        {
            var issue = JsonResource.Issue_RegisteredTypeCustomField.FromJson<Issue>();

            Assert.AreEqual(typeof(CustomType), issue.CustomFields[TestRegisteredCustomFieldName].GetType(), "Custom field with registered converter was not parsed correctly by the converter.");
            Assert.AreEqual((issue.CustomFields[TestRegisteredCustomFieldName] as CustomType).CustomIntProperty, TestCustomFieldIntValue, "Custom field with registered converter was not parsed correctly by the converter.");
            Assert.AreEqual((issue.CustomFields[TestRegisteredCustomFieldName] as CustomType).CustomStringProperty, TestCustomFieldStringValue, "Custom field with registered converter was not parsed correctly by the converter.");
        }

        /// <summary>
        /// Tests that attempting to register null parser or serializer with <see cref="CustomFieldTypes"/>
        /// will throw a specific exception.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRegisterNullConverter()
        {
            CustomFieldTypes.Register("customfield_another", null, null);
        }

        /// <summary>
        /// Tests that attempting to register a custom field without "customfield" prefix
        /// will throw a specific exception.
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRegisterConverterForInvalidFieldName()
        {
            CustomFieldTypes.Register("myfield", (string json) => { return new CustomType(); }, (object obj) => { return string.Empty; });
        }

        /// <summary>
        /// Tests that editing an <see cref="Issue"/> field using <see cref="IssueEditMetaJsonGenerator"/>
        /// correctly handles null custom fields with no converter.
        /// </summary>
        [Test]
        public void TestEditFieldDefaultNull()
        {
            var mock = new MockJsonServiceClient();
            var client = new JiraIssueRestClient(mock, null, null);
            var issue = JsonResource.Issue_StringCustomField.FromJson<Issue>();

            client.EditField(issue, TestCustomFieldName, null);

            // Check the JSON generated for a field update to ensure it matches expected JSON for a null field.
            Assert.AreEqual("{customfield_1234:[{}]}", mock.PutRequest["update"], "Unexpected Json generated for editing a null custom field with no converter.");
        }

        /// <summary>
        /// Tests that editing an <see cref="Issue"/> field using <see cref="IssueEditMetaJsonGenerator"/>
        /// correctly handles string custom fields with no converter.
        /// </summary>
        [Test]
        public void TestEditFieldString()
        {
            var mock = new MockJsonServiceClient();
            var client = new JiraIssueRestClient(mock, null, null);
            var issue = JsonResource.Issue_StringCustomField.FromJson<Issue>();

            client.EditField(issue, TestCustomFieldName, TestCustomFieldStringValue);

            // Check the JSON generated for a field update to ensure it matches expected JSON for a string field.
            Assert.AreEqual("{customfield_1234:[{set:Hello, World!}]}", mock.PutRequest["update"], "Unexpected Json generated for editing a string custom field with no converter.");
        }

        /// <summary>
        /// Tests that editing an <see cref="Issue"/> field using <see cref="IssueEditMetaJsonGenerator"/>
        /// correctly handles custom type fields with no converter.
        /// </summary>
        [Test]
        public void TestEditFieldCustomType()
        {
            var mock = new MockJsonServiceClient();
            var client = new JiraIssueRestClient(mock, null, null);
            var issue = JsonResource.Issue_StringCustomField.FromJson<Issue>();

            var custom = new CustomType();
            custom.CustomIntProperty = TestCustomFieldIntValue;
            custom.CustomStringProperty = TestCustomFieldStringValue;

            client.EditField(issue, TestCustomFieldName, custom);

            // Check the JSON generated for a field update to ensure it matches expected JSON for
            // a custom type field with no converter (this should dump custom type name).
            Assert.AreEqual(
                "{customfield_1234:[{set:JIRC.Internal.Tests.Json.CustomType}]}",
                mock.PutRequest["update"], "Unexpected Json generated for editing a string custom field with no converter.");
        }

        /// <summary>
        /// Tests that editing an <see cref="Issue"/> field using <see cref="IssueEditMetaJsonGenerator"/>
        /// correctly handles custom fields with a registered converter.
        /// </summary>
        [Test]
        public void TestEditFieldConvertedType()
        {
            var mock = new MockJsonServiceClient();
            var client = new JiraIssueRestClient(mock, null, null);
            var issue = JsonResource.Issue_StringCustomField.FromJson<Issue>();

            var custom = new CustomType();
            custom.CustomIntProperty = TestCustomFieldIntValue;
            custom.CustomStringProperty = TestCustomFieldStringValue;

            client.EditField(issue, TestRegisteredCustomFieldName, custom);

            // Check the JSON generated for a field update to ensure it matches expected JSON for
            // a custom type field with no converter (this should dump custom type name).

            Assert.AreEqual(
               "{customfield_5678:[{set:{customstring:Hello, World!,customint:546}}]}",
                mock.PutRequest["update"], "Unexpected Json generated for editing a string custom field with a converter.");
        }
    }
}
