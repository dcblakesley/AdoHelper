using System.Text.Json.Serialization;

namespace AdoUtilities.Dtos;

// DTOs
internal record Links([property: JsonInclude] Link? Self, [property: JsonInclude] Link? WorkItemUpdates, [property: JsonInclude] Link? WorkItemRevisions, [property: JsonInclude] Link? WorkItemComments, [property: JsonInclude] Link? Html, [property: JsonInclude] Link? WorkItemType, [property: JsonInclude] Fields Fields);
internal record Attributes([property: JsonInclude] bool IsLocked, [property: JsonInclude] string? Name);
internal record Relation([property: JsonInclude] string? Rel, [property: JsonInclude] string? Url, [property: JsonInclude] Attributes Attributes);
internal record Link([property: JsonInclude] string? Href);
internal record AdoUser([property: JsonInclude] string? Descriptor);
internal record Comment([property: JsonInclude] AdoUser CreatedBy, [property: JsonInclude] DateTime ModifiedDate, [property: JsonInclude] string Text);
internal record JsonPatchDocument([property: JsonInclude] string op, [property: JsonInclude] string path, [property: JsonInclude] object value);
