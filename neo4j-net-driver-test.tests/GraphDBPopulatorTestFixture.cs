// ------------------------------------------------------------------------------------------------
// <copyright file="GraphDBPopulatorTestFixture.cs" company="Sam Gerené">
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
    using System;
    using System.Threading.Tasks;

    using Neo4j.Driver;

    using neo4j_net_driver_test.Model;

    using NUnit.Framework;

    using Serilog;

    /// <summary>
    /// Suite of tests for the <see cref="GraphDBPopulator"/> class
    /// </summary>
    [TestFixture]
    public class GraphDBPopulatorTestFixture
    {
        private IDriver driver;

        private GraphGenerator graphGenerator;

        private GraphDBPopulator graphDbPopulator;

        [SetUp]
        public void SetUp()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("graph-populator-log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            this.graphGenerator = new GraphGenerator();
            this.graphDbPopulator = new GraphDBPopulator();

            this.driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "marvin"));
        }

        [Test]
        public async Task Verify_that_graph_can_be_stored_in_neo4j()
        {
            var session = driver.AsyncSession();

            var deleteDataTransaction = await session.BeginTransactionAsync();
            await deleteDataTransaction.RunAsync("MATCH (n) DETACH DELETE n");
            await deleteDataTransaction.CommitAsync();

            try
            {
                var dropConstraintsTransaction = await session.BeginTransactionAsync();
                await dropConstraintsTransaction.RunAsync("DROP CONSTRAINT ON (t:Thing) ASSERT t.identifier IS UNIQUE");
                await dropConstraintsTransaction.CommitAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            
            var createConstraintsTransaction = await session.BeginTransactionAsync();
            await createConstraintsTransaction.RunAsync("CREATE CONSTRAINT ON (t:Thing) ASSERT t.identifier IS UNIQUE");
            await createConstraintsTransaction.CommitAsync();

            var graph = this.graphGenerator.Generate(10000, 2000);

            await this.graphDbPopulator.Populate(session, graph);
        }
    }
}