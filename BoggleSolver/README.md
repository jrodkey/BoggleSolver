# BoggleSolver

A high-performance Boggle game solver written in C# (.NET 8.0) that finds all valid words on a Boggle board using Trie data structure for efficient word lookup.

## Overview

BoggleSolver is a console application that solves Boggle puzzles of various sizes. It uses a Trie (prefix tree) data structure to efficiently search for valid words from a dictionary while traversing the game board using depth-first search.

## Features

- **Flexible Board Sizes**: Supports boards of any dimension (e.g., 3x3, 4x4, 5x5, and larger)
- **Efficient Word Lookup**: Uses Trie data structure for O(1) prefix checking
- **Dictionary Validation**: Filters words based on Boggle rules:
  - Minimum 3 letters
  - No capitalized words
  - No hyphenated words
  - No duplicate entries
- **Performance Metrics**: Displays initialization and solving times
- **Comprehensive Results**: Outputs all found words in sorted order

## Requirements

- .NET 8.0 SDK or later
- Windows, macOS, or Linux

## Installation

1. Clone or download the repository
2. Navigate to the project directory
3. Ensure `BoggleDictionary.txt` is present in the root directory

## Usage

### Building the Project

```bash
dotnet build
```

### Running the Solver

```bash
dotnet run
```

### Customizing Board Size and Letters

Edit [Program.cs](Program.cs) to modify the board configuration:

```csharp
// Solve a 3x3 board with specified letters
IEnumerable<string> results = boggle.SolveBoard(3, 3, "yoxrbaved");

// Solve a 4x4 board
IEnumerable<string> results = boggle.SolveBoard(4, 4, "baexbaagvedueuda");

// Solve a 5x5 board
IEnumerable<string> results = boggle.SolveBoard(5, 5, "braexrbaagvedqueudasimdie");
```

The letters string should contain `width * height` characters representing the board from left-to-right, top-to-bottom.

## How It Works

1. **Initialization Phase**:
   - Loads dictionary from `BoggleDictionary.txt`
   - Validates words according to Boggle rules
   - Builds a Trie structure for efficient prefix lookup

2. **Solving Phase**:
   - Creates game board from input string
   - Performs depth-first search (DFS) from each cell
   - Uses Trie to validate word prefixes during traversal
   - Tracks visited cells to prevent reuse in same word
   - Collects all valid words found

3. **Output**:
   - Sorts and displays all found words
   - Shows total word count
   - Reports performance metrics

## Example Output

```
Boggle Initializing Time: 00:00:01:23:
Boggle Solving Result: ============================
Words Found: 42
abed, bad, bade, bared, bare, bed,boa, board, boar, box, ...
Boggle Solving Time: 00:00:00:05:
```

## Algorithm Complexity

- **Initialization**: O(N × M) where N is number of words, M is average word length
- **Search**: O(R × C × 8^L) where R×C is board size, L is maximum word length
- **Space**: O(N × M) for Trie storage

## Dictionary File

The `BoggleDictionary.txt` file should contain one word per line. Words must:
- Be in lowercase
- Contain at least 3 letters
- Not contain hyphens or special characters
- Not be all uppercase (filtered out)

## Performance Notes

- Larger boards (10x10+) may take several seconds to solve
- Initialization time depends on dictionary size
- Trie structure provides significant speedup over dictionary lookup
