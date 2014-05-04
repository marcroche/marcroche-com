using System;
using MarcRoche.Domain.Blog;
using MarcRoche.Repository.Mongo.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Linq;

namespace MarcRoche.Repository.Mongo.Tests
{
    [TestClass]
    public class CrudTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var connectionString = "mongodb://localhost";
            var mongoClient = new MongoClient(connectionString);
            var mongoServer = mongoClient.GetServer();
            var databaseName = "blog";
            var db = mongoServer.GetDatabase(databaseName);
            var games = db.GetCollection<AboutMeEntity>("posts");

            AboutMeEntity about = new AboutMeEntity
            {
                Author = "marcroche",
                AboutMeId = Guid.Parse("2471ecc5-256e-4ea9-accf-d74a652015d4"),
                Content = "##About Me\r\n\r\n<time class=\"postinfo left-50 postdate\">November 29, 2013</time>\r\n\r\nA little about me and this blog... <img class=\"img-right\" src=\"/BlogArtifacts/about-me/me.jpg\" />\r\n\r\nI am a Technical Software Architect and Lead Software Developer with over 10 years of experience. My specialities include designing and developing enterprise messaging solutions and web applications.\r\n\r\nOver the last decade I have had the opportunity to gain experience in all phases of software development. This includes leading projects from concept and prototype development, through to documentation, estimation, and delivery.\r\n\r\nThe majority of this time has been spent developing with C# and the .Net framework. Application domains have ranged from Telecommunication products, E-Commerce, application frameworks, and business support systems.\r\n\r\nRecently my interest has started shifting to dynamic and functional languages. I find myself frequently working with Javascript, Python, and Ruby (with an empahsis on server side and client side Javascript). I also expect the topics I blog about to reflect this as well.\r\n\r\nThe intent of this blog is to create a platform where I can share my experiences and explore new ideas. Hopefully it will open the door to new ideas and technologies for myself and others!\r\n\r\nOutside of software development, I enjoy chess, art, music, exercise, soccer, and family.",
                HtmlContent = "",
                PublishDate = DateTime.Parse("2012-08-25T00:00:00"),
                Title = "About Me"
            };

            games.Insert(about);

            var aboutMeQuery = Query<AboutMeEntity>.EQ(g => g.Author, "marcroche");

            var aboutMe = games.FindOne(aboutMeQuery);
        }
    }
}
