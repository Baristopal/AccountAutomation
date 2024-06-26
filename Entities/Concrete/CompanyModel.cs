﻿using Dapper.Contrib.Extensions;
using Entities.Abstract;
using System.Text.Json.Serialization;

namespace Entities.Concrete;
[Table("Company")]
public class CompanyModel : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    [JsonIgnore]
    public string Password { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
}
