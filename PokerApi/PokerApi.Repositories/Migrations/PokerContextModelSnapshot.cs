// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PokerApi.Repositories;

namespace PokerApi.Repositories.Migrations
{
    [DbContext(typeof(PokerContext))]
    partial class PokerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("PokerApi.Repositories.Entities.Card", b =>
                {
                    b.Property<int>("CardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("card_id");

                    b.Property<string>("Number")
                        .HasColumnType("TEXT")
                        .HasColumnName("number");

                    b.Property<string>("Suit")
                        .HasColumnType("TEXT")
                        .HasColumnName("suit");

                    b.HasKey("CardId");

                    b.ToTable("card");

                    b.HasData(
                        new
                        {
                            CardId = 1,
                            Number = "2",
                            Suit = "H"
                        },
                        new
                        {
                            CardId = 2,
                            Number = "2",
                            Suit = "D"
                        },
                        new
                        {
                            CardId = 3,
                            Number = "2",
                            Suit = "C"
                        },
                        new
                        {
                            CardId = 4,
                            Number = "2",
                            Suit = "S"
                        },
                        new
                        {
                            CardId = 5,
                            Number = "3",
                            Suit = "H"
                        },
                        new
                        {
                            CardId = 6,
                            Number = "3",
                            Suit = "D"
                        },
                        new
                        {
                            CardId = 7,
                            Number = "3",
                            Suit = "C"
                        },
                        new
                        {
                            CardId = 8,
                            Number = "3",
                            Suit = "S"
                        },
                        new
                        {
                            CardId = 9,
                            Number = "4",
                            Suit = "H"
                        },
                        new
                        {
                            CardId = 10,
                            Number = "4",
                            Suit = "D"
                        },
                        new
                        {
                            CardId = 11,
                            Number = "4",
                            Suit = "C"
                        },
                        new
                        {
                            CardId = 12,
                            Number = "4",
                            Suit = "S"
                        },
                        new
                        {
                            CardId = 13,
                            Number = "5",
                            Suit = "H"
                        },
                        new
                        {
                            CardId = 14,
                            Number = "5",
                            Suit = "D"
                        },
                        new
                        {
                            CardId = 15,
                            Number = "5",
                            Suit = "C"
                        },
                        new
                        {
                            CardId = 16,
                            Number = "5",
                            Suit = "S"
                        },
                        new
                        {
                            CardId = 17,
                            Number = "6",
                            Suit = "H"
                        },
                        new
                        {
                            CardId = 18,
                            Number = "6",
                            Suit = "D"
                        },
                        new
                        {
                            CardId = 19,
                            Number = "6",
                            Suit = "C"
                        },
                        new
                        {
                            CardId = 20,
                            Number = "6",
                            Suit = "S"
                        },
                        new
                        {
                            CardId = 21,
                            Number = "7",
                            Suit = "H"
                        },
                        new
                        {
                            CardId = 22,
                            Number = "7",
                            Suit = "D"
                        },
                        new
                        {
                            CardId = 23,
                            Number = "7",
                            Suit = "C"
                        },
                        new
                        {
                            CardId = 24,
                            Number = "7",
                            Suit = "S"
                        },
                        new
                        {
                            CardId = 25,
                            Number = "8",
                            Suit = "H"
                        },
                        new
                        {
                            CardId = 26,
                            Number = "8",
                            Suit = "D"
                        },
                        new
                        {
                            CardId = 27,
                            Number = "8",
                            Suit = "C"
                        },
                        new
                        {
                            CardId = 28,
                            Number = "8",
                            Suit = "S"
                        },
                        new
                        {
                            CardId = 29,
                            Number = "9",
                            Suit = "H"
                        },
                        new
                        {
                            CardId = 30,
                            Number = "9",
                            Suit = "D"
                        },
                        new
                        {
                            CardId = 31,
                            Number = "9",
                            Suit = "C"
                        },
                        new
                        {
                            CardId = 32,
                            Number = "9",
                            Suit = "S"
                        },
                        new
                        {
                            CardId = 33,
                            Number = "10",
                            Suit = "H"
                        },
                        new
                        {
                            CardId = 34,
                            Number = "10",
                            Suit = "D"
                        },
                        new
                        {
                            CardId = 35,
                            Number = "10",
                            Suit = "C"
                        },
                        new
                        {
                            CardId = 36,
                            Number = "10",
                            Suit = "S"
                        },
                        new
                        {
                            CardId = 37,
                            Number = "J",
                            Suit = "H"
                        },
                        new
                        {
                            CardId = 38,
                            Number = "J",
                            Suit = "D"
                        },
                        new
                        {
                            CardId = 39,
                            Number = "J",
                            Suit = "C"
                        },
                        new
                        {
                            CardId = 40,
                            Number = "J",
                            Suit = "S"
                        },
                        new
                        {
                            CardId = 41,
                            Number = "Q",
                            Suit = "H"
                        },
                        new
                        {
                            CardId = 42,
                            Number = "Q",
                            Suit = "D"
                        },
                        new
                        {
                            CardId = 43,
                            Number = "Q",
                            Suit = "C"
                        },
                        new
                        {
                            CardId = 44,
                            Number = "Q",
                            Suit = "S"
                        },
                        new
                        {
                            CardId = 45,
                            Number = "K",
                            Suit = "H"
                        },
                        new
                        {
                            CardId = 46,
                            Number = "K",
                            Suit = "D"
                        },
                        new
                        {
                            CardId = 47,
                            Number = "K",
                            Suit = "C"
                        },
                        new
                        {
                            CardId = 48,
                            Number = "K",
                            Suit = "S"
                        },
                        new
                        {
                            CardId = 49,
                            Number = "A",
                            Suit = "H"
                        },
                        new
                        {
                            CardId = 50,
                            Number = "A",
                            Suit = "D"
                        },
                        new
                        {
                            CardId = 51,
                            Number = "A",
                            Suit = "C"
                        },
                        new
                        {
                            CardId = 52,
                            Number = "A",
                            Suit = "S"
                        });
                });

            modelBuilder.Entity("PokerApi.Repositories.Entities.Hand", b =>
                {
                    b.Property<int>("HandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("hand_id");

                    b.Property<int>("CardId1")
                        .HasColumnType("INTEGER")
                        .HasColumnName("card_id_1");

                    b.Property<int>("CardId2")
                        .HasColumnType("INTEGER")
                        .HasColumnName("card_id_2");

                    b.Property<int>("CardId3")
                        .HasColumnType("INTEGER")
                        .HasColumnName("card_id_3");

                    b.Property<int>("CardId4")
                        .HasColumnType("INTEGER")
                        .HasColumnName("card_id_4");

                    b.Property<int>("CardId5")
                        .HasColumnType("INTEGER")
                        .HasColumnName("card_id_5");

                    b.Property<string>("PlayerUserId")
                        .HasColumnType("TEXT")
                        .HasColumnName("player_user_id");

                    b.HasKey("HandId");

                    b.ToTable("card_hand");
                });

            modelBuilder.Entity("PokerApi.Repositories.Entities.Player", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT")
                        .HasColumnName("player_user_id");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT")
                        .HasColumnName("player_user_name");

                    b.HasKey("UserId");

                    b.ToTable("player");
                });
#pragma warning restore 612, 618
        }
    }
}
