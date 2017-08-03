using System;
using System.Collections.Generic;
using System.Data.Entity;
using FluentAssertions;
using Microsoft.AspNet.Identity;
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
    public class PollRepositoryTests
    {
        private PollRepository _pollRepository;
        private Mock<IDbSet<Poll>> _mockPolls;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockPolls = new Mock<IDbSet<Poll>>();
            var mockContext = new Mock<IApplicationDbContext>();

            mockContext.SetupGet(c => c.Polls).Returns(_mockPolls.Object);
            _pollRepository = new PollRepository(mockContext.Object);
        }

        [TestMethod]
        public void Get_PollDoesntExist_ShouldThrowNullReferenceException()
        {
            _pollRepository.Invoking(m => m.GetSingle(1)).ShouldThrow<NullReferenceException>();
        }

        [TestMethod]
        public void Get_PollExists_ShouldBeReturned()
        {
            var poll = new Poll { Id = 1, Question = "Question1" };
            _mockPolls.SetSource(new[] { poll });

            var pollFromDb = _pollRepository.GetSingle(1);

            pollFromDb.Should().NotBeNull();
            pollFromDb.Should().Be(poll);
        }

        [TestMethod]
        public void GetAll_NoPollExists_ShouldReturnEmptyIEnumerable()
        {
            //_mockPolls.SetSource(new List<Poll>());
            //var allPolls = _pollRepository.GetAll();
            //allPolls.Should().HaveCount(0);
        }

        [TestMethod]
        public void GetAll_PollsExist_ShouldReturnAllPolls()
        {
            var poll1 = new Poll { Id = 1, Question = "Question1" };
            var poll2 = new Poll { Id = 2, Question = "Question2" };
            var poll3 = new Poll { Id = 3, Question = "Question3" };
            _mockPolls.SetSource(new[] { poll1, poll2, poll3 });

            //var allPolls = _pollRepository.GetAll();

            //allPolls.Should().NotBeNull();
            //allPolls.Should().HaveCount(3);
        }

        [TestMethod]
        public void GetAll_UserPollsExist_ShouldReturnGivenUserPolls()
        {
            var poll1 = new Poll { Id = 1, Question = "Question1", UserId = "1" };
            var poll2 = new Poll { Id = 2, Question = "Question2", UserId = "2" };
            var poll3 = new Poll { Id = 3, Question = "Question2.1", UserId = "2" };
            var poll4 = new Poll { Id = 4, Question = "Question3", UserId = "3" };
            _mockPolls.SetSource(new[] { poll1, poll2, poll3, poll4 });

            var allPolls = _pollRepository.Get("2");

            allPolls.Should().NotBeNull();
            allPolls.Should().HaveCount(2);
        }

        [TestMethod]
        public void GetAll_UserDoesntHavePolls_ShouldReturnEmptyIEnumerable()
        {
            var poll1 = new Poll { Id = 1, Question = "Question1", UserId = "1" };
            var poll2 = new Poll { Id = 2, Question = "Question2", UserId = "2" };
            var poll3 = new Poll { Id = 3, Question = "Question2.1", UserId = "2" };
            var poll4 = new Poll { Id = 4, Question = "Question3", UserId = "3" };
            _mockPolls.SetSource(new[] { poll1, poll2, poll3, poll4 });

            var allPolls = _pollRepository.Get("4");

            allPolls.Should().NotBeNull();
            allPolls.Should().HaveCount(0);
        }

        [TestMethod]
        public void GetAllFiltered_ValidRequest_ShouldReturnFittingPolls()
        {
            var poll1 = new Poll { Id = 1, Question = "What is it?", User = new ApplicationUser { Id = "1", UserName = "JohnHansen1@gmail.com" } };
            var poll2 = new Poll { Id = 2, Question = "What is your favorite color?", User = new ApplicationUser { Id = "2", UserName = "test@test.com" } };
            var poll3 = new Poll { Id = 3, Question = "Do you like winter?", User = new ApplicationUser { Id = "2", UserName = "Anonymous" } };
            var poll4 = new Poll { Id = 4, Question = "Is it better or worse?", User = new ApplicationUser { Id = "3", UserName = "noemail123@email.com" } };
            _mockPolls.SetSource(new[] { poll1, poll2, poll3, poll4 });

            var polls = _pollRepository.GetAll("4");
            polls.Should().HaveCount(1);

            polls = _pollRepository.GetAll("Anonymous");
            polls.Should().HaveCount(1);

            polls = _pollRepository.GetAll("2");
            polls.Should().NotBeNull();
            polls.Should().HaveCount(2);

            polls = _pollRepository.GetAll("What is");
            polls.Should().HaveCount(2);

            polls = _pollRepository.GetAll("is");
            polls.Should().HaveCount(2);
        }

        [TestMethod]
        public void GetAnswers_PollDoesntExist_ShouldThrowNullReferenceException()
        {
            _pollRepository.Invoking(m => m.GetAnswers(1)).ShouldThrow<NullReferenceException>();
        }

        [TestMethod]
        public void GetPollAnswers_PollHasNoAnswers_ShouldReturnNull()
        {
            var poll = new Poll { Id = 1, Question = "Question1" };
            _mockPolls.SetSource(new[] { poll });

            var answers = _pollRepository.GetAnswers(1);

            answers.Should().BeEmpty();
        }

        [TestMethod]
        public void GetAnswers_PollExistsAndHasOneAnswer_ShouldReturnOneAnswer()
        {
            var answer1 = new Answer { Id = 1, Content = "Content1", PollId = 1, Votes = 3 };
            var answerList = new List<Answer> { answer1 };
            var poll = new Poll { Id = 1, Question = "Question1", Answers = answerList };

            _mockPolls.SetSource(new[] { poll });

            var answers = _pollRepository.GetAnswers(1);

            answers.Should().Contain(answerList);
        }

        [TestMethod]
        public void GetAnswers_PollExistsAndHasSeveralAnswers_ShouldReturnSeveralAnswers()
        {
            var answer1 = new Answer { Id = 1, Content = "Content1", PollId = 1, Votes = 3 };
            var answer2 = new Answer { Id = 2, Content = "Content2", PollId = 1, Votes = 6 };
            var answer3 = new Answer { Id = 3, Content = "Content3", PollId = 1, Votes = 9 };
            var answerList = new List<Answer> { answer1, answer2, answer3 };
            var poll = new Poll { Id = 1, Question = "Question1", Answers = answerList };

            _mockPolls.SetSource(new[] { poll });

            var answers = _pollRepository.GetAnswers(1);

            answers.Should().Contain(answerList);
        }

        [TestMethod]
        public void GetBestAnswers_PollHasNoAnswers_ShouldNotBeReturned()
        {
            var poll = new Poll { Id = 1, Question = "Question1" };
            _mockPolls.SetSource(new[] { poll });

            var bestAnswers = _pollRepository.GetBestAnswers(1);
            bestAnswers.Should().BeEmpty();
        }

        [TestMethod]
        public void GetBestAnswers_PollHasOneBestAnswer_ShouldReturnListWithOneAnswer()
        {
            var answer1 = new Answer { Id = 1, Content = "Content1", PollId = 1, Votes = 3 };
            var answer2 = new Answer { Id = 2, Content = "Content2", PollId = 1, Votes = 6 };
            var answer3 = new Answer { Id = 3, Content = "Content3", PollId = 1, Votes = 9 };
            var answerList = new List<Answer> { answer1, answer2, answer3 };
            var poll = new Poll { Id = 1, Question = "Question1", Answers = answerList };

            _mockPolls.SetSource(new[] { poll });
            var bestAnswers = _pollRepository.GetBestAnswers(1);

            bestAnswers.Should().Contain(answer3);
            bestAnswers.Should().HaveCount(1);
        }

        [TestMethod]
        public void GetBestAnswers_PollHaveSeveralAnswersWithHigestVotes_ShouldReturnListWithSeveralAnswers()
        {
            var answer1 = new Answer { Id = 1, Content = "Content1", PollId = 1, Votes = 9 };
            var answer2 = new Answer { Id = 2, Content = "Content2", PollId = 1, Votes = 9 };
            var answer3 = new Answer { Id = 3, Content = "Content3", PollId = 1, Votes = 9 };
            var answer4 = new Answer { Id = 4, Content = "Content4", PollId = 1, Votes = 4 };
            var answerList = new List<Answer> { answer1, answer2, answer3, answer4 };
            var poll = new Poll { Id = 1, Question = "Question1", Answers = answerList };

            _mockPolls.SetSource(new[] { poll });

            var bestAnswers = _pollRepository.GetBestAnswers(1);
            var resultList = new List<Answer> { answer1, answer2, answer3 };
            bestAnswers.Should().Contain(resultList);
            bestAnswers.Should().HaveCount(3);
        }

        [TestMethod]
        public void GetBestAnswers_AnswersHasNoVotes_ShouldNotBeReturned()
        {
            var answer1 = new Answer { Id = 1, Content = "Content1", PollId = 1, Votes = 0 };
            var answer2 = new Answer { Id = 2, Content = "Content2", PollId = 1 };
            var answer3 = new Answer { Id = 3, Content = "Content3", PollId = 1, Votes = 0 };
            var answerList = new List<Answer> { answer1, answer2, answer3 };
            var poll = new Poll { Id = 1, Question = "Question1", Answers = answerList };

            _mockPolls.SetSource(new[] { poll });

            var bestAnswers = _pollRepository.GetBestAnswers(1);

            bestAnswers.Should().BeEmpty();
        }
    }
}
