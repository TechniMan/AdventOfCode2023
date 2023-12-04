using System.Drawing;
using System.Text.RegularExpressions;

namespace AdventOfCode2023;

class Day03 : ISolution {
  const int GRID_WIDTH = 140;
  const int GRID_HEIGHT = 140;

  public static void Solve() {
    var input = GetInput("03");

    // find all potential part numbers
    var partNumbers = new Dictionary<Rectangle, int>();
    foreach (var line in input) {
      var y = input.IndexOf(line);
      var matches = Regex.EnumerateMatches(line, "\\d+");
      foreach (var match in matches) {
        // use surrounding rectangle to find symbols
        var x = match.Index;
        var w = match.Length;
        var surroundingRect = new Rectangle(x - 1, y - 1, w + 2, 3);
        #region adjust rect for borders
        if (surroundingRect.X < 0) {
          surroundingRect.X = 0;
          surroundingRect.Width -= 1;
        } else if (surroundingRect.Right > GRID_WIDTH) {
          surroundingRect.Width -= 1;
        }
        if (surroundingRect.Y < 0) {
          surroundingRect.Y = 0;
          surroundingRect.Height -= 1;
        } else if (surroundingRect.Bottom > GRID_HEIGHT) {
          surroundingRect.Height -= 1;
        }
        #endregion
        // find any symbols around the number
        var isValid = false;
        for (int yy = surroundingRect.Top; yy < surroundingRect.Bottom; ++yy) {
          for (int xx = surroundingRect.Left; xx < surroundingRect.Right; ++xx) {
            if (Regex.IsMatch(input[yy][xx].ToString(), "[^.\\d]")) {
              isValid = true;
              break;
            }
          }
          if (isValid) break;
        }

        // if not valid, don't add it
        if (!isValid) continue;

        // determine the part number value and add to answer
        var value = 0;

        for (var i = x; i < x + match.Length; ++i) {
          var j = int.Parse(line[i].ToString());
          value = value * 10 + j;
        }

        partNumbers.Add(new Rectangle(x, y, w, 1), value);
      }
    }

    // then determine if they have a symbol 'adjacent' to them
    var answer1 = partNumbers.Values.Sum();

    Console.WriteLine($"Part 1: {answer1}");
  }
}