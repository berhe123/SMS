using System.Web.Mvc;
using SMS.Entities;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using SMS.Core;
using SMS.Business.Service;
using SMS.Business;
using System.Web.Http;

namespace SMS.Web
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new UnityDependencyResolver(container));
      GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
        var container = new UnityContainer();

        container.RegisterType<ILogger, LoggingSvc>();      
        container.RegisterType<IComboEntitySvc<AppSetting, AppSetting, AppSetting>, AppSettingSvc>();
        container.RegisterType<IComboEntitySvc<Payment, Payment, Payment>, PaymentSvc>();
        container.RegisterType<IComboEntitySvc<ClassRoom, ClassRoom, ClassRoom>, ClassRoomSvc>();
        container.RegisterType<IComboEntitySvc<Subject, Subject, Subject>, SubjectSvc>();     
        container.RegisterType<IComboEntitySvc<Student, Student, Student>, StudentSvc>();
        //container.RegisterType<IComboEntitySvc<Salary, Salary, Salary>, SalarySvc>();
        container.RegisterType<IComboEntitySvc<Teacher, Teacher, Teacher>, TeacherSvc>();
        container.RegisterType<IComboEntitySvc<Worker, Worker, Worker>, WorkerSvc>();
        container.RegisterType<IComboEntitySvc<Holiday, Holiday, Holiday>, HolidaySvc>();
        container.RegisterType<IComboEntitySvc<FeeSetting, FeeSetting, FeeSetting>, FeeSettingSvc>();
        container.RegisterType<IComboEntitySvc<Parent, Parent, Parent>, ParentSvc>();
        container.RegisterType<IComboEntitySvc<LessonClass, LessonClass, LessonClass>, LessonClassSvc>();
        container.RegisterType<IComboEntitySvc<SubjectTeacherClassRoom, SubjectTeacherClassRoom, SubjectTeacherClassRoom>, SubjectTeacherClassRoomSvc>();
        container.RegisterType<IComboEntitySvc<PaymentCalander, PaymentCalander, PaymentCalander>, PaymentCalanderSvc>();
        container.RegisterType<IComboEntitySvc<PaymentCalandersToClassRoom, PaymentCalandersToClassRoom, PaymentCalandersToClassRoom>, PaymentCalandersToClassRoomSvc>();
        container.RegisterType<IComboEntitySvc<AcademicCalander, AcademicCalander, AcademicCalander>, AcademicCalanderSvc>();
        container.RegisterType<IComboEntitySvc<Period, Period, Period>, PeriodSvc>();
        container.RegisterType<IComboEntitySvc<ExamSchedule, ExamSchedule, ExamSchedule>, ExamScheduleSvc>();
        container.RegisterType<IComboEntitySvc<IndividualWorkSchedule, IndividualWorkSchedule, IndividualWorkSchedule>, IndividualWorkScheduleSvc>();
        container.RegisterType<IComboEntitySvc<IndividualWorkResult, IndividualWorkResult, IndividualWorkResult>, IndividualWorkResultSvc>();
        container.RegisterType<IComboEntitySvc<QuizSchedule, QuizSchedule, QuizSchedule>, QuizScheduleSvc>();
        container.RegisterType<IComboEntitySvc<QuizResult, QuizResult, QuizResult>, QuizResultSvc>();
        container.RegisterType<IComboEntitySvc<TestSchedule, TestSchedule, TestSchedule>, TestScheduleSvc>();
        container.RegisterType<IComboEntitySvc<TestResult, TestResult, TestResult>, TestResultSvc>();
        container.RegisterType<IComboEntitySvc<GroupWorkSchedule, GroupWorkSchedule, GroupWorkSchedule>, GroupWorkScheduleSvc>();
        container.RegisterType<IComboEntitySvc<GroupWorkResult, GroupWorkResult, GroupWorkResult>, GroupWorkResultSvc>();
        container.RegisterType<IComboEntitySvc<StudentAttendance, StudentAttendance, StudentAttendance>, StudentAttendanceSvc>();
        container.RegisterType<IComboEntitySvc<ExerciseBookResult, ExerciseBookResult, ExerciseBookResult>, ExerciseBookResultSvc>();
        container.RegisterType<IComboEntitySvc<ExamResult, ExamResult, ExamResult>, ExamResultSvc>();
        container.RegisterType<IComboEntitySvc<StudentClassRoom, StudentClassRoom, StudentClassRoom>, StudentClassRoomSvc>();
        container.RegisterType<IComboEntitySvc<GroupMember, GroupMember, GroupMember>, GroupMemberSvc>();
        container.RegisterType<IComboEntitySvc<GroupName, GroupName, GroupName>, GroupNameSvc>();

        container.RegisterType<IReadOnlyEntitySvc<UserProfile, UserProfile, UserProfile>, UserProfileSvc>();
        container.RegisterType<IReadOnlyEntitySvc<security_Roles, security_Roles, security_Roles>, RoleSvc>();
        container.RegisterType<IEntitySvc<UsersInLocation, UsersInLocation, UsersInLocation>, UsersInLocationSvc>();
        



        container.RegisterType<FilterSpecSvc>();
       
        RegisterTypes(container);

        return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
    
    }
  }
}
