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
    using System.Collections.Generic;

    /// <summary>
    /// Container object to hold the data in the <see cref="Graph"/>
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Graph"/> class.
        /// </summary>
        /// <param name="countries">
        /// The countries that are part of the <see cref="Graph"/>
        /// </param>
        /// <param name="persons">
        /// The persons that are part of the <see cref="Graph"/>
        /// </param>
        /// <param name="travelAgencies">
        /// The travelAgencies that are part of the <see cref="Graph"/>
        /// </param>
        public Graph(IEnumerable<Country> countries, IEnumerable<Person> persons, IEnumerable<TravelAgency> travelAgencies)
        {
            this.Countries = countries;
            this.Persons = persons;
            this.TravelAgencies = travelAgencies;
        }

        /// <summary>
        /// Gets the <see cref="IEnumerable{Country}"/>
        /// </summary>
        public IEnumerable<Country> Countries { get; private set; }

        /// <summary>
        /// Gets the <see cref="IEnumerable{Person}"/>
        /// </summary>
        public IEnumerable<Person> Persons { get; private set; }

        /// <summary>
        /// Gets the <see cref="IEnumerable{TravelAgency}"/>
        /// </summary>
        public IEnumerable<TravelAgency> TravelAgencies { get; private set; }
    }
}
