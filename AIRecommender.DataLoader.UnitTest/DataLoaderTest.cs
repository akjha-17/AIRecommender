using AIRecommender.Entities;
using AIRecommender_DataLoader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AIRecommender.DataLoader.UnitTest
{
    [TestClass]
    public class PearsonRecommenderTest
    {

        CSVDataLoader target = null;
        [TestInitialize]
        public void Init()
        {
            // Before every Method
            target = new CSVDataLoader();
        }
        [TestMethod]
        public void Load_ValidISBNno_ShouldGiveSameValue()
        {
            string TestISBN = "0002005018";
            string BookName = "Clara Callan";
            BookDetails bd = target.Load();
            var temp=bd.Books.Where(r => r.ISBN == TestISBN).ToList();
            Assert.AreEqual(temp[0].BookTitle, BookName);
        }
    }
}
