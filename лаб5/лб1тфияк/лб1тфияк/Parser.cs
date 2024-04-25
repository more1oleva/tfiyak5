using System;
using System.Collections.Generic;
using System.Text;

namespace лб1тфияк
{
    public class Parser
    {
        private string input;
        private int position;

        public Parser(string input)
        {
            this.input = input;
            this.position = 0;
        }

        public double Parse()
        {
            List<string> poliz = ConvertToPoliz(input);
            return EvaluatePoliz(poliz);
        }

        private List<string> ConvertToPoliz(string expression)
        {
            List<string> poliz = new List<string>();
            Stack<char> operators = new Stack<char>();
            bool lastTokenWasOperatorOrOpeningBracket = true; // Flag to handle unary minus

            for (int i = 0; i < expression.Length; i++)
            {
                char c = expression[i];
                if (char.IsDigit(c))
                {
                    StringBuilder numBuilder = new StringBuilder();
                    while (i < expression.Length && (char.IsDigit(expression[i]) || expression[i] == '.'))
                    {
                        numBuilder.Append(expression[i]);
                        i++;
                    }
                    i--;
                    poliz.Add(numBuilder.ToString());
                    lastTokenWasOperatorOrOpeningBracket = false;
                }
                else if (c == '(')
                {
                    operators.Push(c);
                    lastTokenWasOperatorOrOpeningBracket = true;
                }
                else if (c == ')')
                {
                    while (operators.Count > 0 && operators.Peek() != '(')
                    {
                        poliz.Add(operators.Pop().ToString());
                    }
                    if (operators.Count == 0)
                    {
                        throw new Exception($"Unmatched closing parenthesis at position {i}");
                    }
                    operators.Pop(); // Pop '('
                    lastTokenWasOperatorOrOpeningBracket = false;
                }
                else if (c == '+' || c == '*' || c == '/')
                {
                    while (operators.Count > 0 && Priority(operators.Peek()) >= Priority(c))
                    {
                        poliz.Add(operators.Pop().ToString());
                    }
                    operators.Push(c);
                    lastTokenWasOperatorOrOpeningBracket = true;
                }
                else if (c == '-')
                {
                    // Check if it's a unary minus or a binary minus
                    if (lastTokenWasOperatorOrOpeningBracket)
                    {
                        // Unary minus
                        poliz.Add("0"); // Add a zero placeholder
                        operators.Push(c);
                    }
                    else
                    {
                        // Binary minus
                        while (operators.Count > 0 && Priority(operators.Peek()) >= Priority(c))
                        {
                            poliz.Add(operators.Pop().ToString());
                        }
                        operators.Push(c);
                    }
                    lastTokenWasOperatorOrOpeningBracket = true;
                }
                else
                {
                    throw new Exception($"Invalid character '{c}' at position {i}");
                }
            }

            while (operators.Count > 0)
            {
                poliz.Add(operators.Pop().ToString());
            }

            return poliz;
        }

        private double EvaluatePoliz(List<string> poliz)
        {
            Stack<double> stack = new Stack<double>();

            foreach (string token in poliz)
            {
                if (double.TryParse(token, out double num))
                {
                    stack.Push(num);
                }
                else
                {
                    double b = stack.Pop();
                    double a = stack.Pop();
                    switch (token)
                    {
                        case "+":
                            stack.Push(a + b);
                            break;
                        case "-":
                            stack.Push(a - b);
                            break;
                        case "*":
                            stack.Push(a * b);
                            break;
                        case "/":
                            stack.Push(a / b);
                            break;
                        default:
                            throw new Exception("Invalid token in POLIZ: " + token);
                    }
                }
            }

            return stack.Pop();
        }

        private int Priority(char op)
        {
            switch (op)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                default:
                    return 0;
            }
        }
    }
}
