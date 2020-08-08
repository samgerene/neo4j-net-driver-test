// ------------------------------------------------------------------------------------------------
// <copyright file="CountryReaderTestFixture.cs" company="Sam Gerené">
//   Copyright 2020 Sam Gerené
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
// -----------------------------------------------------------------------------------------------

namespace neo4j_net_driver_test.tests
{
    using System.Linq;

    using neo4j_net_driver_test.Data;

    using NUnit.Framework;

    public class CountryReaderTestFixture
    {
        private CountryReader countryReader;

        [SetUp]
        public void Setup()
        {
            this.countryReader = new CountryReader();
        }

        [Test]
        public void Verify_that_countries_can_be_read()
        {
            var countries = this.countryReader.Read();
            Assert.That(countries.Count(), Is.EqualTo(76));
        }
    }
}