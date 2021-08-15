﻿namespace WeQuiz.Infrastructure
{
    using System.Security.Claims;

    using static WebConstants;

    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier).Value;


        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(WebConstants.AdministratorRoleName);

        public static bool IsSchoolAdmin(this ClaimsPrincipal user)
          => user.IsInRole(WebConstants.SchoolAdminRoleName);

        public static bool IsTeacher(this ClaimsPrincipal user)
          => user.IsInRole(WebConstants.TeacherRoleName);

        public static bool IsStudent(this ClaimsPrincipal user)
          => user.IsInRole(WebConstants.StudentRoleName);
    }
}
