/**
 * Copyright since 2023 AUSCOM (LECH) and Contributors
 * AUSCOM is an International Registered Trademark 
 *
 * NOTICE OF LICENSE
 *
 * This source file is subject to the Academic Free License 
 * that is bundled with this package in the file LICENSE.md.
 * It is also available through the world-wide-web at this URL:
 * 
 * If you did not receive a copy of the license and are unable to
 * obtain it through the world-wide-web, please send an email
 * to office@auscom.at so we can send you a copy immediately.
 *
 * DISCLAIMER
 
 *
 * @author    Lech Downarowicz
 * @copyright Since 2023 Auscom GmbH
 * @license   Academic Free License 
 */


using System;
using cAlgo.API;
using cAlgo.API.Indicators;
using cAlgo.API.Internals;

namespace cAlgo.Robots
{
    [Robot(TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class SamplecBot : Robot
    {

        public string BotName = "AUSCOM BOT";
        public string BotVersion = "1.20.1";

        // Grundlegende Parameter

        [Parameter("1-Base Quantity (Lots)", Group = "Volume", DefaultValue = 0.5, MinValue = 0.01)] //BaseQuantity: Dieser Parameter bestimmt die Basis-Handelsmenge, die der Bot für jeden Trade verwendet.
        public double BaseQuantity { get; set; }
        //
        [Parameter("2-Label Name", Group = "Volume", DefaultValue = "My Order")]
        public string LabelName { get; set; }

        //Short Aktivieren
        [Parameter("Short Aktivieren", Group = "Volume", DefaultValue = true)]
        public bool ActivateShort { get; set; }

        //Long Aktivieren
        [Parameter("Long Aktivieren", Group = "Volume", DefaultValue = true)]
        public bool ActivateLong { get; set; }

        // Short- TP /SL / TSL    -----------------------------------------------------------------------------------------------------------------------------------

        [Parameter("S1--Take Profit (Pips)", Group = "Short Value TP/SL/TSL", DefaultValue = 90)]
        public int ShortTakeProfitInPips { get; set; } //ShortTakeProfitInPips, LongTakeProfitInPips: Diese Parameter bestimmen das Take-Profit-Level (in Pips) für Short- und Long-Trades.

        [Parameter("S2-Trailing Stop at Profit (Pips)", Group = "Short Value TP/SL/TSL", DefaultValue = 15)]
        public int ShortActivateTrailingStopAtProfitInPips { get; set; } //ShortActivateTrailingStopAtProfitInPips, LongActivateTrailingStopAtProfitInPips: Diese Parameter bestimmen das Profit-Level (in Pips), bei dem der Bot einen Trailing Stop für Short- und Long-Trades aktiviert.

        [Parameter("S3-Trailing Stop (Pips)", Group = "Short Value TP/SL/TSL", DefaultValue = 15)]
        public int ShortTrailingStopInPips { get; set; } //ShortTrailingStopInPips, LongTrailingStopInPips: Diese Parameter bestimmen die Größe des Trailing Stops (in Pips) für Short- und Long-Trades.

        [Parameter("S4-Stop Loss (Pips)", Group = "Short Value TP/SL/TSL", DefaultValue = 51)]
        public int ShortUnconditionalStopLossInPips { get; set; } //ShortUnconditionalStopLossInPips, LongUnconditionalStopLossInPips: Diese Parameter bestimmen das Stop-Loss-Level (in Pips) für Short- und Long-Trades.


        [Parameter("Activate First SL", Group = "Short SAVE SL", DefaultValue = true)]
        public bool ShortSLJaNein { get; set; } //ShortSLJaNein, LongtSLJaNein: Diese Parameter bestimmen, ob der Bot einen ersten Stop-Loss für Short- und Long-Trades aktiviert.

        [Parameter("S5-Safe SL at Profit (Pips)", Group = "Short SAVE SL", DefaultValue = 15)]
        public int ShortActivateSaveStopAtProfitInPips { get; set; } //ShortActivateSaveStopAtProfitInPips, LongActivateSaveStopAtProfitInPips: Diese Parameter bestimmen das Profit-Level (in Pips), bei dem der Bot einen Save Stop für Short- und Long-Trades aktiviert.

        [Parameter("S-6Safe SL Stop (Pips)", Group = "Short SAVE SL", DefaultValue = 15)]
        public int ShortSaveStopInPips { get; set; } //ShortSaveStopInPips, LongSaveStopInPips: Diese Parameter bestimmen die Größe des Save Stops (in Pips) für Short- und Long-Trades.


        // Long- TP /SL / TSL     -----------------------------------------------------------------------------------------------------------------------------------


        [Parameter("L1-Take Profit (Pips)", Group = "Long Value TP/SL/TSL", DefaultValue = 68)]
        public int LongTakeProfitInPips { get; set; } //ShortTakeProfitInPips, LongTakeProfitInPips: Diese Parameter bestimmen das Take-Profit-Level (in Pips) für Short- und Long-Trades.

        [Parameter("L2-Trailing Stop at Profit (Pips)", Group = "Long Value TP/SL/TSL", DefaultValue = 45)]
        public int LongActivateTrailingStopAtProfitInPips { get; set; } //ShortActivateTrailingStopAtProfitInPips, LongActivateTrailingStopAtProfitInPips: Diese Parameter bestimmen das Profit-Level (in Pips), bei dem der Bot einen Trailing Stop für Short- und Long-Trades aktiviert.

        [Parameter("L3-Trailing Stop (Pips)", Group = "Long Value TP/SL/TSL", DefaultValue = 15)]
        public int LongTrailingStopInPips { get; set; } //ShortTrailingStopInPips, LongTrailingStopInPips: Diese Parameter bestimmen die Größe des Trailing Stops (in Pips) für Short- und Long-Trades.

        [Parameter("L4-Stop Loss (Pips)", Group = "Long Value TP/SL/TSL", DefaultValue = 47)]
        public int LongUnconditionalStopLossInPips { get; set; } //ShortUnconditionalStopLossInPips, LongUnconditionalStopLossInPips: Diese Parameter bestimmen das Stop-Loss-Level (in Pips) für Short- und Long-Trades.

        [Parameter("Activate First SL", Group = "Long SAVE SL", DefaultValue = true)]
        public bool LongtSLJaNein { get; set; } //ShortSLJaNein, LongtSLJaNein: Diese Parameter bestimmen, ob der Bot einen ersten Stop-Loss für Short- und Long-Trades aktiviert.

        [Parameter("L5-Save SL at Profit (Pips)", Group = "Long SAVE SL", DefaultValue = 7)]
        public int LongActivateSaveStopAtProfitInPips { get; set; } //ShortActivateSaveStopAtProfitInPips, LongActivateSaveStopAtProfitInPips: Diese Parameter bestimmen das Profit-Level (in Pips), bei dem der Bot einen Save Stop für Short- und Long-Trades aktiviert.

        [Parameter("L6-Save Stop (Pips)", Group = "Long SAVE SL", DefaultValue = 3)]
        public int LongSaveStopInPips { get; set; } //ShortSaveStopInPips, LongSaveStopInPips: Diese Parameter bestimmen die Größe des Save Stops (in Pips) für Short- und Long-Trades.


        // SHORT Trading Paramter

        [Parameter("MA Fast Periods", Group = "Short Value", DefaultValue = -1)]
        public int ShortMaFastPeriods { get; set; } //ShortMaFastPeriods und LongMaFastPeriods: Diese Parameter bestimmen die Anzahl der Perioden, die für die Berechnung des schnellen Moving Average für Short- und Long-Trades verwendet werden.

        [Parameter("MA Slow Periods", Group = "Short Value", DefaultValue = 18)]
        public int ShortMaSlowPeriods { get; set; } //ShortMaSlowPeriods und LongMaSlowPeriods: Diese Parameter bestimmen die Anzahl der Perioden, die für die Berechnung des langsamen Moving Average für Short- und Long-Trades verwendet werden.

        [Parameter("MACD Long Cycle", Group = "Short Value", DefaultValue = 7)]
        public int ShortMacdLongCycle { get; set; } //ShortMacdLongCycle, ShortMacdShortCycle, LongMacdLongCycle, LongMacdShortCycle: Diese Parameter bestimmen die Länge der langen und kurzen Zyklen, die für die Berechnung des MACD Histogramms für Short- und Long-Trades verwendet werden.

        [Parameter("MACD Short Cycle", Group = "Short Value", DefaultValue = 5)]
        public int ShortMacdShortCycle { get; set; }

        [Parameter("MACD Signal Periods", Group = "Short Value", DefaultValue = 9)]
        public int ShortMacdSignalPeriods { get; set; }

        [Parameter("RSI Periods", Group = "Short Value", DefaultValue = 16)]
        public int ShortRsiPeriods { get; set; } //ShortRsiPeriods und LongRsiPeriods: Diese Parameter bestimmen die Anzahl der Perioden, die für die Berechnung des RSI für Short- und Long-Trades verwendet werden.
        //ShortStochasticPeriods, ShortStochasticKSlowPeriods, ShortStochasticDSlowPeriods, LongStochasticPeriods, LongStochasticKSlowPeriods, LongStochasticDSlowPeriods: Diese Parameter bestimmen die Anzahl der Perioden, die für die Berechnung des Stochastic Oscillators für Short- und Long-Trades verwendet werden.
        [Parameter("Stochastic Periods", Group = "Short Value", DefaultValue = 13)]
        public int ShortStochasticPeriods { get; set; }

        [Parameter("Stochastic K Slow Periods", Group = "Short Value", DefaultValue = 9)]
        public int ShortStochasticKSlowPeriods { get; set; }

        [Parameter("Stochastic D Slow Periods", Group = "Short Value", DefaultValue = 5)]
        public int ShortStochasticDSlowPeriods { get; set; }

        [Parameter("CCI Periods", Group = "Short Value", DefaultValue = 22)]
        public int ShortCciPeriods { get; set; } //ShortCciPeriods und LongCciPeriods: Diese Parameter bestimmen die Anzahl der Perioden, die für die Berechnung des CCI für Short- und Long-Trades verwendet werden.

        [Parameter("Momentum Periods", Group = "Short Value", DefaultValue = 15)]
        public int ShortMomentumPeriods { get; set; } //ShortMomentumPeriods und LongMomentumPeriods: Diese Parameter bestimmen die Anzahl der Perioden, die für die Berechnung des Momentum Oscillators für Short- und Long-Trades verwendet werden.

        // Long-Parameter   .

        [Parameter("MA Fast Periods", Group = "Long Value", DefaultValue = 9)]
        public int LongMaFastPeriods { get; set; } //ShortMaFastPeriods und LongMaFastPeriods: Diese Parameter bestimmen die Anzahl der Perioden, die für die Berechnung des schnellen Moving Average für Short- und Long-Trades verwendet werden.

        [Parameter("MA Slow Periods", Group = "Long Value", DefaultValue = 21)]
        public int LongMaSlowPeriods { get; set; } //ShortMaSlowPeriods und LongMaSlowPeriods: Diese Parameter bestimmen die Anzahl der Perioden, die für die Berechnung des langsamen Moving Average für Short- und Long-Trades verwendet werden.

        [Parameter("MACD Long Cycle", Group = "Long Value", DefaultValue = 26)]
        public int LongMacdLongCycle { get; set; } //ShortMacdLongCycle, ShortMacdShortCycle, LongMacdLongCycle, LongMacdShortCycle: Diese Parameter bestimmen die Länge der langen und kurzen Zyklen, die für die Berechnung des MACD Histogramms für Short- und Long-Trades verwendet werden.

        [Parameter("MACD Short Cycle", Group = "Long Value", DefaultValue = 12)]
        public int LongMacdShortCycle { get; set; }

        [Parameter("MACD Signal Periods", Group = "Long Value", DefaultValue = 9)]
        public int LongMacdSignalPeriods { get; set; }

        [Parameter("RSI Periods", Group = "Long Value", DefaultValue = 14)]
        public int LongRsiPeriods { get; set; } //ShortRsiPeriods und LongRsiPeriods: Diese Parameter bestimmen die Anzahl der Perioden, die für die Berechnung des RSI für Short- und Long-Trades verwendet werden.
        //ShortStochasticPeriods, ShortStochasticKSlowPeriods, ShortStochasticDSlowPeriods, LongStochasticPeriods, LongStochasticKSlowPeriods, LongStochasticDSlowPeriods: Diese Parameter bestimmen die Anzahl der Perioden, die für die Berechnung des Stochastic Oscillators für Short- und Long-Trades verwendet werden.
        [Parameter("Stochastic Periods", Group = "Long Value", DefaultValue = 14)]
        public int LongStochasticPeriods { get; set; }

        [Parameter("Stochastic K Slow Periods", Group = "Long Value", DefaultValue = 3)]
        public int LongStochasticKSlowPeriods { get; set; }

        [Parameter("Stochastic D Slow Periods", Group = "Long Value", DefaultValue = 3)]
        public int LongStochasticDSlowPeriods { get; set; }

        [Parameter("CCI Periods", Group = "Long Value", DefaultValue = 20)]
        public int LongCciPeriods { get; set; } //ShortCciPeriods und LongCciPeriods: Diese Parameter bestimmen die Anzahl der Perioden, die für die Berechnung des CCI für Short- und Long-Trades verwendet werden.

        [Parameter("Momentum Periods", Group = "Long Value", DefaultValue = 14)]
        public int LongMomentumPeriods { get; set; } //ShortMomentumPeriods und LongMomentumPeriods: Diese Parameter bestimmen die Anzahl der Perioden, die für die Berechnung des Momentum Oscillators für Short- und Long-Trades verwendet werden.

        // Gemeinsame Parameter

        /*
        [Parameter("Profit Increase Percentage (%)", Group = "==== COMMON ====", DefaultValue = 11.8)]
        public double ProfitIncreasePercentage { get; set; }
        */
        [Parameter("Max Spread (Pips)", Group = "==== COMMON ====", DefaultValue = 13)]
        public int MaxSpreadInPips { get; set; } //MaxSpreadInPips: Dieser Parameter bestimmt den maximalen Spread (in Pips), den der Bot akzeptiert, bevor er einen Trade ausführt.

        [Parameter("Max Positions", Group = "==== COMMON ====", DefaultValue = 8)]
        public int MaxPositions { get; set; } //MaxPositions: Dieser Parameter bestimmt die maximale Anzahl von offenen Positionen, die der Bot gleichzeitig haben kann.

        [Parameter("Period Next Trade", Group = "==== COMMON ====", DefaultValue = 4)]
        public int PeriodNextTrade { get; set; } //PeriodNextTrade: Dieser Parameter bestimmt die Anzahl der Perioden, die der Bot zwischen den Trades wartet.


        //  Variablen für Long & Short

        private double _currentQuantity;
        private int LastPositionOpenedOnBar;

        private MovingAverage _shortMaFast;
        private MovingAverage _shortMaSlow;
        private MacdHistogram _shortMacd;
        private RelativeStrengthIndex _shortRsi;
        private StochasticOscillator _shortStochastic;
        private CommodityChannelIndex _shortCci;
        private MomentumOscillator _shortMomentum;

        private MovingAverage _longMaFast;
        private MovingAverage _longMaSlow;
        private MacdHistogram _longMacd;
        private RelativeStrengthIndex _longRsi;
        private StochasticOscillator _longStochastic;
        private CommodityChannelIndex _longCci;
        private MomentumOscillator _longMomentum;

        protected override void OnStart()
        {
            // DEBUGGER  ON OFF 
            //   System.Diagnostics.Debugger.Launch();



            Print($"Bot Name: {BotName}");
            Print($"Bot Version: {BotVersion}");

            // Initialisierung der Indikatoren SHORT
            _shortMaFast = Indicators.MovingAverage(Bars.ClosePrices, ShortMaFastPeriods, MovingAverageType.Exponential); // Initialisiert den schnellen Moving Average für Short-Trades
            _shortMaSlow = Indicators.MovingAverage(Bars.ClosePrices, ShortMaSlowPeriods, MovingAverageType.Exponential); // Initialisiert den langsamen Moving Average für Short-Trades
            _shortMacd = Indicators.MacdHistogram(ShortMacdLongCycle, ShortMacdShortCycle, ShortMacdSignalPeriods); // Initialisiert das MACD Histogramm für Short-Trades
            _shortRsi = Indicators.RelativeStrengthIndex(Bars.ClosePrices, ShortRsiPeriods); // Initialisiert den RSI für Short-Trades
            _shortStochastic = Indicators.StochasticOscillator(ShortStochasticPeriods, ShortStochasticKSlowPeriods, ShortStochasticDSlowPeriods, MovingAverageType.Simple); // Initialisiert den Stochastic Oscillator für Short-Trades
            _shortCci = Indicators.CommodityChannelIndex(ShortCciPeriods); // Initialisiert den CCI für Short-Trades
            _shortMomentum = Indicators.MomentumOscillator(Bars.ClosePrices, ShortMomentumPeriods); // Initialisiert den Momentum Oscillator für Short-Trades
            // Initialisierung der Indikatoren LONG
            _longMaFast = Indicators.MovingAverage(Bars.ClosePrices, LongMaFastPeriods, MovingAverageType.Exponential); // Initialisiert den schnellen Moving Average für Long-Trades
            _longMaSlow = Indicators.MovingAverage(Bars.ClosePrices, LongMaSlowPeriods, MovingAverageType.Exponential); // Initialisiert den langsamen Moving Average für Long-Trades
            _longMacd = Indicators.MacdHistogram(LongMacdLongCycle, LongMacdShortCycle, LongMacdSignalPeriods); // Initialisiert das MACD Histogramm für Long-Trades
            _longRsi = Indicators.RelativeStrengthIndex(Bars.ClosePrices, LongRsiPeriods); // Initialisiert den RSI für Long-Trades
            _longStochastic = Indicators.StochasticOscillator(LongStochasticPeriods, LongStochasticKSlowPeriods, LongStochasticDSlowPeriods, MovingAverageType.Simple); // Initialisiert den Stochastic Oscillator für Long-Trades
            _longCci = Indicators.CommodityChannelIndex(LongCciPeriods); // Initialisiert den CCI für Long-Trades
            _longMomentum = Indicators.MomentumOscillator(Bars.ClosePrices, LongMomentumPeriods); // Initialisiert den Momentum Oscillator für Long-Trades

            _currentQuantity = BaseQuantity; // Setzt die aktuelle Handelsmenge auf die Basis-Handelsmenge

        }

        protected override void OnBar()
        {
            foreach (var position in Positions)
            {
                if (Symbol.Spread / Symbol.PipSize > MaxSpreadInPips) return; // Max Spread Kontrolle

                if (Positions.Count >= MaxPositions && position.SymbolName == SymbolName) return; // Max Position Kontrolle

                if (Bars.ClosePrices.Count - LastPositionOpenedOnBar <= PeriodNextTrade) return; // Wenn weniger als x Kerzen seit der letzten Positionseröffnung vergangen sind, tun Sie nichts
            }

            _currentQuantity = BaseQuantity;

            /*
            In diesem Code fließen alle Bedingungen mit einem bestimmten Gewicht in das Gesamtgewicht ein. 
            Wenn das Gesamtgewicht größer oder gleich 0.6 ist (was bedeutet, dass mindestens drei der sechs Bedingungen erfüllt sind, 
            wenn wir davon ausgehen, dass alle Bedingungen gleich wichtig sind), führt der Algorithmus den Short od Long Trade aus.
            */


            //Short Bedinungen 

            bool isShortTrend = _shortMaFast.Result.LastValue < _shortMaSlow.Result.LastValue;
            bool isShortMacdCondition = _shortMacd.Histogram.LastValue < 0 && _shortMacd.Signal.LastValue > 0;
            bool isShortRsiCondition = _shortRsi.Result.LastValue > 80;
            bool isShortStochasticCondition = _shortStochastic.PercentK.LastValue > 80 && _shortStochastic.PercentD.LastValue > 80;
            bool isShortCciCondition = _shortCci.Result.LastValue > 100;
            bool isShortMomentumCondition = _shortMomentum.Result.LastValue < 0;

            double totalWeightShort = 0.0;
            totalWeightShort += isShortTrend ? 0.2 : 0.0;
            totalWeightShort += isShortMacdCondition ? 0.2 : 0.0;
            totalWeightShort += isShortRsiCondition ? 0.2 : 0.0;
            totalWeightShort += isShortStochasticCondition ? 0.2 : 0.0;
            totalWeightShort += isShortCciCondition ? 0.1 : 0.0;
            totalWeightShort += isShortMomentumCondition ? 0.1 : 0.0;

            // Long Bedinungen 

            bool isLongTrend = _longMaFast.Result.LastValue > _longMaSlow.Result.LastValue;
            bool isLongMacdCondition = _longMacd.Histogram.LastValue > 0 && _longMacd.Signal.LastValue < 0;
            bool isLongRsiCondition = _longRsi.Result.LastValue < 20;
            bool isLongStochasticCondition = _longStochastic.PercentK.LastValue < 20 && _longStochastic.PercentD.LastValue < 20;
            bool isLongCciCondition = _longCci.Result.LastValue < -100;
            bool isLongMomentumCondition = _longMomentum.Result.LastValue > 0;

            double totalWeightLong = 0.0;
            totalWeightLong += isLongTrend ? 0.2 : 0.0;
            totalWeightLong += isLongMacdCondition ? 0.2 : 0.0;
            totalWeightLong += isLongRsiCondition ? 0.2 : 0.0;
            totalWeightLong += isLongStochasticCondition ? 0.2 : 0.0;
            totalWeightLong += isLongCciCondition ? 0.1 : 0.0;
            totalWeightLong += isLongMomentumCondition ? 0.1 : 0.0;

            if (totalWeightShort >= 0.6) // Überprüft, ob das Gesamtgewicht für Short-Trades größer oder gleich 0.6 ist
            {
                if (!ActivateShort) // Überprüft, ob Short-Trades aktiviert sind
                {
                    return; // Beendet die Methode, wenn Short-Trades nicht aktiviert sind
                }
                var tradeResult = ExecuteMarketOrder(TradeType.Sell, SymbolName, 1 * Symbol.LotSize * _currentQuantity, LabelName, ShortUnconditionalStopLossInPips, ShortTakeProfitInPips); // Führt einen Short-Trade aus, wenn die Bedingungen erfüllt sind
                if (tradeResult.IsSuccessful) // Überprüft, ob der Trade erfolgreich ausgeführt wurde
                {
                    tradeResult.Position.ModifyStopLossPrice(tradeResult.Position.EntryPrice + ShortUnconditionalStopLossInPips * Symbol.PipSize); // Modifiziert den Stop-Loss-Preis der Position, wenn der Trade erfolgreich war
                    LastPositionOpenedOnBar = Bars.ClosePrices.Count; // Aktualisiert die Position der letzten Positionseröffnung
                }
            }

            if (totalWeightLong >= 0.6) // Überprüft, ob das Gesamtgewicht für Long-Trades größer oder gleich 0.6 ist
            {
                if (!ActivateLong) // Überprüft, ob Long-Trades aktiviert sind
                {
                    return; // Beendet die Methode, wenn Long-Trades nicht aktiviert sind
                }
                var tradeResult = ExecuteMarketOrder(TradeType.Buy, SymbolName, 1 * Symbol.LotSize * _currentQuantity, LabelName, LongUnconditionalStopLossInPips, LongTakeProfitInPips); // Führt einen Long-Trade aus, wenn die Bedingungen erfüllt sind
                if (tradeResult.IsSuccessful) // Überprüft, ob der Trade erfolgreich ausgeführt wurde
                {
                    tradeResult.Position.ModifyStopLossPrice(tradeResult.Position.EntryPrice - LongUnconditionalStopLossInPips * Symbol.PipSize); // Modifiziert den Stop-Loss-Preis der Position, wenn der Trade erfolgreich war
                    LastPositionOpenedOnBar = Bars.ClosePrices.Count; // Aktualisiert die Position der letzten Positionseröffnung
                }
            }
        }
        protected override void OnTick()
        {
            foreach (var position in Positions)  // Geht durch jede offene Position
            {
                if (position.SymbolName != SymbolName) continue; // Überspringt die Position, wenn sie nicht dem aktuellen Symbol entspricht
                if (position.TradeType == TradeType.Buy)// Überprüft, ob die Position ein Kauf ist
                {

                    // Save SL Aktivieren 
                    if (LongtSLJaNein) // Überprüft, ob der Save Stop-Loss für Long-Trades aktiviert ist
                    {
                        if (position.Pips >= LongActivateSaveStopAtProfitInPips || position.Pips <= LongActivateSaveStopAtProfitInPips)
                        {
                            var newStopLossPrice = position.EntryPrice + (LongSaveStopInPips * Symbol.PipSize);
                            position.ModifyStopLossPrice(newStopLossPrice); // Ändert den Stop-Loss-Preis der Position
                        }
                    }
                    // Save SL Ende

                    if (position.Pips >= LongActivateTrailingStopAtProfitInPips && position.StopLoss == LongUnconditionalStopLossInPips || position.Pips == LongActivateSaveStopAtProfitInPips)
                    {
                        var newStopLossPrice = position.EntryPrice + (LongTrailingStopInPips * Symbol.PipSize);

                        // Setzen Sie den Stop-Loss das erste Mal
                        position.ModifyStopLossPrice(newStopLossPrice);
                    }
                    if (position.Pips >= LongActivateTrailingStopAtProfitInPips && position.StopLoss == null)
                    {
                        //    var newStopLossPrice = position.EntryPrice + (LongTrailingStopInPips * Symbol.PipSize);
                        var newStopLossPrice = Math.Max(position.EntryPrice + LongTrailingStopInPips * Symbol.PipSize, Symbol.Bid - LongTrailingStopInPips * Symbol.PipSize);

                        // Setzen Sie den Stop-Loss das erste Mal in Bezug auf den Kaufpreis
                        position.ModifyStopLossPrice(newStopLossPrice);
                    }
                    else if (position.StopLoss != null && Symbol.Ask > position.StopLoss && position.Pips >= LongActivateTrailingStopAtProfitInPips)
                    {
                        //  var newStopLossPrice = Symbol.Ask - (LongTrailingStopInPips * Symbol.PipSize);
                        var newStopLossPrice = Math.Max(position.EntryPrice + LongTrailingStopInPips * Symbol.PipSize, Symbol.Bid - LongTrailingStopInPips * Symbol.PipSize);

                        // Überprüfen Sie, ob der neue Stop-Loss-Preis höher ist
                        if (newStopLossPrice > position.StopLoss)
                        {
                            position.ModifyStopLossPrice(newStopLossPrice);
                        }
                    }
                }
                //************************************************************************* Short TP TSL**************************************************

                else if (position.TradeType == TradeType.Sell)
                {

                    //Save SL Aktivieren  Start 

                    if (ShortSLJaNein)
                    {
                        if (position.Pips >= ShortActivateSaveStopAtProfitInPips || position.StopLoss <= ShortUnconditionalStopLossInPips)
                        {

                            var newStopLossPrice = position.EntryPrice - (ShortSaveStopInPips * Symbol.PipSize);

                            position.ModifyStopLossPrice(newStopLossPrice);
                        }
                    }

                    //Save SL Aktivieren  Ende 

                    if (position.Pips >= ShortActivateTrailingStopAtProfitInPips && position.StopLoss == ShortUnconditionalStopLossInPips)
                    {
                        var newStopLossPrice = position.EntryPrice + (ShortTrailingStopInPips * Symbol.PipSize);

                        // Setzen Sie den Stop-Loss das erste Mal
                        position.ModifyStopLossPrice(newStopLossPrice);
                    }

                    if (position.Pips >= ShortActivateTrailingStopAtProfitInPips && position.StopLoss == null)
                    {
                        var newStopLossPrice = position.EntryPrice + (ShortTrailingStopInPips * Symbol.PipSize);

                        // Setzen Sie den Stop-Loss das erste Mal in Bezug auf den Verkaufspreis
                        position.ModifyStopLossPrice(newStopLossPrice);
                    }
                    else if (position.StopLoss != null && Symbol.Bid < position.StopLoss && position.Pips >= ShortActivateTrailingStopAtProfitInPips)
                    {
                        var newStopLossPrice = Symbol.Bid + (ShortTrailingStopInPips * Symbol.PipSize);

                        // Überprüfen Sie, ob der neue Stop-Loss-Preis niedriger ist
                        if (newStopLossPrice < position.StopLoss)
                        {
                            position.ModifyStopLossPrice(newStopLossPrice);
                        }
                    }
                }
            }
        }
    }
}