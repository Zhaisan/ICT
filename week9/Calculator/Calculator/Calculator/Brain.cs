using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    enum State
    {
        Zero,
        AccumulateDigits,
        ComputePending,
        Compute
    }
   
    delegate void DisplayMessage(string text);

    class Brain
    {
        DisplayMessage displayMessage;
        State currentState = State.Zero;
        string previousNumber = "";
        string currentNumber = "";
        string currentOperation = "";
        bool isPoint = true;
        public Brain(DisplayMessage displayMessageDelegate)
        {
            displayMessage = displayMessageDelegate;
        }

        string[] nonZeroDigit = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        string[] digit = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        string[] zero = { "0" };
        string[] operation = { "+", "-", "*", "/", "^", "%"};
        string[] equal = { "=" };
        string[] point = { "," };

        public void ProcessSignal(string msg)
        {
            switch (currentState)
            {
                case State.Zero:
                    Zero(msg, false);
                    break;
                case State.AccumulateDigits:
                    AccumulateDigits(msg, false);
                    break;
                case State.ComputePending:
                    Operation(msg, false);
                    break;
                case State.Compute:
                    Result(msg, false);
                    break;
                default:
                    break;
            }
        }
        void Zero(string msg, bool income)
        {
            if (income)
            {
                previousNumber = "0";
                currentState = State.Zero;
            }
            else
            {
                if (nonZeroDigit.Contains(msg))
                {
                    AccumulateDigits(msg, true);
                }
            }
        }
        void AccumulateDigits(string msg , bool income)
        {
            if (income)
            {
                currentState = State.AccumulateDigits;
                previousNumber += msg;
                displayMessage(previousNumber);
            }
            else
            {
                if (digit.Contains(msg))
                {
                    AccumulateDigits(msg, true);
                }
                else if (operation.Contains(msg))
                {
                    Operation(msg, true);
                }
                else if (equal.Contains(msg))
                {
                    Result(msg, true);
                }
                else if (point.Contains(msg) && isPoint)
                {
                    isPoint = false;
                    AccumulateDigits(msg, true);
                }
            }
        }
        void Operation(string msg , bool income)
        {
            if (income)
            {
                currentState = State.ComputePending;
                if (currentOperation.Length != 0)
                {
                    PerformCalculation();
                    displayMessage(currentNumber);
                }
                if (currentNumber == "")
                {
                    currentNumber = previousNumber;
                }
                isPoint = true;
                currentOperation = msg;
                previousNumber = "";
            }
            else
            {
                if (digit.Contains(msg))
                {
                    AccumulateDigits(msg, true);
                }
                else if (operation.Contains(msg))
                {
                    currentOperation = "";
                    Operation(msg, true);
                }
            }
        }

        void Result(string msg, bool income)
        {
            if (income)
            {
                isPoint = true;
                currentState = State.Compute;
                PerformCalculation();
                currentOperation = "";
                displayMessage(currentNumber);
            }
            else
            {
                if (operation.Contains(msg))
                {
                    Operation(msg, true);
                }
                else if (nonZeroDigit.Contains(msg))
                {
                    previousNumber = "";
                    currentNumber = "";
                    displayMessage(previousNumber);
                    AccumulateDigits(msg, true);
                }
                else if (equal.Contains(msg))
                {
                    Result(msg, true);
                }
            }
        }
        void PerformCalculation()
        {
            double a = double.Parse(currentNumber);
            double b = double.Parse(previousNumber);
            if (currentOperation == "+")
            {
                currentNumber = (a + b).ToString();
            }
            else if (currentOperation == "-")
            {
                currentNumber = (a - b).ToString();
            }
            else if (currentOperation == "*")
            {
                currentNumber = (a * b).ToString();
            }
            else if (currentOperation == "/")
            {
                currentNumber = (a / b).ToString();
            }
            else if (currentOperation == "^")
            {
                currentNumber = (Math.Pow(a, b)).ToString();
            }
            else if (currentOperation == "%")
            {
                currentNumber = (a * b / 100).ToString();
            }
        }
        
    }
}
