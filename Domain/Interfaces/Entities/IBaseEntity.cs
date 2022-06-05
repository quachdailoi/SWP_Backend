﻿namespace SECapstoneEvaluation.Domain.Interfaces.Entities
{
    public interface IBaseEntity
    {
        int Id { get; set; }

        DateTime CreatedAt { get; set; }
        string CreatedBy { get; set; } 
        DateTime? UpdatedAt { get; set; }
        string UpdatedBy { get; set; } 

        bool IsDeleted { get; set; }
    }
}
