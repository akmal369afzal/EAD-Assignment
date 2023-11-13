using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");
        using (var context = new MyContext())
        {
            // Insert operation using SQL
            string insertSql = "INSERT INTO Students (FirstMidName, LastName, EnrollmentDate, Email) " +
                              "VALUES ('John', 'Doe', GETDATE(), 'john@example.com');" +
                              "INSERT INTO Students (FirstMidName, LastName, EnrollmentDate, Email) " +
                              "VALUES ('Jane', 'Smith', GETDATE(), 'jane@example.com');";
            context.Database.ExecuteSqlRaw(insertSql);

            // Retrieving all students
            var students = context.Students.OrderBy(s => s.FirstMidName).ToList();

            Console.WriteLine("Retrieve all Students from the database:");
            foreach (var student in students)
            {
                string name = $"{student.FirstMidName} {student.LastName}";
                Console.WriteLine($"ID: {student.ID}, Name: {name}, Email: {student.Email}");
            }

            // Update operation using SQL
            string updateSql = "UPDATE Students SET Email = 'updatedjohn@example.com' WHERE FirstMidName = 'John';";
            context.Database.ExecuteSqlRaw(updateSql);

            // Delete operation using SQL
            string deleteSql = "DELETE FROM Students WHERE FirstMidName = 'John';";
            context.Database.ExecuteSqlRaw(deleteSql);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
public enum Grade
{
    A, B, C, D, F
}

public class Enrollment
{
    public int EnrollmentID { get; set; }
    public int CourseID { get; set; }
    public int StudentID { get; set; }
    public Grade? Grade { get; set; }

    public virtual Course Course { get; set; }
    public virtual Student Student { get; set; }
}

public class Student
{
    public int ID { get; set; }
    public string LastName { get; set; }
    public string FirstMidName { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public string Email { get; set; } // Additional field

    public virtual ICollection<Enrollment> Enrollments { get; set; }
}

public class Course
{
    public int CourseID { get; set; }
    public string Title { get; set; }
    public int Credits { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; }
}

public class MyContext : DbContext
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=localDB;User Id=AKMAL;Password=admin123;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().Property(s => s.Email).HasMaxLength(100); // Set email field maximum length
    }

}

















