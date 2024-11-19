using AdoUtilities.Dtos;
using System.Text;
using System.Text.Json;

namespace AdoUtilities;

/// <summary> Helper class for interacting with Azure DevOps </summary>
public class AdoHelper
{
    JsonSerializerOptions so = new() { PropertyNameCaseInsensitive = true };
    
    public async Task<WorkItem?> CreateWorkItem(WorkItem workItem, HttpClient httpClient, string apiAddress, string personalAccessToken)
    {
        ConfigureClient(httpClient, personalAccessToken);

        // Convert the PBI to a WorkItem
        var adoWorkItem = workItem.ToAdoWorkItem();

        // Add AssignedTo if it exists
        if (!string.IsNullOrEmpty(workItem.AssignedToDescriptor))
            adoWorkItem.Fields.AssignedTo = new(workItem.AssignedToDescriptor);
        //workItem.Fields.AssignedTo = new(pbi.AssignedToDescriptor, null, null, null, null, null, null);

        // Create the WorkItem
        var createdWorkItem = await CreateWorkItem(adoWorkItem, httpClient, apiAddress);
        if (createdWorkItem == null)
            return null;

        // Convert the WorkItem back to a PBI
        return adoWorkItem.ToWorkItem();
    }

    void ConfigureClient(HttpClient httpClient, string personalAccessToken)
    {
        httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));
        httpClient.DefaultRequestHeaders.Authorization = 
            new("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($":{personalAccessToken}")));
    }

    // Get Single
    public async Task<WorkItem?> GetWorkItem(int id, HttpClient httpClient, string apiAddress, string personalAccessToken)
    {
        ConfigureClient(httpClient, personalAccessToken);
        using var response = await httpClient.GetAsync($"{apiAddress}/_apis/wit/workItems/{id}?$expand=all");
        var responseBody = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
     
        var wi = JsonSerializer.Deserialize<AdoWorkItem>(responseBody, so);
        var workItem = wi.ToWorkItem();
        return workItem ?? null;
    }

    /// <summary>
    /// Everything in ADO is a WorkItem. The rules are dynamic based on the WorkItemType and the settings for an organization.
    /// This is the only method that goes directly to ADO, the other Create/Update methods are targeted to a specific type with their appropriate rules.
    /// </summary>
    async Task<AdoWorkItem?> CreateWorkItem(AdoWorkItem workItem, HttpClient httpClient, string apiAddress)
    {
        var uri = $"{apiAddress}/_apis/wit/workItems/${workItem.Fields.Type}?api-version=7.0";
       
        var patch = workItem.ToJsonPatch(apiAddress);
       
        var content = new StringContent(patch, Encoding.UTF8, "application/json-patch+json");
      
        using var response = await httpClient.PostAsync(uri, content);

        var responseBody = await response.Content.ReadAsStringAsync();
        response.EnsureSuccessStatusCode();
        return JsonSerializer.Deserialize<AdoWorkItem>(responseBody, so)!;
    }
}
