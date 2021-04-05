using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerApi.Models;
using PokerApi.RankAssignment.Simple.Models;
using PokerApi.RankAssignment.Simple.ScoreKeepers;
using Serilog;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PokerApi.RankAssignment.SimpleTests
{
    [TestClass]
    public class HelperTests_GenerateScores_HighCard
    {
        [TestMethod]
        public void HighCard_Should_Be_Ranked_By_Card_Values_Case1()
        {
            var playerEvaluation = new Dictionary<Player, (bool, List<int>)>();
            playerEvaluation.Add(
                new Player { UserId = "1", UserName = "1" },
                (true, new List<int> { 14, 11, 8, 7, 2 }));
            playerEvaluation.Add(
                new Player { UserId = "2", UserName = "2" },
                (true, new List<int> { 14, 11, 10, 8, 3 }));

            var scores = Helper.GenerateScores(CategoryEnum.HighCard, playerEvaluation, Log.Logger).ToList();

            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "2").Rank == 1);
            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "1").Rank == 2);
        }

        [TestMethod]
        public void HighCard_Should_Be_Ranked_By_Card_Values_Case2()
        {
            var playerEvaluation = new Dictionary<Player, (bool, List<int>)>();
            playerEvaluation.Add(
                new Player { UserId = "1", UserName = "1" },
                (true, new List<int> { 14, 11, 8, 7, 2 }));
            playerEvaluation.Add(
                new Player { UserId = "2", UserName = "2" },
                (true, new List<int> { 14, 11, 10, 8, 3 }));
            playerEvaluation.Add(
                new Player { UserId = "3", UserName = "3" },
                (true, new List<int> { 14, 11, 8, 7, 2 }));
            playerEvaluation.Add(
                new Player { UserId = "4", UserName = "4" },
                (true, new List<int> { 10, 9, 7, 3, 2 }));

            var scores = Helper.GenerateScores(CategoryEnum.HighCard, playerEvaluation, Log.Logger).ToList();

            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "2").Rank == 1);
            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "1").Rank == 2);
            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "3").Rank == 2);
            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "4").Rank == 3);
        }

        [TestMethod]
        public void HighCard_Should_Be_Ranked_By_Card_Values_Case3()
        {
            var playerEvaluation = new Dictionary<Player, (bool, List<int>)>();
            playerEvaluation.Add(
                new Player { UserId = "1", UserName = "1" },
                (true, new List<int> { 14, 11, 8, 7, 2 }));
            playerEvaluation.Add(
                new Player { UserId = "2", UserName = "2" },
                (true, new List<int> { 14, 9, 10, 7, 2 }));
            playerEvaluation.Add(
                new Player { UserId = "3", UserName = "3" },
                (true, new List<int> { 14, 11, 8, 7, 2 }));
            playerEvaluation.Add(
                new Player { UserId = "4", UserName = "4" },
                (true, new List<int> { 10, 7, 5, 3, 2 }));

            var scores = Helper.GenerateScores(CategoryEnum.HighCard, playerEvaluation, Log.Logger).ToList();

            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "1").Rank == 1);
            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "3").Rank == 1);
            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "2").Rank == 2);
            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "4").Rank == 3);
        }

        private static void CreateLogger()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
