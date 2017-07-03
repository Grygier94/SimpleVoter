using System;
using System.Collections.Generic;
using System.Data.Entity;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SimpleVoter.Core;
using SimpleVoter.Core.Models;
using SimpleVoter.Persistence;
using SimpleVoter.Persistence.Repositories;
using SimpleVoter.Tests.Extensions;

namespace SimpleVoter.Tests.Persistence.Repositories
{
    [TestClass]
    public class AnswerRepositoryTests
    {
        private AnswerRepository _answerRepository;
        private Mock<IDbSet<Answer>> _mockAnswers;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockAnswers = new Mock<IDbSet<Answer>>();

            var mockContext = new Mock<IApplicationDbContext>();

            mockContext.SetupGet(c => c.Answers).Returns(_mockAnswers.Object);
            _answerRepository = new AnswerRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetVotes_AnswerHasNoVotes_ShouldReturn0()
        {
            var answer1 = new Answer { Id = 1, Content = "Content1", Votes = 4};
            var answerList = new List<Answer> { answer1 };

            _mockAnswers.SetSource(answerList);

            var answers = _answerRepository.GetVotes(answerId: 1);

            answers.Should().Be(4);
        }

        [TestMethod]
        public void GetVotes_AnswerHas0Votes_ShouldReturn0()
        {
            var answer1 = new Answer { Id = 1, Content = "Content1", PollId = 1, Votes = 0 };
            var answerList = new List<Answer> { answer1 };

            _mockAnswers.SetSource(answerList);

            var answers = _answerRepository.GetVotes(answerId: 1);

            answers.Should().Be(0);
        }

        [TestMethod]
        public void GetVotes_AnswerHasManyVotes_ShouldReturnNumberOfVotes()
        {
            var answer1 = new Answer { Id = 1, Content = "Content1", PollId = 1, Votes = 331 };
            var answer2 = new Answer { Id = 2, Content = "Content2", PollId = 1, Votes = 4343 };
            var answer3 = new Answer { Id = 3, Content = "Content3", PollId = 1, Votes = 3657 };
            var answerList = new List<Answer> { answer1, answer2, answer3 };

            _mockAnswers.SetSource(answerList);

            var answers = _answerRepository.GetVotes(answerId: 2);

            answers.Should().Be(4343);
        }

        [TestMethod]
        public void GetVotes_AnswerDoesntExist_ShouldThrowNullReferenceException()
        {
            _answerRepository.Invoking(m => m.GetVotes(answerId: 1)).ShouldThrow<NullReferenceException>();
        }
    }
}
