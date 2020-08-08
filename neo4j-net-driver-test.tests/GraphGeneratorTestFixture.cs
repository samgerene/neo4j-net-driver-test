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

    using neo4j_net_driver_test.Model;

    using NUnit.Framework;

    [TestFixture]
    public class GraphGeneratorTestFixture
    {
        private GraphGenerator graphGenerator;

        [SetUp]
        public void SetUp()
        {
            this.graphGenerator = new GraphGenerator();
        }

        [Test]
        public void Verify_that_a_graph_can_be_created()
        {
            var nrPersons = 12000;
            var nrTravelAgencies = 2000;

            var graph = this.graphGenerator.Generate(nrPersons, nrTravelAgencies);

            Assert.That(graph.Countries.Count(), Is.EqualTo(76));
            Assert.That(graph.Persons.Count(), Is.EqualTo(nrPersons));
            Assert.That(graph.TravelAgencies.Count(), Is.EqualTo(2000));
        }
    }
}