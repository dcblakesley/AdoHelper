using System.Text.Json;
using System.Text.Json.Serialization;

namespace AdoUtilities.Dtos;

/// <summary> Don't use this, it's the model that goes to ADO. </summary>
internal class AdoWorkItem
{
    [JsonInclude] internal int Id { get; set; }
    [JsonInclude] internal int? Rev { get; set; }
    [JsonInclude] internal Fields? Fields { get; set; } = new();
    [JsonInclude] internal List<Relation>? Relations { get; set; }

    [JsonInclude, JsonPropertyName("_links")]
    internal Links? Links { get; set; }

    [JsonInclude] internal string? Url { get; set; }
    [JsonInclude] internal List<Comment>? Comments { get; set; }
    [JsonInclude] internal List<AdoWorkItem>? Children { get; set; }

    internal WorkItem ToWorkItem() => new()
    {
        Id = Id,
        Title = Fields?.Title ?? "",
        State = Fields?.State ?? "",
        Description = Fields?.Description ?? "",
        AcceptanceCriteria = Fields?.AcceptanceCriteria ?? "",
        Area = Fields?.AreaPath ?? "",
        Iteration = Fields?.IterationPath ?? "",
        Tags = Fields?.Tags ?? "",
        ParentId = Fields?.ParentId ?? 0,
        Type = Fields?.Type ?? ""
    };

    internal string ToJsonPatch(string serverUrl)
    {
        if (Fields == null)
            return "";
        if (Fields.State == "Done")
        {
            if (Fields.CompletedWork == 0)
                Fields.CompletedWork = Fields.OriginalEstimate;
        }

        // Only add patches for non-null fields.
        var patches = new List<JsonPatchDocument>();
        if (Fields.Title != null)
            patches.Add(new("replace", "/fields/System.Title", Fields.Title));
        if (Fields.State != null)
            patches.Add(new("replace", "/fields/System.State", Fields.State));
        if (Fields.Description != null)
            patches.Add(new("replace", "/fields/System.Description", Fields.Description));
        if (Fields.AcceptanceCriteria != null)
            patches.Add(new("replace", "/fields/Microsoft.VSTS.Common.AcceptanceCriteria", Fields.AcceptanceCriteria));
        if (Fields.AreaPath != null)
            patches.Add(new("replace", "/fields/System.AreaPath", Fields.AreaPath));
        if (Fields.IterationPath != null)
            patches.Add(new("replace", "/fields/System.IterationPath", Fields.IterationPath));
        if (Fields.Tags != null)
            patches.Add(new("replace", "/fields/System.Tags", Fields.Tags));
        if (Fields.CompletedWork != null)
            patches.Add(new("replace", "/fields/Microsoft.VSTS.Scheduling.CompletedWork",
                ((double)Fields.CompletedWork).ToString("N2")));
        if (Fields.OriginalEstimate != null)
            patches.Add(new("replace", "/fields/Microsoft.VSTS.Scheduling.OriginalEstimate",
                ((double)Fields.OriginalEstimate).ToString("N2")));
        if (Fields.Effort != null)
            patches.Add(new("replace", "/fields/Microsoft.VSTS.Scheduling.Effort",
                ((double)Fields.Effort).ToString("N2")));


        if (Fields.State != "Done")
        {
            // This was used by Tasks to insert remaining work during sprint planning
            //patches.Add(new("replace", "/fields/Microsoft.VSTS.Scheduling.RemainingWork", ((Fields.OriginalEstimate - Fields.CompletedWork) < 0 ? Fields.RemainingWork : (Fields.OriginalEstimate - Fields.CompletedWork)).ToString("N2")));
        }

        if (Fields?.AssignedTo != null)
        {
            //var assignedTo = Fields.AssignedTo.Id;
            // Strip out everything in a user except for their Id 
            patches.Add(new("add", "/fields/System.AssignedTo",
                new AdoUser("win.Uy0xLTUtMjEtMTYxNDg5NTc1NC0xMDc4MDgxNTMzLTgzOTUyMjExNS02MTY2Nw")));
        }

        if (Fields!.ParentId != 0)
        {
            var rel = new Relation("System.LinkTypes.Hierarchy-Reverse",
                $"{serverUrl}/_apis/wit/workItems/{Fields.ParentId}", null);
            patches.Add(new("add", "/relations/-", rel));
        }

        var serialized = JsonSerializer.Serialize(patches);
        return serialized;
    }

    public override string ToString() => Fields?.Title ?? "";
}