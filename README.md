# EloCalculatorNET

ELO calculation library written in .NET Standard.

## How to use

### Two player scenario
Create and calculate match between two players
* Player 1 has 1200 rating and wins
* Player 2 has 1320 rating and loses
* Get the ELO rating difference for each player

```csharp
var match = new EloMatch();
var player1Identifier = match.AddPlayer(1200, true);
var player2Identifier = match.AddPlayer(1320, false);
var result = match.Calculate();

int player1EloDifference = result.GetRatingDifference(player1Identifier);
int player2EloDifference = result.GetRatingDifference(player2Identifier);
```
### Three player scenario
Create a match between three players
* Player 1 has 1280 rating and places 1st
* Player 2 has 1300 rating and places 2nd
* Player 3 has 1220 rating and places 3rd
* Get the new ELO rating for each player

```csharp
var match = new EloMatch();
var player1Identifier = match.AddPlayer(1280, 1);
var player2Identifier = match.AddPlayer(1300, 2);
var player3Identifier = match.AddPlayer(1220, 3);
var result = match.Calculate();

int player1NewRating = result.GetRatingAfter(player1Identifier);
int player2NewRating = result.GetRatingAfter(player2Identifier);
int player3NewRating = result.GetRatingAfter(player3Identifier);
```
The calculations is based on:
- Player 1 won Player 2 and Player 3
- Player 2 won Player 3 and lost to Player 1
- Player 3 lost to Player 1 and Player 2

### Ten player scenario
* All players have 1200 ELO
* Only one player wins (the first one)
* Write out rating before and after for every player

```csharp
var teams = new List<EloTeam>();

for (var i = 0; i < 10; i++)
    teams.Add(new EloTeam(1200, i == 0));

var result = new EloMatch(teams).Calculate();

foreach (var ir in result.GetResults())
    Console.WriteLine($"{ir.PlayerIdentifier}, rating before: {ir.RatingBefore}, rating after: {ir.RatingAfter}");
```
This is calculated as if the first Player won all, and every other player drew.

### Two versus two scenario
* Team 1 consists of Player 1 with 1230 ELO and Player 2 with 1260 ELO
* Team 2 consists of Player 3 with 1120 ELO and Player 4 with 1410 ELO
* Team 1 wins Team 2
* Get results for each team and print out rating difference for each individual

```csharp
var match = new EloMatch();
var team1 = match.AddTeam(new EloTeam(true));
var team2 = match.AddTeam(new EloTeam(false));
var result = match.Calculate();

foreach (var ir in result.GetResults(team1))
    Console.WriteLine($"Team 1 - Player: {ir.PlayerIdentifier}, Rating difference: {ir.RatingDifference}");

foreach (var ir in result.GetResults(team2))
    Console.WriteLine($"Team 2 - Player: {ir.PlayerIdentifier}, Rating difference: {ir.RatingDifference}");
```

Each team has a rating which is an average of the team members. 
Team 1 has a rating of 1245
Team 2 has a rating of 1265.

### Get expected score between two ratings

Say you have two players, one with 1460 ELO and another with 1130 ELO, and want to know the likelihood of one winning another.

```csharp
var expectedScore = new EloRating(1460).ExpectedScoreAgainst(1130);
```

This returns a float number and in this specific scenario the value is 0.8698499 meaning the higher ranked player is estimated to win 87% of encounters.


