# EloCalculatorNET

ELO calculation library written in .NET Standard.

## How to use

Create and calculate match between two players

```csharp
// Create a player with 1200 rating and wins
EloPlayer player1 = new EloPlayer(1200, true);
// Create a player with 1300 rating and loses
EloPlayer player2 = new EloPlayer(1300, false);
EloMatch match = new EloMatch(player1, player2);
EloResult result = match.Calculate();

// EloResult contains the result for each player
EloIndividiualResult player1Result = result.GetResult(player1.Identifier);
EloIndividiualResult player2Result = result.GetResult(player2.Identifier);

// You can also get the rating difference only
int player1EloDiff = result.GetRatingDifference(player1.Identifier);
```

Create a match between three players

```csharp
// Create a player with 1300 rating and places 1st
EloPlayer player1 = new EloPlayer(1300, 1);
// Create a player with 1200 rating and places 2nd
EloPlayer player2 = new EloPlayer(1200, 2);
// Create a player with 1350 rating and places 3rd
EloPlayer player3 = new EloPlayer(1350, 3);
EloMatch match = new EloMatch(player1, player2, player3);
```

You can also create match and add players dynamically

```csharp
EloMatch match = new EloMatch();
match.AddPlayer(new EloPlayer(1300, 1));
match.AddPlayer(new EloPlayer(1320, 2));
```