Dear Reader,

I have created this project because I am having some trouble using Neo4j version 4.1.1 and the [Neo4j dot.net driver version 4.1.0](https://github.com/neo4j/neo4j-dotnet-driver). The following image shows the simple graph that i am using:

![Graph](/graph.svg?raw=true "Graph")

I am using the following query to retrieve all Country nodes. The Country nodes that are returned also get the unique identifiers (of type GUID) of the:
  - person nodes that have a TRAVELS_TO relationship to the Country node
  - travelAgency nodes that have a FLYING_TO relationship to the Country node

```
MATCH (country:Country)
   CALL { WITH country OPTIONAL MATCH (person:Person)-[:TRAVELS_TO]->(country) RETURN COLLECT(DISTINCT person.identifier) AS person_TRAVELS_TO_country }
   CALL { WITH country OPTIONAL MATCH (travelAgency:TravelAgency)-[:FLYING_TO]->(country) RETURN COLLECT(DISTINCT travelAgency.identifier) AS travelAgency_FLYING_TO_country }
   RETURN country {.*
    , PERSON_TRAVELS_TO_COUNTRY: person_TRAVELS_TO_country
    , TRAVEL_AGENCY_FLYING_TO_COUNTRY: travelAgency_FLYING_TO_country 
  }
```

I am using the following code to query the database

```
    var cypher = "...";
    var readTransaction = await session.BeginTransactionAsync();
    var cursor = await readTransaction.RunAsync(cypher.ToString());
    var records = await cursor.ToListAsync();
```

The last line of the above snippet never completes, and this is exatly the problem that i have. I have verified that my query returns the expected results using the Neo4j browser as well as using the unit test but with a data set that is significantly smaller, i.e. 100 persons and 100 travelAgencies with each 10 relationships to the Country nodes. So, the issue that i am having seems to be related to the size of the data, or the amount of incoming relationships per `Country` node.

I am using the following Neo4j docker setup:

```
docker run \
	--name neo4j-test \
	--detach \
    --publish=7474:7474 --publish=7687:7687 \
    --volume=$HOME/neo4j/data:/data --volume=$HOME/neo4j/logs:/logs \
	--env NEO4J_dbms_memory_pagecache_size=1G \
	--env NEO4J_dbms_memory_heap_max__size=1G \
	--env NEO4J_AUTH=neo4j/marvin \
    neo4j:4.1.1
```

To reproduce this issue do the following:

  - Populate the graph by running the `GraphDBPopulatorTestFixture.Verify_that_graph_can_be_stored_in_neo4j` test (this will take a couple of minutes)
  - Run the `GraphDBReaderTestFixture.Verify_that_country_records_can_beRead` test and see that it does not complete.
  - Inspect the log file in the bin folder named `graph-reader-log-date.txt` and see that the `[DBG] Pull all records in the result stream into memory and return in a list.` statement is the last while it should be the following statement `[DBG] All records pulled into memory and returned in list in xx [ms].`