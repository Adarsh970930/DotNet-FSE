using System;

namespace DesignPatterns
{
    // Model
    public class Student
    {
        public string RollNo { get; set; } = "";
        public string Name { get; set; } = "";
    }

    // View
    public class StudentView
    {
        public void PrintStudentDetails(string studentName, string studentRollNo)
        {
            Console.WriteLine("Student Profile View:");
            Console.WriteLine($"Name: {studentName}");
            Console.WriteLine($"Roll No: {studentRollNo}");
        }
    }

    // Controller
    public class StudentController
    {
        private readonly Student _model;
        private readonly StudentView _view;

        public StudentController(Student model, StudentView view)
        {
            _model = model;
            _view = view;
        }

        public void SetStudentName(string name) => _model.Name = name;
        public string GetStudentName() => _model.Name;

        public void SetStudentRollNo(string rollNo) => _model.RollNo = rollNo;
        public string GetStudentRollNo() => _model.RollNo;

        public void UpdateView() => _view.PrintStudentDetails(_model.Name, _model.RollNo);
    }

    public static class MvcDemo
    {
        public static void Run()
        {
            Console.WriteLine("--- MVC Pattern ---");
            Student model = new() { Name = "Keshav", RollNo = "2026-FSE" };
            StudentView view = new();
            StudentController controller = new(model, view);

            controller.UpdateView();

            Console.WriteLine("\nUpdating student name...");
            controller.SetStudentName("Keshav Kumar");
            controller.UpdateView();
        }
    }
}