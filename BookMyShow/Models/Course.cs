using System;
using System.Collections.Generic;

namespace BookMyShow.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string? CourseName { get; set; }

    public string? Duration { get; set; }

    public decimal? Fee { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
