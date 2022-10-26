using Dotmim.Sync;
using Dotmim.Sync.Sqlite;
using Dotmim.Sync.Web.Client;

var serverOrchestrator = new WebRemoteOrchestrator("http://localhost:7845/api/sync");

// Second provider is using plain old Sql Server provider,
// relying on triggers and tracking tables to create the sync environment
string clientConnectionString = @"Data Source=C:\Users\Owner\AppData\Roaming\SyncTest_Local.db";
SqliteSyncProvider clientProvider = new SqliteSyncProvider(clientConnectionString);
var options = new SyncOptions
{
    BatchSize = 2000
};

serverOrchestrator.AddCustomHeader("Authorization", "Bearer " + "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxNCIsImVtYWlsIjoic2x0ZXN0QHlvcG1haWwuY29tIiwianRpIjoiYTFiYmJhZmQtZDhiNC00MDBhLTkwNDgtNTFkYTFhNzExMDRlIiwiZXhwIjoxNjY2NzY1NTMyLCJpc3MiOiJTbWFydExheWVyIiwiYXVkIjoiU21hcnRMYXllciJ9.alB0eKprmSoqwIPBTqJKBltf6UidYB9gXe2CsdeWAfo");

// Creating an agent that will handle all the process
var agent = new SyncAgent(clientProvider, serverOrchestrator, options);

var parameters = new SyncParameters
{
    { "TenantId", "3" }
};
var progress = new SynchronousProgress<ProgressArgs>(
   pa => Console.WriteLine($"{pa.ProgressPercentage:p}\t {pa.Message}"));

SyncContext context = new SyncContext();

    
do
{
    // Launch the sync process
    try
    {
        var s1 = await agent.SynchronizeAsync(progress);
        Console.WriteLine(s1);
    }
    catch(Exception ex)
    { 
    
    }
    // Write results
   

} while (Console.ReadKey().Key != ConsoleKey.Escape);

Console.WriteLine("End");
