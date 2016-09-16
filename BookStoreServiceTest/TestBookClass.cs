using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookStoreService;

namespace BookStoreServiceTest
{
    [TestClass]
    public class TestBookClass
    {
        [TestMethod]
        public void TestMethod1()
        {
            Book myBook = new Book {Author = "Fred Smith"};
            Assert.AreEqual("Fred Smith", myBook.Author);
        }
    }
}
