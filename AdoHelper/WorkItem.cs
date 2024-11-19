using System.ComponentModel;
using AdoUtilities.Dtos;

namespace AdoUtilities;

public class WorkItem
{
    // Internal Notes: This is a more friendly version of Fields
    public int Id { get; set; }
    public string? Type { get; set; } 
    public string? Title { get; set; }
    public string? Tags { get; set; }
    public string? Description { get; set; }
    public string? AcceptanceCriteria { get; set; }
    public string? Area { get; set; }
    public string? Iteration { get; set; }

    /// <summary>
    /// PBI Options: New, Removed, Approved, Committed, Done
    /// Task Options: To Do, In Progress, Done
    /// Feature: New, In Progress, Removed, Done
    /// </summary>
    public string? State { get; set; }

    /// <summary> The Descriptor of the User to assign the PBI to, looks like "win.Uy0xL... 67 characters" </summary>
    public string? AssignedToDescriptor { get; set; }

    public int ParentId { get; set; }
    
    internal AdoWorkItem ToAdoWorkItem()
    {
        var wi = new AdoWorkItem();
        wi.Id = wi.Id;
        wi.Fields = new()
        {
            Title = Title,
            State = State,
            Description = Description,
            AcceptanceCriteria = AcceptanceCriteria,
            AreaPath = Area,
            IterationPath = Iteration,
            Tags = Tags,
            ParentId = ParentId,
            Type = Type
        };
        return wi;
    }
}
