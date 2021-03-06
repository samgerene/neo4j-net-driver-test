﻿// ------------------------------------------------------------------------------------------------
// <copyright file="Person.cs" company="Sam Gerené">
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

    /// <summary>
    /// Represents a Person that likes to travel to different <see cref="Country"/>
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        public Person()
        {
            this.Destinations = new List<Country>();
        }

        /// <summary>
        /// Gets or sets the unique identifier 
        /// </summary>
        public Guid Identifier { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Country"/> objects that are the destinations of this <see cref="Person"/>
        /// </summary>
        public List<Country> Destinations { get; set; }
    }
}