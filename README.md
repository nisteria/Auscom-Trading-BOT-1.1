# Auscom-Trading-BOT-1.1
cTrader BOT 

# AUSCOM Trading Bot

## Beschreibung

Der AUSCOM Trading Bot ist ein automatisiertes Handelssystem, das auf der cAlgo Plattform entwickelt wurde. Der Bot verwendet eine Kombination von technischen Indikatoren, um Handelsentscheidungen zu treffen, darunter Moving Averages, MACD, RSI, Stochastic Oscillator, CCI und Momentum Oscillator.

## Installation

Um den AUSCOM Trading Bot zu verwenden, müssen Sie ihn in Ihre cAlgo Plattform importieren. Folgen Sie dazu den Anweisungen in der cAlgo Dokumentation.

INFO LINK : https://help.ctrader.com/ctrader-automate/creating-and-running-a-cbot/#introduction

## BOT TESTEN und OPTIEMIEREN 

INFO LINK : https://help.ctrader.com/ctrader-automate/backtesting-and-optimizing-cbots/

## Konfiguration

Der Bot hat eine Reihe von Parametern, die Sie an Ihre Handelsstrategie anpassen können. Hier sind einige der wichtigsten Parameter:

- **BaseQuantity**: Die Basis-Handelsmenge, die der Bot für jeden Trade verwendet.
- **MaxSpreadInPips**: Der maximale Spread (in Pips), den der Bot akzeptiert, bevor er einen Trade ausführt.
- **MaxPositions**: Die maximale Anzahl von offenen Positionen, die der Bot gleichzeitig haben kann.
- **PeriodNextTrade**: Die Anzahl der Perioden, die der Bot zwischen den Trades wartet.

Darüber hinaus gibt es spezifische Parameter für Long- und Short-Trades, darunter die Perioden für die verschiedenen Indikatoren und die Levels für Stop-Loss, Take-Profit und Trailing Stop. Hier sind sie im Detail:

- **ShortMaFastPeriods und LongMaFastPeriods**: Die Anzahl der Perioden, die für die Berechnung des schnellen Moving Average für Short- und Long-Trades verwendet werden.
- **ShortMaSlowPeriods und LongMaSlowPeriods**: Die Anzahl der Perioden, die für die Berechnung des langsamen Moving Average für Short- und Long-Trades verwendet werden.
- **ShortMacdLongCycle, ShortMacdShortCycle, LongMacdLongCycle, LongMacdShortCycle**: Die Länge der langen und kurzen Zyklen, die für die Berechnung des MACD Histogramms für Short- und Long-Trades verwendet werden.
- **ShortRsiPeriods und LongRsiPeriods**: Die Anzahl der Perioden, die für die Berechnung des RSI für Short- und Long-Trades verwendet werden.
- **ShortStochasticPeriods, ShortStochasticKSlowPeriods, ShortStochasticDSlowPeriods, LongStochasticPeriods, LongStochasticKSlowPeriods, LongStochasticDSlowPeriods**: Die Anzahl der Perioden, die für die Berechnung des Stochastic Oscillators für Short- und Long-Trades verwendet werden.
- **ShortCciPeriods und LongCciPeriods**: Die Anzahl der Perioden, die für die Berechnung des CCI für Short- und Long-Trades verwendet werden.
- **ShortMomentumPeriods und LongMomentumPeriods**: Die Anzahl der Perioden, die für die Berechnung des Momentum Oscillators für Short- und Long-Trades verwendet werden.
- **ShortTakeProfitInPips, LongTakeProfitInPips**: Das Take-Profit-Level (in Pips) für Short- und Long-Trades.
- **ShortActivateTrailingStopAtProfitInPips, LongActivateTrailingStopAtProfitInPips**: Das Profit-Level (in Pips), bei dem der Bot einen Trailing Stop für Short- und Long-Trades aktiviert.
- **ShortTrailingStopInPips, LongTrailingStopInPips**: Die Größe des Trailing Stops (in Pips) für Short- und Long-Trades.
- **ShortUnconditionalStopLossInPips, LongUnconditionalStopLossInPips**: Das Stop-Loss-Level (in Pips) für Short- und Long-Trades.
- **ShortSLJaNein, LongtSLJaNein**: Diese Parameter bestimmen, ob der Bot einen ersten Stop-Loss für Short- und Long-Trades aktiviert.
- **ShortActivateSaveStopAtProfitInPips, LongActivateSaveStopAtProfitInPips**: Das Profit-Level (in Pips), bei dem der Bot einen Save Stop für Short- und Long-Trades aktiviert.
- **ShortSaveStopInPips, LongSaveStopInPips**: Die Größe des Save Stops (in Pips) für Short- und Long-Trades.

## Verwendung

Sobald der Bot konfiguriert ist, können Sie ihn starten und er wird automatisch Trades auf der Grundlage seiner Strategie ausführen. Der Bot überprüft die Handelsbedingungen bei jedem neuen Balken (oder Kerze) auf dem Chart und führt Trades aus, wenn die Bedingungen erfüllt sind. Darüber hinaus aktualisiert der Bot die Stop-Loss- und Take-Profit-Levels der offenen Positionen bei jedem neuen Tick (oder Preisänderung).

## Lizenz

Der AUSCOM Trading Bot ist lizenziert unter der Academic Free License. Weitere Informationen finden Sie in der Datei LICENSE.md.

## Haftungsausschluss



Der Handel auf Finanzmärkten birgt Risiken, einschließlich des Risikos des Verlusts von Geld. Verwenden Sie den AUSCOM Trading Bot auf eigenes Risiko. Die Autoren und Mitwirkenden übernehmen keine Haftung für Verluste, die durch die Verwendung des Bots entstehen.

## Unterstützung

Wenn Sie Fragen oder Probleme haben, senden Sie bitte eine WhatsApp +43 664 44 46 396.






Der Code initialisiert eine Reihe von technischen Indikatoren und führt dann Abfragen auf diesen Indikatoren aus, um zu entscheiden, ob ein Handel ausgeführt werden soll oder nicht. Hier sind die spezifischen Kriterien, die für jeden Indikator verwendet werden:

Moving Average (MA): Der Code initialisiert zwei Moving Averages für Long- und Short-Trades. Für Short-Trades wird geprüft, ob der schnellere MA (Moving Average) kleiner als der langsamere MA ist. Für Long-Trades wird geprüft, ob der schnellere MA größer als der langsamere MA ist.

MACD Histogram: Für Short-Trades wird geprüft, ob der Wert des MACD Histograms kleiner als 0 und der Wert des MACD Signals größer als 0 ist. Für Long-Trades wird geprüft, ob der Wert des MACD Histograms größer als 0 und der Wert des MACD Signals kleiner als 0 ist.

Relative Strength Index (RSI): Für Short-Trades wird geprüft, ob der RSI-Wert größer als 80 ist. Für Long-Trades wird geprüft, ob der RSI-Wert kleiner als 20 ist.

Stochastic Oscillator: Für Short-Trades wird geprüft, ob die Werte von %K und %D größer als 80 sind. Für Long-Trades wird geprüft, ob die Werte von %K und %D kleiner als 20 sind.

Commodity Channel Index (CCI): Für Short-Trades wird geprüft, ob der CCI-Wert größer als 100 ist. Für Long-Trades wird geprüft, ob der CCI-Wert kleiner als -100 ist.

Momentum Oscillator: Für Short-Trades wird geprüft, ob der Wert des Momentum Oscillators kleiner als 0 ist. Für Long-Trades wird geprüft, ob der Wert des Momentum Oscillators größer als 0 ist.

Die Variablen in diesem Code repräsentieren verschiedene technische Indikatoren und Parameter, die für die Handelsentscheidungen des Bots verwendet werden. Hier ist eine Beschreibung jeder Variable:

_shortMaFast und _longMaFast: Diese Variablen repräsentieren den schnellen Moving Average (MA) für Short- und Long-Trades. Der Moving Average ist ein technischer Indikator, der den durchschnittlichen Preis eines Wertpapiers über einen bestimmten Zeitraum darstellt. Ein "schneller" MA hat eine kürzere Periode und reagiert daher schneller auf Preisänderungen.

_shortMaSlow und _longMaSlow: Diese Variablen repräsentieren den langsamen Moving Average für Short- und Long-Trades. Ein "langsamer" MA hat eine längere Periode und reagiert daher langsamer auf Preisänderungen.

_shortMacd und _longMacd: Diese Variablen repräsentieren das Moving Average Convergence Divergence (MACD) Histogramm für Short- und Long-Trades. Das MACD Histogramm ist ein technischer Indikator, der den Unterschied zwischen zwei Moving Averages darstellt. Es wird oft verwendet, um Kauf- und Verkaufssignale zu generieren.

_shortRsi und _longRsi: Diese Variablen repräsentieren den Relative Strength Index (RSI) für Short- und Long-Trades. Der RSI ist ein technischer Indikator, der das Tempo der Preisänderungen misst. Er wird oft verwendet, um überkaufte und überverkaufte Bedingungen zu identifizieren.

_shortStochastic und _longStochastic: Diese Variablen repräsentieren den Stochastic Oscillator für Short- und Long-Trades. Der Stochastic Oscillator ist ein technischer Indikator, der das Verhältnis des aktuellen Preises zu einem Preishoch oder -tief über einen bestimmten Zeitraum darstellt. Er wird oft verwendet, um überkaufte und überverkaufte Bedingungen zu identifizieren.

_shortCci und _longCci: Diese Variablen repräsentieren den Commodity Channel Index (CCI) für Short- und Long-Trades. Der CCI ist ein technischer Indikator, der den Unterschied zwischen dem aktuellen Preis und dem durchschnittlichen Preis über einen bestimmten Zeitraum misst. Er wird oft verwendet, um überkaufte und überverkaufte Bedingungen zu identifiz

Die Parameter in diesem Code repräsentieren verschiedene Einstellungen und Werte, die für die Handelsentscheidungen des Bots verwendet werden. Hier ist eine Beschreibung einiger der Parameter:

BaseQuantity: Dieser Parameter bestimmt die Basis-Handelsmenge, die der Bot für jeden Trade verwendet.

ShortMaFastPeriods und LongMaFastPeriods: Diese Parameter bestimmen die Anzahl der Perioden, die für die Berechnung des schnellen Moving Average für Short- und Long-Trades verwendet werden.

ShortMaSlowPeriods und LongMaSlowPeriods: Diese Parameter bestimmen die Anzahl der Perioden, die für die Berechnung des langsamen Moving Average für Short- und Long-Trades verwendet werden.

ShortMacdLongCycle, ShortMacdShortCycle, LongMacdLongCycle, LongMacdShortCycle: Diese Parameter bestimmen die Länge der langen und kurzen Zyklen, die für die Berechnung des MACD Histogramms für Short- und Long-Trades verwendet werden.

ShortRsiPeriods und LongRsiPeriods: Diese Parameter bestimmen die Anzahl der Perioden, die für die Berechnung des RSI für Short- und Long-Trades verwendet werden.

ShortStochasticPeriods, ShortStochasticKSlowPeriods, ShortStochasticDSlowPeriods, LongStochasticPeriods, LongStochasticKSlowPeriods, LongStochasticDSlowPeriods: Diese Parameter bestimmen die Anzahl der Perioden, die für die Berechnung des Stochastic Oscillators für Short- und Long-Trades verwendet werden.

ShortCciPeriods und LongCciPeriods: Diese Parameter bestimmen die Anzahl der Perioden, die für die Berechnung des CCI für Short- und Long-Trades verwendet werden.

ShortMomentumPeriods und LongMomentumPeriods: Diese Parameter bestimmen die Anzahl der Perioden, die für die Berechnung des Momentum Oscillators für Short- und Long-Trades verwendet werden.

MaxSpreadInPips: Dieser Parameter bestimmt den maximalen Spread (in Pips), den der Bot akzeptiert, bevor er einen Trade ausführt.

MaxPositions: Dieser Parameter bestimmt die maximale Anzahl von offenen Positionen, die der Bot gleichzeitig haben kann.

PeriodNextTrade: Dieser Parameter bestimmt die Anzahl der Perioden, die der Bot zwischen den Trades wartet.

ShortTakeProfitInPips, LongTakeProfitInPips: Diese Parameter bestimmen das Take-Profit-Level (in Pips) für Short- und Long-Trades.

ShortActivateTrailingStopAtProfitInPips, LongActivateTrailingStopAtProfitInPips: Diese Parameter bestimmen das Profit-Level (in Pips), bei dem der Bot einen Trailing Stop für Short- und Long-Trades aktiviert.

ShortTrailingStopInPips, LongTrailingStopInPips: Diese Parameter bestimmen die Größe des Trailing Stops (in Pips) für Short- und Long-Trades.

ShortUnconditionalStopLossInPips, LongUnconditionalStopLossInPips: Diese Parameter bestimmen das Stop-Loss-Level (in Pips) für Short- und Long-Trades.

ShortSLJaNein, LongtSLJaNein: Diese Parameter bestimmen, ob der Bot einen ersten Stop-Loss für Short- und Long-Trades aktiviert.

ShortActivateSaveStopAtProfitInPips, LongActivateSaveStopAtProfitInPips: Diese Parameter bestimmen das Profit-Level (in Pips), bei dem der Bot einen Save Stop für Short- und Long-Trades aktiviert.

ShortSaveStopInPips, LongSaveStopInPips: Diese Parameter bestimmen die Größe des Save Stops (in Pips) für Short- und Long-Trades.
