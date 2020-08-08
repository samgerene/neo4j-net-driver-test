// ------------------------------------------------------------------------------------------------
// <copyright file="Country.cs" company="Sam Gerené">
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
    /// Represents a <see cref="Country"/>
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Country"/> class.
        /// </summary>
        public Country()
        {
            this.PERSON_TRAVELS_TO_COUNTRY = new List<Guid>();
            this.TRAVEL_AGENCY_FLYING_TO_COUNTRY = new List<Guid>();
        }

        /// <summary>
        /// Gets or sets the unique identifier 
        /// </summary>
        public Guid Identifier { get; set; }

        /// <summary>
        /// Gets or sets the ISO 2 character country code
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="MembershipKind"/>
        /// </summary>
        public MembershipKind MembershipKind { get; set; }

        /// <summary>
        /// Gets or sets a list of unique identifiers of <see cref="Person"/>s that
        /// travel to this <see cref="Country"/>
        /// </summary>
        public List<Guid> PERSON_TRAVELS_TO_COUNTRY { get; set; }

        /// <summary>
        /// Gets or sets a list of unique identifiers of <see cref="Person"/>s that
        /// travel to this <see cref="Country"/>
        /// </summary>
        public List<Guid> TRAVEL_AGENCY_FLYING_TO_COUNTRY { get; set; }
    }
}