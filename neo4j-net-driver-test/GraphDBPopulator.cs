// ------------------------------------------------------------------------------------------------
// <copyright file="GraphDBPopulator.cs" company="Sam Gerené">
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

namespace neo4j_net_driver_test
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Neo4j.Driver;

    using neo4j_net_driver_test.Model;

    using Serilog;

    /// <summary>
    /// The purpose of the <see cref="GraphDBPopulator"/> class is to generate Cypher queries to populate a
    /// Neo4j database with representative data to reproduce the seemingly faulty behavior of the
    /// Neo4j.Driver
    /// </summary>
    public class GraphDBPopulator
    {
        /// <summary>
        /// Populate the Neo4j database with a test data set that is representative
        /// </summary>
        /// <returns></returns>
        public async Task Populate(IAsyncSession session, Graph graph)
        {
            

            foreach (var country in graph.Countries)
            {
                var countryTransaction = await session.BeginTransactionAsync();

                var properties = new Dictionary<string, object>();
                properties.Add("countryCode", country.CountryCode);
                properties.Add("identifier", country.Identifier.ToString());
                properties.Add("membershipKind", country.MembershipKind.ToString());
                properties.Add("name", country.Name);
                properties.Add("type", "Country");
                var propertiesObject = new { properties };

                var createCypher = "CREATE (n:Country:Thing $properties)";

                await countryTransaction.RunAsync(createCypher, propertiesObject);
                await countryTransaction.CommitAsync();

                Log.Debug("CREATED Country: {Country}", country);
            }

            foreach (var person in graph.Persons)
            {
                var personTransaction = await session.BeginTransactionAsync();

                var properties = new Dictionary<string, object>();
                properties.Add("identifier", person.Identifier.ToString());
                properties.Add("name", person.Name);
                properties.Add("type", "Person");
                var propertiesObject = new { properties };

                var createCypher = "CREATE (n:Person:Thing $properties)";

                await personTransaction.RunAsync(createCypher, propertiesObject);

                foreach (var destination in person.Destinations)
                {
                    var cypher = $"MATCH (person:Person), (country:Country) WHERE person.identifier='{person.Identifier}' AND country.identifier = '{destination.Identifier}' MERGE (person)-[:TRAVELS_TO]->(country);";
                    await personTransaction.RunAsync(cypher);
                }

                await personTransaction.CommitAsync();

                Log.Debug("CREATED Country: {Person}", person);
            }

            foreach (var travelAgency in graph.TravelAgencies)
            {
                var travelAgencyTransaction = await session.BeginTransactionAsync();

                var properties = new Dictionary<string, object>();
                properties.Add("identifier", travelAgency.Identifier.ToString());
                properties.Add("name", travelAgency.Name);
                properties.Add("type", "TravelAgency");
                var propertiesObject = new { properties };

                var createCypher = "CREATE (n:TravelAgency:Thing $properties)";

                await travelAgencyTransaction.RunAsync(createCypher, propertiesObject);

                foreach (var destination in travelAgency.Destinations)
                {
                    var cypher = $"MATCH (travelAgency:TravelAgency), (country:Country) WHERE travelAgency.identifier='{travelAgency.Identifier}' AND country.identifier = '{destination.Identifier}' MERGE (travelAgency)-[:FLYING_TO]->(country);";
                    await travelAgencyTransaction.RunAsync(cypher);
                }

                await travelAgencyTransaction.CommitAsync();

                Log.Debug("CREATED Country: {TravelAgency}", travelAgency);
            }
        }
    }
}