using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerApi.Common.Models;
using PokerApi.Mappers;
using PokerApi.Models;
using System;

namespace PokerApi.MappersTests
{
    [TestClass]
    public class PokerApiModelsProfileTests
    {
        private static IMapper _mapper;

        [TestInitialize]
        public void Initialize()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new PokerApiModelsProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [TestMethod]
        public void Get_Mapped_Entity_For_Player()
        {
            var model = new Player { UserId = "FSGS7788", UserName = "Sam" };
            var mapped = _mapper.Map<Repositories.Entities.Player>(model);

            Assert.IsTrue(mapped.UserId == "FSGS7788");
            Assert.IsTrue(mapped.UserName == "Sam");
        }

        [TestMethod]
        public void Get_Mapped_Model_For_PlayerEntity()
        {
            var entity = new Repositories.Entities.Player("Pam", "YYTW5323");
            var mapped = _mapper.Map<Player>(entity);

            Assert.IsTrue(mapped.UserId == "YYTW5323");
            Assert.IsTrue(mapped.UserName == "Pam");
        }

        [TestMethod]
        public void Get_Mapped_Entity_For_CardEnum()
        {
            var model = CardEnum.D3;
            var mapped = _mapper.Map<Repositories.Entities.Card>(model);

            Assert.IsTrue(mapped.CardId == 6);
            Assert.IsTrue(mapped.Suit == "D");
            Assert.IsTrue(mapped.Number == "3");
        }

        [TestMethod]
        public void Get_Mapped_Entity_For_CardEnum_Char3()
        {
            var model = CardEnum.H10;
            var mapped = _mapper.Map<Repositories.Entities.Card>(model);

            Assert.IsTrue(mapped.CardId == 33);
            Assert.IsTrue(mapped.Suit == "H");
            Assert.IsTrue(mapped.Number == "10");
        }

        [TestMethod]
        public void Get_Mapped_Model_For_CardEntity()
        {
            var entity = new Repositories.Entities.Card
            {
                CardId = 7,
                Suit = "C",
                Number = "3"
            };
            var mapped = _mapper.Map<CardEnum>(entity);

            Assert.IsTrue(mapped == CardEnum.C3);
        }

        [TestMethod]
        public void Get_Error_On_Mapped_Model_For_Invalid_CardEntity_Number()
        {
            var entity = new Repositories.Entities.Card
            {
                CardId = 7,
                Suit = "C",
                Number = "5"
            };
            Assert.ThrowsException<Exception>(() => _mapper.Map<CardEnum>(entity));
        }

        [TestMethod]
        public void Get_Error_On_Mapped_Model_For_Invalid_CardEntity_CardId()
        {
            var entity = new Repositories.Entities.Card
            {
                CardId = 22222,
                Suit = "C",
                Number = "3"
            };
            Assert.ThrowsException<Exception>(() => _mapper.Map<CardEnum>(entity));
        }

        [TestMethod]
        public void Get_Mapped_Entity_For_PokerHand()
        {
            var player = new Player { UserId = "123456", UserName = "Sam" };
            var model = new PokerHandDomainModel 
            {
                PokerHandId = 637747,
                Card1 = CardEnum.C2,
                Card2 = CardEnum.C3,
                Card3 = CardEnum.D2,
                Card4 = CardEnum.D3,
                Card5 = CardEnum.H3,
                Player = player
            };
            var mapped = _mapper.Map<Repositories.Entities.Hand>(model);

            Assert.IsTrue(mapped.HandId == 637747);
            Assert.IsTrue(mapped.PlayerUserId == "123456");
            Assert.IsTrue(mapped.CardId1 == (int)CardEnum.C2);
            Assert.IsTrue(mapped.CardId2 == (int)CardEnum.C3);
            Assert.IsTrue(mapped.CardId3 == (int)CardEnum.D2);
            Assert.IsTrue(mapped.CardId4 == (int)CardEnum.D3);
            Assert.IsTrue(mapped.CardId5 == (int)CardEnum.H3);
        }

        [TestMethod]
        public void Get_Mapped_Model_For_HandEntity()
        {
            var entity = new Repositories.Entities.Hand
            {
                HandId = 7,
                PlayerUserId = "C83838JS",
                CardId1 = 1,
                CardId2 = 2,
                CardId3 = 3,
                CardId4 = 5,
                CardId5 = 7
            };
            var mapped = _mapper.Map<PokerHandDomainModel>(entity);

            Assert.IsTrue(mapped.PokerHandId == 7);
            Assert.IsTrue(mapped.Player.UserId == "C83838JS");
            Assert.IsTrue(mapped.Card1 == CardEnum.H2);
            Assert.IsTrue(mapped.Card2 == CardEnum.D2);
            Assert.IsTrue(mapped.Card3 == CardEnum.C2);
            Assert.IsTrue(mapped.Card4 == CardEnum.H3);
            Assert.IsTrue(mapped.Card5 == CardEnum.C3);
        }
    }
}
