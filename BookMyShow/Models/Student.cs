using System;
using System.Collections.Generic;

namespace BookMyShow.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
