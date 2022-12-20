# pgnToScores
Tool to convert scores into curves<p>

command : pgnToScores.exe path_to_your_pgn_file.pgn<p>

# most common scenario
1°) after a gauntlet tourney, we get something like this :<br>
![gauntlet](https://github.com/chris13300/pgnToScores/blob/main/pgnToScores/bin/Debug/gauntlet.jpg)<br>
![pgn](https://github.com/chris13300/pgnToScores/blob/main/pgnToScores/bin/Debug/pgn.jpg)<p>

2°) if we run the above command with "eman experience" as reference player and "10" as score group size, we get something like this :<br>
![scores](https://github.com/chris13300/pgnToScores/blob/main/pgnToScores/bin/Debug/scores.jpg)<p>

3°) copy-paste the content of the [scores.txt](https://github.com/chris13300/pgnToScores/blob/main/pgnToScores/modMain.vb#L105) file into your spreadsheet<p>

4°) create your chart :
![curves](https://github.com/chris13300/pgnToScores/blob/main/pgnToScores/bin/Debug/curves.jpg)<p>
  
# tips
- i advise you to accumulate about 10 scores in order to smooth the curves.<br>
- you will find a spreadsheet/chart example [here](https://github.com/chris13300/pgnToScores/blob/main/pgnToScores/bin/Debug/curves.ods).<br>
- the scores are separated by a [semicolon](https://github.com/chris13300/pgnToScores/blob/main/pgnToScores/modMain.vb#L91) in order to import them like a CSV file
