﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SMSEntities : DbContext
    {
        public SMSEntities()
            : base("name=SMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AcademicCalander> AcademicCalanders { get; set; }
        public virtual DbSet<AppSetting> AppSettings { get; set; }
        public virtual DbSet<ClassRoom> ClassRooms { get; set; }
        public virtual DbSet<ClassRoomsForPaymentCalander> ClassRoomsForPaymentCalanders { get; set; }
        public virtual DbSet<Day> Days { get; set; }
        public virtual DbSet<ExamResult> ExamResults { get; set; }
        public virtual DbSet<ExamSchedule> ExamSchedules { get; set; }
        public virtual DbSet<ExerciseBookResult> ExerciseBookResults { get; set; }
        public virtual DbSet<FeeSetting> FeeSettings { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<GroupMember> GroupMembers { get; set; }
        public virtual DbSet<GroupName> GroupNames { get; set; }
        public virtual DbSet<GroupWorkResult> GroupWorkResults { get; set; }
        public virtual DbSet<GroupWorkSchedule> GroupWorkSchedules { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<IndividualWorkResult> IndividualWorkResults { get; set; }
        public virtual DbSet<IndividualWorkSchedule> IndividualWorkSchedules { get; set; }
        public virtual DbSet<LessonClass> LessonClasses { get; set; }
        public virtual DbSet<Letter> Letters { get; set; }
        public virtual DbSet<Month> Months { get; set; }
        public virtual DbSet<ParentComment> ParentComments { get; set; }
        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<PaymentCalander> PaymentCalanders { get; set; }
        public virtual DbSet<PaymentCalandersToClassRoom> PaymentCalandersToClassRooms { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Period> Periods { get; set; }
        public virtual DbSet<QuizResult> QuizResults { get; set; }
        public virtual DbSet<QuizSchedule> QuizSchedules { get; set; }
        public virtual DbSet<security_Membership> security_Membership { get; set; }
        public virtual DbSet<security_OAuthMembership> security_OAuthMembership { get; set; }
        public virtual DbSet<security_Roles> security_Roles { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<StudentAttendance> StudentAttendances { get; set; }
        public virtual DbSet<StudentClassRoom> StudentClassRooms { get; set; }
        public virtual DbSet<StudentComment> StudentComments { get; set; }
        public virtual DbSet<StudentProfile> StudentProfiles { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<SubjectTeacherClassRoom> SubjectTeacherClassRooms { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<TeacherComment> TeacherComments { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<TestResult> TestResults { get; set; }
        public virtual DbSet<TestSchedule> TestSchedules { get; set; }
        public virtual DbSet<TotalGroupWorkResult> TotalGroupWorkResults { get; set; }
        public virtual DbSet<TotalIndividualWorkResult> TotalIndividualWorkResults { get; set; }
        public virtual DbSet<TotalQuizResult> TotalQuizResults { get; set; }
        public virtual DbSet<TotalSum> TotalSums { get; set; }
        public virtual DbSet<TotalTestResult> TotalTestResults { get; set; }
        public virtual DbSet<UserProfile> UserProfiles { get; set; }
        public virtual DbSet<UsersInLocation> UsersInLocations { get; set; }
        public virtual DbSet<Worker> Workers { get; set; }
        public virtual DbSet<vw_ExamScheduleList> vw_ExamScheduleList { get; set; }
        public virtual DbSet<vw_FeeSettingList> vw_FeeSettingList { get; set; }
        public virtual DbSet<vw_GroupNameList> vw_GroupNameList { get; set; }
        public virtual DbSet<vw_GroupWorkResultList> vw_GroupWorkResultList { get; set; }
        public virtual DbSet<vw_GroupWorkScheduleList> vw_GroupWorkScheduleList { get; set; }
        public virtual DbSet<vw_IndividualWorkScheduleList> vw_IndividualWorkScheduleList { get; set; }
        public virtual DbSet<vw_LessonClassList> vw_LessonClassList { get; set; }
        public virtual DbSet<vw_LessonClassListForTeacher> vw_LessonClassListForTeacher { get; set; }
        public virtual DbSet<vw_PaymentCalanderList> vw_PaymentCalanderList { get; set; }
        public virtual DbSet<vw_PaymentCalanderToClassRoomList> vw_PaymentCalanderToClassRoomList { get; set; }
        public virtual DbSet<vw_PaymentList> vw_PaymentList { get; set; }
        public virtual DbSet<vw_PeriodList> vw_PeriodList { get; set; }
        public virtual DbSet<vw_QuizScheduleList> vw_QuizScheduleList { get; set; }
        public virtual DbSet<vw_StudentAttendanceList> vw_StudentAttendanceList { get; set; }
        public virtual DbSet<vw_StudentClassRoomList> vw_StudentClassRoomList { get; set; }
        public virtual DbSet<vw_StudentInfromationDataView> vw_StudentInfromationDataView { get; set; }
        public virtual DbSet<vw_StudentList> vw_StudentList { get; set; }
        public virtual DbSet<vw_StudentListByClassRoomId> vw_StudentListByClassRoomId { get; set; }
        public virtual DbSet<vw_StudentPaymentDataView> vw_StudentPaymentDataView { get; set; }
        public virtual DbSet<vw_StudentsForExamResultDataEntry> vw_StudentsForExamResultDataEntry { get; set; }
        public virtual DbSet<vw_StudentsForExamResultDataView> vw_StudentsForExamResultDataView { get; set; }
        public virtual DbSet<vw_StudentsForExamScheduleDataView> vw_StudentsForExamScheduleDataView { get; set; }
        public virtual DbSet<vw_StudentsForExerciseBookResultDataEntry> vw_StudentsForExerciseBookResultDataEntry { get; set; }
        public virtual DbSet<vw_StudentsForExerciseBookResultDataView> vw_StudentsForExerciseBookResultDataView { get; set; }
        public virtual DbSet<vw_StudentsForIndividualWorkResultDataEntry> vw_StudentsForIndividualWorkResultDataEntry { get; set; }
        public virtual DbSet<vw_StudentsForIndividualWorkResultDataView> vw_StudentsForIndividualWorkResultDataView { get; set; }
        public virtual DbSet<vw_StudentsForIndividualWorkScheduleDataView> vw_StudentsForIndividualWorkScheduleDataView { get; set; }
        public virtual DbSet<vw_StudentsForLessonClassDataView> vw_StudentsForLessonClassDataView { get; set; }
        public virtual DbSet<vw_StudentsForQuizResultDataEntry> vw_StudentsForQuizResultDataEntry { get; set; }
        public virtual DbSet<vw_StudentsForQuizResultDataView> vw_StudentsForQuizResultDataView { get; set; }
        public virtual DbSet<vw_StudentsForQuizScheduleDataView> vw_StudentsForQuizScheduleDataView { get; set; }
        public virtual DbSet<vw_StudentsForTestResultDataEntry> vw_StudentsForTestResultDataEntry { get; set; }
        public virtual DbSet<vw_StudentsForTestResultDataView> vw_StudentsForTestResultDataView { get; set; }
        public virtual DbSet<vw_StudentsForTestScheduleDataView> vw_StudentsForTestScheduleDataView { get; set; }
        public virtual DbSet<vw_StudentUnPaymentDataView> vw_StudentUnPaymentDataView { get; set; }
        public virtual DbSet<vw_SubjectTeacherClassRoomList> vw_SubjectTeacherClassRoomList { get; set; }
        public virtual DbSet<vw_TeacherList> vw_TeacherList { get; set; }
        public virtual DbSet<vw_TeachersForLessonClassDataView> vw_TeachersForLessonClassDataView { get; set; }
        public virtual DbSet<vw_TestScheduleList> vw_TestScheduleList { get; set; }
        public virtual DbSet<vw_WorkerList> vw_WorkerList { get; set; }
    }
}