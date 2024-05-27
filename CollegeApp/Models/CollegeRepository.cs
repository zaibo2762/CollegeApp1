namespace CollegeApp.Models
{
    public static class CollegeRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>() {
         new Student
                {
                    Id = 1,
                    StudentName = "khan",
                    Email = "Student1@gmail.com",
                    Address = "Pakistan",
                },
                new Student
                {
                    Id = 2,
                    StudentName = "ahmed",
                    Email = "Student2@gmail.com",
                    Address = "India",
                },
};


    }
}
