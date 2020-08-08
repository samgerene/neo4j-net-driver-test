// ------------------------------------------------------------------------------------------------
// <copyright file="HttpGrapDbReader.cs" company="Sam Gerené">
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
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using Serilog;

    /// <summary>
    /// The purpose of the <see cref="GraphDBReader"/> is to read the objects from the graph using the HTTP API
    /// </summary>
    public class HttpGrapDbReader
    {
        private HttpClient httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpGrapDbReader"/> class.
        /// </summary>
        public HttpGrapDbReader()
        {
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = new Uri("http://localhost:7474");
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes("neo4j:marvin")));
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.httpClient.DefaultRequestHeaders.Add("X-Stream", "true");
        }

        /// <summary>
        /// Reads the data from the graph and returns A <see cref="Stream"/>
        /// </summary>
        /// <returns>
        /// A <see cref="Stream"/> that contains the results from the Cypher query
        /// </returns>
        public async Task<Stream> Read()
        {
            var payload = File.ReadAllText(Path.Combine("Data", "post-message.json"));
        
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            var sw = Stopwatch.StartNew();
            var response = await this.httpClient.PostAsync("db/neo4j/tx", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            var stream = await response.Content.ReadAsStreamAsync();

            Log.Debug("result returned in {sw} [ms]", sw.ElapsedMilliseconds);

            return stream;
        }
    }
}