using System;
using System.Collections.Generic;

namespace BookMyShow.Models;

public partial class EmployeeFullDetail
{
    public int EmployeeId { get; set; }

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }
}
