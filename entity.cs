using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

[Table(Name = "student")]
public class Student {

	[Column(IsPrimaryKey = true, CanBeNull = false)]
	public int id { get; set; }

	[Column(CanBeNull = false)]
	public string studentname { get; set; }

	[Column(CanBeNull = false)]
	public int studentage { get; set; }


}

[Table(Name = "grade")]
public class Grade {

	[Column(IsPrimaryKey = true, CanBeNull = false)]
	public int studentid { get; set; }

	[Column(IsPrimaryKey = true, CanBeNull = false)]
	public string coursecode { get; set; }

	[Column(CanBeNull = false)]
	public char grade { get; set; }


}

[Table(Name = "course")]
public class Course
{
    [Column(IsPrimaryKey = true, CanBeNull = false)]
    public string coursecode { get; set; }

    [Column(CanBeNull = false)]
    public string coursename { get; set; }

    [Column(CanBeNull = false)]
    public string semester { get; set; }

    [Column(CanBeNull = false)]
    public string teacher { get; set; }
}
