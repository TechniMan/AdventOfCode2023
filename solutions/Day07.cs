namespace AdventOfCode2023;

enum CardRank {
    Two = 2,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace
}

enum HandRank {
    HighCard,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    FullHouse,
    FourOfAKind,
    FiveOfAKind
}

class HandComparer : IComparer<string>
{
    private static CardRank RankForCard(char c) {
        if (int.TryParse(c.ToString(), out int i)) {
            return (CardRank)i;
        }
        return c switch
        {
            'T' => CardRank.Ten,
            'J' => CardRank.Jack,
            'Q' => CardRank.Queen,
            'K' => CardRank.King,
            _ => CardRank.Ace
        };
    }

    private static HandRank RankForHand(string hand) {
        // determine stats about hand content
        var cardCounts = new Dictionary<char, int>();
        foreach (var c in hand) {
            if (cardCounts.TryGetValue(c, out int value)) {
                cardCounts[c] = value + 1;
            }
            else {
                cardCounts.Add(c, 1);
            }
        }

        // order
        var orderedCardCounts = cardCounts.OrderBy(kv => kv.Value);

        // determine rank based on card counts
        // 5
        if (orderedCardCounts.First().Value == 5) {
            return HandRank.FiveOfAKind;
        }
        // 4,1
        if (orderedCardCounts.First().Value == 4) {
            return HandRank.FourOfAKind;
        }
        if (orderedCardCounts.First().Value == 3) {
            // 3,2
            if (orderedCardCounts.Last().Value == 2) {
                return HandRank.FullHouse;
            }
            // 3,1,1
            return HandRank.ThreeOfAKind;
        }
        if (orderedCardCounts.First().Value == 2) {
            // 2,2,1
            if (orderedCardCounts.ElementAt(1).Value == 2) {
                return HandRank.TwoPair;
            }
            // 2,1,1,1
            return HandRank.OnePair;
        }
        return HandRank.HighCard;
    }

    public int Compare(string? handA, string? handB)
    {
        if (handA == null || handB == null) throw new Exception("one of the given hands is null");

        var rankA = RankForHand(handA);
        var rankB = RankForHand(handB);

        // both same rank
        if (rankA == rankB) {
            // compare each card in hands
            for (var c = 0; c < 5; ++c) {
                var cardA = RankForCard(handA[c]);
                var cardB = RankForCard(handB[c]);
                if (cardA > cardB) {
                    return 1;
                }
                else if (cardA < cardB) {
                    return -1;
                }
            }
            // both hands exactly the same
            return 0;
        }
        // else return greater
        return rankA > rankB ? 1 : -1;
    }
}

class Day07: ISolution {
    public static void Solve() {
        var input = GetInput("07");

        // fill in hands and bids
        var handsAndBids = new Dictionary<string, int>();
        input.ForEach(i => {
            var split = i.Split(' ');
            handsAndBids.Add(split[0], int.Parse(split[1]));
        });

        // order hands, lowest rank first
        var comparer = new HandComparer();
        var orderedHandsAndBids = handsAndBids.OrderBy(kv => kv.Key, comparer);

        // multiply bid by hand's ranking-against-other-hands (from 1 to 1000)
        var answer1 = orderedHandsAndBids.Select((handAndBid, idx) => {
            return handAndBid.Value * (idx + 1);
        }).Sum();
        Console.WriteLine($"Part 1: {answer1}");
    }
}
