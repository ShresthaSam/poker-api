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
    public class HelperTests_GenerateScores_Flush
    {
        [TestMethod]
        public void Same_Top1_Card_Value_Should_Be_Ranked_By_Remaining_Card_Values_Case1()
        {
            var playerEvaluation = new Dictionary<Player, (bool, List<int>)>();
            playerEvaluation.Add(
                new Player { UserId = "1", UserName = "1" },
                (true, new List<int> { 14, 11, 10, 7, 8 }));
            playerEvaluation.Add(
                new Player { UserId = "2", UserName = "2" },
                (true, new List<int> { 14, 11, 10, 9, 8 }));
            playerEvaluation.Add(
                new Player { UserId = "3", UserName = "3" },
                (true, new List<int> { 14, 12, 10, 9, 8 }));
            playerEvaluation.Add(
                new Player { UserId = "4", UserName = "4" },
                (true, new List<int> { 14, 11, 10, 9, 8 }));

            var scores = Helper.GenerateScores(CategoryEnum.Flush, playerEvaluation, Log.Logger).ToList();

            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "3").Rank == 1);
            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "2").Rank == 2);
            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "4").Rank == 2);
            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "1").Rank == 3);
        }

        [TestMethod]
        public void Same_Top1_Card_Value_Should_Be_Ranked_By_Remaining_Card_Values_Case2()
        {
            var playerEvaluation = new Dictionary<Player, (bool, List<int>)>();
            playerEvaluation.Add(
                new Player { UserId = "1", UserName = "1" },
                (true, new List<int> { 14, 11, 10, 7, 8 }));
            playerEvaluation.Add(
                new Player { UserId = "2", UserName = "2" },
                (true, new List<int> { 14, 11, 10, 9, 8 }));

            var scores = Helper.GenerateScores(CategoryEnum.Flush, playerEvaluation, Log.Logger).ToList();

            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "2").Rank == 1);
            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "1").Rank == 2);
        }

        [TestMethod]
        public void Same_Top1_Card_Value_Should_Be_Ranked_By_Remaining_Card_Values_Case3()
        {
            var playerEvaluation = new Dictionary<Player, (bool, List<int>)>();
            playerEvaluation.Add(
                new Player { UserId = "1", UserName = "1" },
                (true, new List<int> { 14, 11, 10, 7, 8 }));
            playerEvaluation.Add(
                new Player { UserId = "2", UserName = "2" },
                (true, new List<int> { 14, 11, 10, 9, 8 }));

            var scores = Helper.GenerateScores(CategoryEnum.Flush, playerEvaluation, Log.Logger).ToList();

            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "2").Rank == 1);
            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "1").Rank == 2);
        }

        [TestMethod]
        public void Same_Card_Values_Should_Be_Ranked_The_Same_Case1()
        {
            var playerEvaluation = new Dictionary<Player, (bool, List<int>)>();
            playerEvaluation.Add(
                new Player { UserId = "1", UserName = "1" },
                (true, new List<int> { 14, 11, 10, 7, 8 }));
            playerEvaluation.Add(
                new Player { UserId = "2", UserName = "2" },
                (true, new List<int> { 14, 11, 10, 7, 8 }));

            var scores = Helper.GenerateScores(CategoryEnum.Flush, playerEvaluation, Log.Logger).ToList();

            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "2").Rank == 1);
            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "1").Rank == 1);
        }

        [TestMethod]
        public void Same_Card_Values_Should_Be_Ranked_The_Same_Case2()
        {
            var playerEvaluation = new Dictionary<Player, (bool, List<int>)>();
            playerEvaluation.Add(
                new Player { UserId = "1", UserName = "1" },
                (true, new List<int> { 14, 11, 10, 7, 8 }));
            playerEvaluation.Add(
                new Player { UserId = "2", UserName = "2" },
                (true, new List<int> { 14, 11, 10, 7, 8 }));
            playerEvaluation.Add(
                new Player { UserId = "3", UserName = "3" },
                (true, new List<int> { 14, 11, 10, 7, 8 }));

            var scores = Helper.GenerateScores(CategoryEnum.Flush, playerEvaluation, Log.Logger).ToList();

            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "2").Rank == 1);
            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "1").Rank == 1);
            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "3").Rank == 1);
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
