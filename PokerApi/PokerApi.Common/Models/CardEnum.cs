namespace PokerApi.Common.Models
{
    /// <summary>
    /// Describes a card i.e. its suit of Heart=H, Diamond=D, Club=C and Spades=S followed by 
    /// card number 2 thru 10, Jack=J, Queen=Q, King=K and Ace=A
    /// </summary>
    public enum CardEnum
    {
        H2 = 1,     D2 = 2,     C2 = 3,     S2 = 4,
        H3 = 5,     D3 = 6,     C3 = 7,     S3 = 8,
        H4 = 9,     D4 = 10,    C4 = 11,    S4 = 12,
        H5 = 13,    D5 = 14,    C5 = 15,    S5 = 16,
        H6 = 17,    D6 = 18,    C6 = 19,    S6 = 20,
        H7 = 21,    D7 = 22,    C7 = 23,    S7 = 24,
        H8 = 25,    D8 = 26,    C8 = 27,    S8 = 28,
        H9 = 29,    D9 = 30,    C9 = 31,    S9 = 32,
        H10 = 33,   D10 = 34,   C10 = 35,   S10 = 36,
        HJ = 37,    DJ = 38,    CJ = 39,    SJ = 40,
        HQ = 41,    DQ = 42,    CQ = 43,    SQ = 44,
        HK = 45,    DK = 46,    CK = 47,    SK = 48,
        HA = 49,    DA = 50,    CA = 51,    SA = 52,
    }
}
