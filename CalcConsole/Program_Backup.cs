
using System;
using System.Collections.Generic;
using System.Text;

namespace CalcConsole
{
    class Program_Backup
    {
        static void Main(string[] args)
        {
            while (true)
            {

                Console.Write("Enter expression (ENTER to exit): ");
                string line = Console.ReadLine();

                if (string.IsNullOrEmpty(line))
                    break;

                char[] exp = line.ToCharArray();
                int value = EvaluateExpression(exp);

                Console.WriteLine("{0}={1}", line, value);

            }
        }

        public static int EvaluateExpression(char[] exp)
        {

            Stack<int> vStack = new Stack<int>();
            Stack<char> opStack = new Stack<char>();

            opStack.Push('('); // Implicit opening parenthesis

            int pos = 0;
            while (pos <= exp.Length)
            {
                if (pos == exp.Length || exp[pos] == ')')
                {
                    ProcessClosingParenthesis(vStack, opStack);
                    pos++;
                }
                else if (exp[pos] >= '0' && exp[pos] <= '9')
                {
                    pos = ProcessInputNumber(exp, pos, vStack);
                }
                else
                {
                    ProcessInputOperator(exp[pos], vStack, opStack);
                    pos++;
                }

            }

            return vStack.Pop(); // Result remains on values stacks

        }

        static void ProcessClosingParenthesis(Stack<int> vStack,
                                        Stack<char> opStack)
        {

            while (opStack.Peek() != '(')
                ExecuteOperation(vStack, opStack);

            opStack.Pop(); // Remove the opening parenthesis

        }

        static int ProcessInputNumber(char[] exp, int pos,
                                Stack<int> vStack)
        {

            int value = 0;
            while (pos < exp.Length &&
                    exp[pos] >= '0' && exp[pos] <= '9')
                value = 10 * value + (int)(exp[pos++] - '0');

            vStack.Push(value);

            return pos;

        }

        static void ProcessInputOperator(char op, Stack<int> vStack,
                                    Stack<char> opStack)
        {

            while (opStack.Count > 0 &&
                    OperatorCausesEvaluation(op, opStack.Peek()))
                ExecuteOperation(vStack, opStack);

            opStack.Push(op);

        }

        static bool OperatorCausesEvaluation(char op, char prevOp)
        {

            bool evaluate = false;

            switch (op)
            {
                case '+':
                case '-':
                case '/':
                case '*':
                    evaluate = (prevOp != '(');
                    break;
                case ')':
                    evaluate = true;
                    break;
            }

            return evaluate;

        }

        static void ExecuteOperation(Stack<int> vStack,
                                Stack<char> opStack)
        {

            int rightOperand = vStack.Pop();
            int leftOperand = vStack.Pop();
            char op = opStack.Pop();

            int result = 0;
            switch (op)
            {
                case '+':
                    result = leftOperand + rightOperand;
                    break;
                case '-':
                    result = leftOperand - rightOperand;
                    break;
                case '*':
                    result = leftOperand * rightOperand;
                    break;
                case '/':
                    result = leftOperand / rightOperand;
                    break;
                default:
                    result = 0;
                    break;
            }

            vStack.Push(result);

        }
    }
}
