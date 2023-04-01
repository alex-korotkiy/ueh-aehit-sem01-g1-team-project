using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Website.Infrastructure.Repositories;
using Website.Models.DbDto;
using Website.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebsiteTests.ControllerTests
{
    class BooksControllerTests
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestGet()
        {
            //Arrange
            var bookId = 1;
            var bookInfo = new BookInfo();

            var booksRepoMock = new Mock<IBooksRepository>();
            booksRepoMock.Setup(repo => repo.Get(bookId)).Returns(bookInfo);

            var usersRepoMock = new Mock<IUsersRepository>();
            var booksController = new BooksController(booksRepoMock.Object, usersRepoMock.Object);

            //Act
            var result = booksController.Get(bookId);

            //Assert
            var jsonResult = result as JsonResult;
            Assert.AreEqual(bookInfo, jsonResult.Value);
        }
    }
}
