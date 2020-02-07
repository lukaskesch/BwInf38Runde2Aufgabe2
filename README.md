# BwInf38Runde2Aufgabe2
Aufgabe 2: Geburtstag (38. Bundeswettbewerb Informatik 2. Runde)

2020 ist ein großes Jahr: Der Bundeswettbewerb Informatik feiert seinen 40. Geburtstag! Der
Biber will das vorbereiten und die Jahreszahl möglichst elegant darstellen. Er hat sich ausgedacht, einen Term auszuknobeln, dessen Wert diese Zahl ist. Damit es aber wirklich eine
Knobelei wird, soll der Term folgende Bedingungen erfüllen:

• Er verwendet nur eine vorgegebene Ziffer von 1 bis 9. Aus dieser Ziffer können auch
mehrstellige Zahlen gebildet werden. Ist 4 die Ziffer, sind also auch 44, 444, 4444 und so
weiter erlaubt.

• Er darf die Grundrechenarten +, -, *, / enthalten.

• Er darf beliebige Klammern enthalten, wenn sie korrekt gesetzt sind.

• Der Term muss mit so wenigen Ziffern wie möglich auskommen.


Der Biber hat das für das aktuelle Jahr 2019 schon einmal für jede Ziffer ausgeknobelt. Er
kommt zum Beispiel auf:

(((1+1)*((11111-1)/11))-1)

((2*((22*(2+(2*22)))-2))-(2/2))

(3+((3+3)*(3+333)))

(4+(((4+4)*((4*(4*(4*4)))-4))-(4/4)))

((5*(5+(5*((5*5)+55))))-(5+(5/5)))

((6+(6*(6+666)))/((6+6)/6))

(((77-7)/7)-(7*(7-(7*((7*7)-7)))))

(88+((88/8)-((8+8)*(8-(8*(8+8))))))

(9+((9+(99+((9+9)*999)))/9))


Aufgabe:

a) Schreibe ein Programm, das zu einer eingegebenen natürlichen Zahl und einer Ziffer
zwischen 1 und 9 einen Term ausgibt, der den genannten Bedingungen des Bibers genügt.
Welche Terme findet dein Programm für das Jahr 2020 und die ebenfalls interessanten
Jubiläumsjahre 2030, 2080 und 2980?

b) Wenn man zusätzlich zu den Grundrechenarten die Verwendung von Potenz- und der
Fakultätsfunktion erlaubt, kann man in einigen Fällen mit noch weniger Ziffern auskommen. Zum Beispiel ist 2019 = (((3!)!)+(3+((3!)*((3!)^3)))). Während der
Term ohne Fakultät und Potenzen siebenmal die Ziffer 3 verwendet, kommt dieser Term
mit nur fünf Dreien aus.
Erweitere dein Programm so, dass es versucht, für die gegebene Zahl einen so erweiterten
Term zu finden, der noch weniger Ziffern als die Lösung in (a) enthält. Beschreibe deine
dabei verwendete Strategie.
