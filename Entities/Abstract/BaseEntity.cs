﻿namespace Entities.Abstract;
public class BaseEntity
{
    public virtual string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public string CreatedName { get; set; }
    public string UpdatedName { get; set; }
    public int CompanyId { get; set; }
}
