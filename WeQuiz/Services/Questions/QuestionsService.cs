﻿using System.Collections.Generic;
using System.Linq;
using WeQuiz.Data;
using WeQuiz.Data.Models;

namespace WeQuiz.Services.Questions
{
    public class QuestionsService : IQuestionsService
    {
        private readonly WeQuizDbContext data;

        public QuestionsService(WeQuizDbContext data)
            => this.data = data;

        public QuestionQueryServiceModel All(
            string userId,
            int categoryId = 0,
            string subCategory = null,
            int klas = 0,
            int questionTypeId = 0,
            string text = null,
            int currentPage = 1,
            int questionsPerPage = 10)
        {
            var schoolId = this.data.Users.Find(userId).SchoolId;

            var questionsQuery = this.data.Questions
                   .Where(q => q.SchoolId == 0 || q.SchoolId == schoolId);

            if (categoryId > 0)
            {
                questionsQuery = questionsQuery
                    .Where(q => q.Subcategory.CategoryId == categoryId);
            }

            if (klas > 0)
            {
                questionsQuery = questionsQuery
                   .Where(q => q.Class == klas);
            }

            if (questionTypeId > 0)
            {
                questionsQuery = questionsQuery
                   .Where(q => q.QuestionTypeId == questionTypeId);
            }

            if (text != null)
            {
                questionsQuery = questionsQuery
                   .Where(q => q.Text.ToLower().Contains(text.ToLower()));
            }

            if (subCategory != null)
            {
                questionsQuery = questionsQuery
                   .Where(q => q.Subcategory.Name.ToLower()
                        .Contains(subCategory.ToLower()));
            }

            var totalQuestions = questionsQuery.Count();

            var questions = questionsQuery
                .Skip((currentPage - 1) * questionsPerPage)
                .Take(questionsPerPage)
                .Select(q => new QuestionServiceModel
                {
                    Id = q.Id,
                    SubcategoryId = q.SubcategoryId,
                    SubcategoryName = q.Subcategory.Name,
                    CategoryName = q.Subcategory.Category.Name,
                    Class = q.Class,
                    SchoolId = q.SchoolId,
                    QuestionTypeId = q.QuestionTypeId,
                     Text = q.Text
                })
                .ToList();

            return new QuestionQueryServiceModel
            {
                CurrentPage = currentPage,
                QuestionsPerPage = questionsPerPage,
                TotalQuestions = totalQuestions,
                Questions = questions
            };
        }

        IEnumerable<QuestionTypeServiceModel> IQuestionsService.QuestionTypes()
        {
            var questionTypes = this.data.QuestionTypes
                .Select(q => new QuestionTypeServiceModel
                {
                    Id = q.Id,
                    QuestionType = q.Type
                })
                .ToList();

            return questionTypes;
        }
    }
}
