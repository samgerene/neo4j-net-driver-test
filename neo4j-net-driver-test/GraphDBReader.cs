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

namespace neo4j_net_driver_test
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using System.Threading.Tasks;

    using Neo4j.Driver;

    using Serilog;

    /// <summary>
    /// The purpose of the <see cref="GraphDBReader"/> is to read the objects from the graph
    /// </summary>
    public class GraphDBReader
    {
        /// <summary>
        /// Read the <see cref="Country"/> objects from the Neo4j database
        /// </summary>
        /// <param name="session">
        /// The <see cref="IAsyncSession"/>
        /// </param>
        /// <returns></returns>
        public async Task<List<IRecord>> Read(IAsyncSession session)
        {
            var cypher = new StringBuilder("MATCH (country:Country)");
            cypher.AppendLine("CALL { WITH country OPTIONAL MATCH (person:Person)-[:TRAVELS_TO]->(country) RETURN COLLECT(DISTINCT person.identifier) AS person_TRAVELS_TO_country }");
            cypher.AppendLine("CALL { WITH country OPTIONAL MATCH (travelAgency:TravelAgency)-[:FLYING_TO]->(country) RETURN COLLECT(DISTINCT travelAgency.identifier) AS travelAgency_FLYING_TO_country }");
            cypher.AppendLine("RETURN country {.*");
            cypher.AppendLine(", PERSON_TRAVELS_TO_COUNTRY: person_TRAVELS_TO_country");
            cypher.AppendLine(", TRAVEL_AGENCY_FLYING_TO_COUNTRY: travelAgency_FLYING_TO_country ");
            cypher.AppendLine("}");

            Log.Debug(cypher.ToString());

            var readTransaction = await session.BeginTransactionAsync();

            var sw = Stopwatch.StartNew();
            Log.Debug("Start Running Transaction");
            var cursor = await readTransaction.RunAsync(cypher.ToString());
            Log.Debug("Ran query and returned a task of result stream in {sw} [ms]", sw.ElapsedMilliseconds);

            sw.Restart();
            Log.Debug("Pull all records in the result stream into memory and return in a list.");
            var records = await cursor.ToListAsync();
            Log.Debug("All records pulled into memory and returned in list in {sw} [ms].", sw.ElapsedMilliseconds);

            return records;
        }
    }
}