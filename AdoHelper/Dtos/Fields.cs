using System.Text.Json.Serialization;

namespace AdoUtilities.Dtos;

internal class Fields
{
    [JsonInclude, JsonPropertyName("Microsoft.VSTS.Common.ValueArea")]
    internal string? ValueArea { get; set; }

    [JsonInclude, JsonPropertyName("Microsoft.VSTS.Common.BacklogPriority")]
    internal double? BacklogPriority { get; set; }

    [JsonInclude, JsonPropertyName("Microsoft.VSTS.Scheduling.OriginalEstimate")]
    internal double? OriginalEstimate { get; set; }

    [JsonInclude, JsonPropertyName("Microsoft.VSTS.Scheduling.RemainingWork")]
    internal double? RemainingWork { get; set; }

    [JsonInclude, JsonPropertyName("Microsoft.VSTS.Scheduling.CompletedWork")]
    internal double? CompletedWork { get; set; }

    [JsonInclude, JsonPropertyName("System.AssignedTo")]
    internal AdoUser? AssignedTo { get; set; }

    [JsonInclude, JsonPropertyName("System.Id")]
    internal int? Id { get; set; }

    [JsonInclude, JsonPropertyName("System.AreaId")]
    internal int? AreaId { get; set; }

    [JsonInclude, JsonPropertyName("System.AreaPath")]
    internal string? AreaPath { get; set; }

    [JsonInclude, JsonPropertyName("System.IterationPath")]
    internal string? IterationPath { get; set; }

    [JsonInclude, JsonPropertyName("System.TeamProject")]
    internal string? TeamProject { get; set; }

    [JsonInclude, JsonPropertyName("System.RevisedDate")]
    internal DateTime? RevisedDate { get; set; }

    [JsonInclude, JsonPropertyName("System.IterationId")]
    internal int? IterationId { get; set; }

    [JsonInclude, JsonPropertyName("System.WorkItemType")]
    internal string? Type { get; set; } = "Product Backlog Item";

    [JsonInclude, JsonPropertyName("System.State")]
    internal string? State { get; set; } = "New";

    [JsonInclude, JsonPropertyName("System.Reason")]
    internal string? Reason { get; set; }

    [JsonInclude, JsonPropertyName("System.CreatedDate")]
    internal DateTime? CreatedDate { get; set; }

    [JsonInclude, JsonPropertyName("System.CreatedBy")]
    internal AdoUser? CreatedBy { get; set; }

    [JsonInclude, JsonPropertyName("System.ChangedDate")]
    internal DateTime? ChangedDate { get; set; }

    [JsonInclude, JsonPropertyName("System.ChangedBy")]
    internal AdoUser? ChangedBy { get; set; }

    [JsonInclude, JsonPropertyName("System.Title")]
    internal string? Title { get; set; } = "";

    [JsonInclude, JsonPropertyName("Microsoft.VSTS.Common.Priority")]
    internal int? Priority { get; set; }

    [JsonInclude, JsonPropertyName("Microsoft.VSTS.Scheduling.Effort")]
    internal double? Effort { get; set; }

    [JsonInclude, JsonPropertyName("System.Description")]
    internal string? Description { get; set; } = "";

    [JsonInclude, JsonPropertyName("Microsoft.VSTS.Common.AcceptanceCriteria")]
    internal string? AcceptanceCriteria { get; set; } = "";

    [JsonInclude, JsonPropertyName("System.Tags")]
    internal string? Tags { get; set; } = "";

    [JsonInclude, JsonPropertyName("System.Parent")]
    internal int? ParentId { get; set; }

    [JsonInclude, JsonPropertyName("href")]
    internal string? Href { get; set; }

    [JsonInclude, JsonPropertyName("System.PersonId")]
    internal int? PersonId { get; set; }
}