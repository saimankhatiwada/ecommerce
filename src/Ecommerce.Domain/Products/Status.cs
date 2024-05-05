namespace Ecommerce.Domain.Products;

public sealed record Status
{
    internal static readonly Status None = new(string.Empty);

    public static readonly Status Active = new("Active");

    public static readonly Status Draft = new("Draft");

    public static readonly Status Archived = new("Archived");

    private Status(string value) => Value = value;

    public string Value { get; init; }

    public static Status FromStatus(string value) => All.FirstOrDefault(c => c.Value == value) ??
        throw new ApplicationException("The status value is invalid");

    public static Status CheckStatus(string value) => All.FirstOrDefault(c => c.Value == value) ??
        None;
        
    public static readonly IReadOnlyCollection<Status> All = new[]
    {
        Active,
        Draft,
        Archived
    };
}
