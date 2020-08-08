// ------------------------------------------------------------------------------------------------
// <copyright file="HttpGrapDbReaderTestFixture.cs" company="Sam Gerené">
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
    using System.IO;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using Serilog;

    [TestFixture]
    public class HttpGrapDbReaderTestFixture
    {
        private HttpGrapDbReader httpGrapDbReader;

        [SetUp]
        public void Setup()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("http-graph-reader-log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            this.httpGrapDbReader = new HttpGrapDbReader();
        }

        [Test]
        public async Task Verify_results_are_returned()
        {
            string result = null;
            
            Assert.DoesNotThrowAsync(async () =>
            {
                var stream = await this.httpGrapDbReader.Read();

                var streamReader = new StreamReader(stream);
                result = streamReader.ReadToEnd();
            });

            Log.Debug(result);

            Assert.IsTrue(result.Contains("identifier"));
        }
    }
}