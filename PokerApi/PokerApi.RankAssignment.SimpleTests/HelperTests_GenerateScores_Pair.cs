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
    public class HelperTests_GenerateScores_Pair
    {
        [TestMethod]
        public void Same_Pair_Should_Be_Ranked_By_Remaining_Card_Values_Case1()
        {
            var playerEvaluation = new Dictionary<Player, (bool, int, List<int>)>();
            playerEvaluation.Add(
                new Player { UserId = "1", UserName = "1" },
                (true, 5, new List<int> { 14, 11, 8 }));
            playerEvaluation.Add(
                new Player { UserId = "2", UserName = "2" },
                (true, 5, new List<int> { 14, 11, 10 }));

            var scores = Helper.GenerateScores(CategoryEnum.Pair, playerEvaluation, Log.Logger).ToList();

            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "2").Rank == 1);
            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "1").Rank == 2);
        }

        [TestMethod]
        public void Same_Pair_Should_Be_Ranked_By_Remaining_Card_Values_Case2()
        {
            var playerEvaluation = new Dictionary<Player, (bool, int, List<int>)>();
            playerEvaluation.Add(
                new Player { UserId = "1", UserName = "1" },
                (true, 9, new List<int> { 13, 8, 7 }));
            playerEvaluation.Add(
                new Player { UserId = "2", UserName = "2" },
                (true, 9, new List<int> { 14, 11, 3 }));
            playerEvaluation.Add(
                new Player { UserId = "3", UserName = "3" },
                (true, 9, new List<int> { 13, 8, 7 }));
            playerEvaluation.Add(
                new Player { UserId = "4", UserName = "4" },
                (true, 9, new List<int> { 14, 11, 2 }));
            playerEvaluation.Add(
                new Player { UserId = "5", UserName = "5" },
                (true, 9, new List<int> { 9, 7, 4 }));

            var scores = Helper.GenerateScores(CategoryEnum.TwoPairs, playerEvaluation, Log.Logger).ToList();

            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "2").Rank == 1);
            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "4").Rank == 2);
            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "1").Rank == 3);
            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "3").Rank == 3);
            Assert.IsTrue(scores.FirstOrDefault(i => i.Player.UserId == "5").Rank == 4);
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
