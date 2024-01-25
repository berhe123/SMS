//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SMS.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public Student()
        {
            this.ExamResults = new HashSet<ExamResult>();
            this.ExerciseBookResults = new HashSet<ExerciseBookResult>();
            this.GroupMembers = new HashSet<GroupMember>();
            this.IndividualWorkResults = new HashSet<IndividualWorkResult>();
            this.Parents = new HashSet<Parent>();
            this.Payments = new HashSet<Payment>();
            this.QuizResults = new HashSet<QuizResult>();
            this.StudentAttendances = new HashSet<StudentAttendance>();
            this.StudentClassRooms = new HashSet<StudentClassRoom>();
            this.StudentComments = new HashSet<StudentComment>();
            this.TestResults = new HashSet<TestResult>();
            this.TotalGroupWorkResults = new HashSet<TotalGroupWorkResult>();
            this.TotalIndividualWorkResults = new HashSet<TotalIndividualWorkResult>();
            this.TotalQuizResults = new HashSet<TotalQuizResult>();
            this.TotalSums = new HashSet<TotalSum>();
            this.TotalTestResults = new HashSet<TotalTestResult>();
        }
    
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public System.DateTime BirthDate { get; set; }
        public System.Guid GenderGuid { get; set; }
        public System.DateTime RegistrationDate { get; set; }
        public string Password { get; set; }
        public string PasswordVerification { get; set; }
        public System.Guid LogInUserId { get; set; }
        public Nullable<System.Guid> UserId { get; set; }
        public Nullable<int> EventSource { get; set; }
        public string ComputerName { get; set; }
        public System.Guid TableRowGuid { get; set; }
    
        public virtual ICollection<ExamResult> ExamResults { get; set; }
        public virtual ICollection<ExerciseBookResult> ExerciseBookResults { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
        public virtual ICollection<IndividualWorkResult> IndividualWorkResults { get; set; }
        public virtual ICollection<Parent> Parents { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<QuizResult> QuizResults { get; set; }
        public virtual ICollection<StudentAttendance> StudentAttendances { get; set; }
        public virtual ICollection<StudentClassRoom> StudentClassRooms { get; set; }
        public virtual ICollection<StudentComment> StudentComments { get; set; }
        public virtual ICollection<TestResult> TestResults { get; set; }
        public virtual ICollection<TotalGroupWorkResult> TotalGroupWorkResults { get; set; }
        public virtual ICollection<TotalIndividualWorkResult> TotalIndividualWorkResults { get; set; }
        public virtual ICollection<TotalQuizResult> TotalQuizResults { get; set; }
        public virtual ICollection<TotalSum> TotalSums { get; set; }
        public virtual ICollection<TotalTestResult> TotalTestResults { get; set; }
    }
}