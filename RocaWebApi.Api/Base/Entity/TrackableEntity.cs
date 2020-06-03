using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RocaWebApi.Api.Base.Entity
{
    public abstract class TrackableEntity
    {
        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public DateTimeOffset? DeletedAt { get; set; }

        // [NotMapped]
        // public bool IsDeleted => DeletedAt != null;
    }
}
