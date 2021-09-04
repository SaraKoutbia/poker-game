# poker-game
An Console application that simulates the game of Poker between two players, and computes the score for each player.

## Getting Started

### 1st option - Cloning the repo
From poker-game
```
cd .\poker-game\
dotnet run
```
Or specify the environment using:
```
dotnet run  /p:ASPNETCORE_ENVIRONMENT=Development
or
dotnet run  /p:ASPNETCORE_ENVIRONMENT=Production
```


## Running the tests
From poker-game
```
cd .\poker-game.tests\
test run
```


### About the tests

The poker-game.tests includes:
#### Deck tests

#### Evaluator tests
Tests the nine possible outcomes of a poker-round.


## Limitations 
The score is computed based on the suit, the highest rank and the second highest rank. When all three are equal, the results are assumed to be equal and the score is not incremented.

## Built With
.NET Core 3.1 
