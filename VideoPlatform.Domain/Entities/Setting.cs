namespace VideoPlatform.Domain.Entities;

public class Setting : Entity<short>
{
    public string Name { get; set; }

    public string Value { get; set; }
}