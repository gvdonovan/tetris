using System;

namespace RamQuest.Core.Model
{
    public interface IIdentifiable<TId> where TId : IEquatable<TId> { TId Id { get; set; } }
    public interface IEntity<TId> : IIdentifiable<TId> where TId : IEquatable<TId> { }
    public class Entity<TId> : IEntity<TId> where TId : IEquatable<TId> { public virtual TId Id { get; set; } }
}
