﻿using AirBnB.Domain.Enums;

namespace AirBnB.Domain.Entities;

public class Role
{
    public RoleType Type { get; set; }

    public bool IsDisable { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime ModifiedTime { get; set; }
}
