#  Guessing Game API

A simple and clean REST API where players bet points on guessing a number from 0–9. Correct guesses win 9x the points bet.

---

##  Features

- Player identification using `Player-Id` header.
- Simple betting logic with win/loss and balance tracking.
- Proper error handling and validation.
- Fully unit-tested core logic.
- Swagger UI support for interactive testing.

---

##  Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### Run the API

```bash
cd GuessingGame.API
dotnet run
```


By default, the API will be accessible at:

```bash
http://localhost:5000/swagger

```

*  Required Header

The API expects a GUID player identifier in the header:

```bash
Player-Id: 123e4567-e89b-12d3-a456-426614174000
```

 This header is required when placing bets using the /api/game/bet endpoint.


 Example Request/Response


Request

```bash
POST /api/game/bet
Headers:
  Player-Id: 123e4567-e89b-12d3-a456-426614174000

Body:
{
  "number": 3,
  "points": 100
}


```

Successful Response

```bash
{
  "account": 10900,
  "status": "won",
  "points": "+900"
}


```


### Running Tests

From the root of the test project:
```bash
cd ../GuessingGame.Tests
dotnet test

```

## Design Trade-offs


* Validation Split

    - Basic validation (e.g. number range) handled in BetRequestValidator.

    - Balance validation moved to service layer to throw a domain-specific InsufficientFundsException.

* No Database
Used in-memory dictionary for simplicity. Easy to swap for persistent storage if needed.

* No Docker
Deliberate choice to avoid overengineering. This keeps the focus on core logic and architecture.


## Future Improvements

* Docker support for deployment.

* Persistent player storage.

* Authentication layer.

* Integration tests.