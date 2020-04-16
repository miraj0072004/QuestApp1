using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuestionRevisedApi.Models;

namespace QuestionRevisedApi.Data
{
    public class QuestionsRevisedContext :DbContext
    {
        public QuestionsRevisedContext(DbContextOptions<QuestionsRevisedContext> options) :base(options)
        {}

        public DbSet<Question> Questions { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
