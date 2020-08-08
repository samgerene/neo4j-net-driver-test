// ------------------------------------------------------------------------------------------------
// <copyright file="GraphGenerator.cs" company="Sam Gerené">
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

namespace neo4j_net_driver_test.Model
{
    using System;
    using System.Collections.Generic;

    using neo4j_net_driver_test.Data;
    using neo4j_net_driver_test.Extensions;
    
    /// <summary>
    /// The purpose of the <see cref="GraphGenerator"/> is to generate a representative graph
    /// with instance of <see cref="Country"/>, <see cref="Person"/> and <see cref="TravelAgency"/> classes.
    /// </summary>
    public class GraphGenerator
    {
        private readonly CountryReader countryReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphGenerator"/> class.
        /// </summary>
        public GraphGenerator()
        {
            this.countryReader = new CountryReader();
        }

        /// <summary>
        /// Generates a new instance of <see cref="Graph"/>
        /// </summary>
        /// <param name="nrPersons">
        /// the number of <see cref="Person"/> objects to create
        /// </param>
        /// <param name="nrTravelAgencies">
        /// the number of <see cref="TravelAgency"/> objects to create
        /// </param>
        /// <returns>
        /// an instance of <see cref="Graph"/>
        /// </returns>
        public Graph Generate(int nrPersons, int nrTravelAgencies)
        {
            var countries = this.countryReader.Read();
            var persons = this.CreatePersons(nrPersons, countries);
            var travelAgencies = this.createTravelAgencies(nrTravelAgencies, countries);

            var graph = new Graph(countries, persons, travelAgencies);
            return graph;
        }

        /// <summary>
        /// Creates <see cref="Person"/> objects
        /// </summary>
        /// <param name="countries">
        /// An <see cref="IEnumerable{Country}"/> of which random destinations are selected
        /// for each <see cref="Person"/> that is created
        /// </param>
        /// <returns>
        /// The generated <see cref="Person"/> instances
        /// </returns>
        private IEnumerable<Person> CreatePersons(int nrPersons, IEnumerable<Country> countries)
        {
            for (int i = 1; i < nrPersons + 1; i++)
            {
                var person = new Person { Identifier = Guid.NewGuid(), Name = $"person {i}" };
                person.Destinations.AddRange(countries.PickRandom(10));
                yield return person;
            }
        }

        /// <summary>
        /// Creates <see cref="TravelAgency"/> objects
        /// </summary>
        /// <param name="countries">
        /// An <see cref="IEnumerable{Country}"/> of which random destinations are selected
        /// for each <see cref="TravelAgency"/> that is created
        /// </param>
        /// <returns>
        /// The generated <see cref="TravelAgency"/> instances
        /// </returns>
        private IEnumerable<TravelAgency> createTravelAgencies(int nrTravelAgencies, IEnumerable<Country> countries)
        {
            for (int i = 1; i < nrTravelAgencies + 1; i++)
            {
                var travelAgency = new TravelAgency { Identifier = Guid.NewGuid(), Name = $"travel agency {i}" };
                travelAgency.Destinations.AddRange(countries.PickRandom(10));
                yield return travelAgency;
            }
        }
    }
}