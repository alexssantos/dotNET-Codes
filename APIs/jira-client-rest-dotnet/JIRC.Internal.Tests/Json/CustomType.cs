// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomType.cs" company="David Bevin">
//   Copyright (c) David Bevin.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace JIRC.Internal.Tests.Json
{
    /// <summary>
    /// Used to test custom field converters in unit tests.
    /// </summary>
    class CustomType
    {
        /// <summary>
        /// Gets or sets the custom string property.
        /// </summary>
        public string CustomStringProperty { get; set; }

        /// <summary>
        /// Gets or sets the custom int property.
        /// </summary>
        public int CustomIntProperty { get; set; }
    }
}
