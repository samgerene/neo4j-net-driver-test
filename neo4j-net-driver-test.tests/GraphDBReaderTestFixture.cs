// ------------------------------------------------------------------------------------------------
// <copyright file="GraphDBReader.cs" company="Sam Gerené">
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
    using System.Threading.Tasks;

    using Neo4j.Driver;

    using NUnit.Framework;

    using Serilog;

    /// <summary>
    /// Suite of tests for the <see cref="GraphDBReader"/> class
    /// </summary>
    [TestFixture]
    public class GraphDBReaderTestFixture
    {
        private IDriver driver;

        private GraphDBReader graphDbReader;

        [SetUp]
        public void SetUp()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("graph-reader-log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            this.graphDbReader = new GraphDBReader();

            this.driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "marvin"));
        }

        [Test]
        public async Task Verify_that_country_records_can_beRead_using_ToListAsync()
        {
            var session = driver.AsyncSession();

            var records = await this.graphDbReader.ReadToListAsync(session);

            Assert.That(records.Count, Is.EqualTo(76));
        }

        [Test]
        public async Task Verify_that_country_records_can_beRead()
        {
            var session = driver.AsyncSession();

            var records = await this.graphDbReader.Read(session);

            Assert.That(records.Count, Is.EqualTo(76));
        }
    }
}