namespace WeQuiz.Data
{
    public class DataConstants
    {
        public class Question
        {
            public const int QuestionTypeMaxLength = 20;
            public const int TextMaxLength = 250;
            public const int TextMinLength = 10;
            public const int AnswerMaxLength = 100;
            public const int AnswerMinLength = 2;
        }

        public class Test
        {
            public const int TestTypeMaxLength = 20;
            public const int TitleMaxLength = 100;
            public const int TitleMinLength = 2;
        }

        public const int DescriptionMaxLength = 200;

        public class Category
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public class School
        {
            public const int SchoolNameMaxLength = 50;
            public const int SchoolNameMinLength = 4;
        }

        public class PopulatedArea
        {
            public const int NameMaxLength = 20;
            public const int NameMinLength = 3;
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
