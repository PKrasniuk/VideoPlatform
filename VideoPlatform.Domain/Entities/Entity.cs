namespace VideoPlatform.Domain.Entities;

public abstract class Entity<T> : BaseEntity<T>
{
    public byte[] RowVersion { get; set; }
}