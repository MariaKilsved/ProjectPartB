# Project Part B
## Objective
You will start working on _Project Part B_ individually or together in group. Assignment B1 has
1 part to be completed for G-Level and the Assignment B2 has 1 part to be completed for
VG-Level. You may decide to do part of the work in the group and complete it individually. In
any case the project part is individual and should be send in individually. You upload your
solutions zip format on LearnPoint, so I can easily un-compress them and open in Visual
Studio 2019 and run the code. _Pls see deadline in “Kursplaneringen”._

The objective is to use the skills learned in Object Oriented Programming to simulate a game
of cards.

For _both assignments_ I have provided some simple template in data structure and code for
you to not get stuck on trivial matters as well as some hints.

_Grade criterias_ for this part of the project:
Godkänt (G) : Completion of Assignment B1
Väl godkänt (VG): Completion of Assignment B2

## Project Part A: Assignment B1
Use the Console Solution template I made called ProjectPartB available on LearnPoint in a
zip file.

### Assignment B1
That implements the interfaces IDeckOfCards, IHandOfCards, IPlayingCard,
IComparable<PlayingCard>
- Creates a fresh deck of cards with 52 cards, Sort the Deck, and Shuffles the deck.
- Printed out fresh, sorted and shuffled deck with 13 cards per line. Each card in short
format as seen in screenshots below.
- Plays a game of highest card with two players.
    - Asks user how many cards to get (1-5)
    - Asks user how many rounds to play (1-5)
    - For each round print out
    - Round Nr
        - How many cards given to each player in the round
        - How many cards remains in the deck after each round
        - Player1 hand, lowest and highest card in the hand
        - Player2 hand, lowest and highest card in the hand
        - Winner having the highest card or if both cards have same value, a tie.
        
### Error handling
The program needs to have error handling that indicate if wrong input is entered and then
ask the user to re-enter. When the user enters Q or q the program should quit, and no game
should be played.

When wrong value or non-integer value entered user should be informed and asked for new
input.

Finally, the user types q and the program terminate without playing the game  

## Project Part A: Assignment B2
Modify and extend the code from Assignment B1 to create a program that determines a
poker hand’s Rank according to [https://www.poker.org/poker-hands-ranking-chart/](https://www.poker.org/poker-hands-ranking-chart/)

The program should:
- That implements the interfaces IDeckOfCards, IHandOfCards, IPlayingCard,
IPokerhand, IComparable<PlayingCard>
- Creates a fresh deck of cards with 52 cards, Sort the Deck, and Shuffles the deck.
- Continue to give Poker hands from a deck of cards for as long as there are more than
5 cards in the deck
- For each Poker hand given printout
    - Poker hand
    - Poker Rank and the highest card in the Rank
    - If the Rank is TwoPairs The highest card in each pair should be presented and
highest card in the rank is the highest card from the two pairs
    - How many cards remains in the deck

To test the Rank determination, you should deal the poker hands from a fresh deck and from
a sorted deck.
