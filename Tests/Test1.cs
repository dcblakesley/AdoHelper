using System.Text.Json;
using AdoUtilities;
using static Tests.Test1;

namespace Tests;

[TestClass]
public sealed class Test1
{

    /// <summary> Create a test settings file with secret values. Only needs to be run once manually. </summary>
    [TestMethod]
    public void CreateTestSettings()
    {
        // Put in real values, serialize, then remove values so they're not checked in.
        var apiAddress = "";
        var personalAccessToken = "";
        var testSettings = new TestSettings(apiAddress, personalAccessToken);
        var json = JsonSerializer.Serialize(testSettings);
        // write to file
        File.WriteAllText("testSettings.json", json);
    }

    /// <summary> Only works if you've created TestSettings using the CreateTestSettings method first. </summary>
    TestSettings GetTestSettings()
    {
        var json = File.ReadAllText("testSettings.json");
        return JsonSerializer.Deserialize<TestSettings>(json);
    }
    
    [TestMethod]
    public async Task CreatePbi()
    {
        var pbi = new WorkItem
        {
            Type = "Feature", // Product Backlog Item
            Title = "Test Feature",
            Tags = "Test",
            Description = "This is a test Feature",
            AcceptanceCriteria = "Test it",
            Area = "Webstaurantstore.com\\Procurement",
            //Iteration = "Webstaurantstore.com\\Procurement\\2024\\Q4\\2024 Q4 Sprint 3",
            State = "New",
            AssignedToDescriptor = "win.Uy0xLTUtMjEtMTYxNDg5NTc1NC0xMDc4MDgxNTMzLTgzOTUyMjExNS02MTY2Nw",
            ParentId = 994369 // "Hatch - Accessibility" // 1201030
        };

        var helper = new AdoHelper();
        var testSettings = GetTestSettings();
        var result = await helper.CreateWorkItem(pbi, new(), testSettings.ApiAddress, testSettings.PersonalAccessToken);
    }

    [TestMethod]
    public async Task GetWorkItem()
    {
        // var id = 1184645;
        var id = 942003;
        var testSettings = GetTestSettings();
        var helper = new AdoHelper();
        var result = await helper.GetWorkItem(id, new(), testSettings.ApiAddress, testSettings.PersonalAccessToken);
        var j = result;
    }

    public record TestSettings(string ApiAddress, string PersonalAccessToken);
}
