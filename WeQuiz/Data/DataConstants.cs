namespace WeQuiz.Data
{
    public class DataConstants
    {
        public const int QuestionTypeMaxLength = 20;

        public const int CategoryNameMaxLength = 50;
        public const int DescriptionMaxLength = 200;

        public class School
        {
            public const int SchoolNameMaxLength = 50;
            public const int SchoolNameMinLength = 4;
            public const int PopulatedAreaNameMaxLength = 20;
            public const int PopulatedAreaNameMinLength = 3;
        }

        public class User
        {
            public const int FullNameMinLength = 10;
            public const int FullNameMaxLength = 100;
            public const int AliasMinLength = 3;
            public const int AliasMaxLength = 20;
            public const int PasswordMaxLength = 100;
            public const int PasswordMinLength = 6;
        }


    }
}
