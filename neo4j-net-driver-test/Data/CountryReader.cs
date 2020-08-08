// ------------------------------------------------------------------------------------------------
// <copyright file="CountryReader.cs" company="Sam Gerené">
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

namespace neo4j_net_driver_test.Data
{
    using System.Collections.Generic;
    using System.IO;

    using Newtonsoft.Json;

    using neo4j_net_driver_test.Model;

    /// <summary>
    /// The purpose of the <see cref="CountryReader"/> is to read the <see cref="Country"/>
    /// objects from the JSON file
    /// </summary>
    public class CountryReader
    {
        /// <summary>
        /// Read the <see cref="Country"/> objects
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{Country}"/>
        /// </returns>
        public IEnumerable<Country> Read()
        {
            using (var file = File.OpenText(Path.Combine("Data", "countries.json")))
            {
                var serializer = new JsonSerializer();
                var countries = (IEnumerable<Country>)serializer.Deserialize(file, typeof(IEnumerable<Country>));
                return countries;
            }
        }
    }
}