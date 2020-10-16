using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace postFixProgramming
{
    class PostFix
    {
        #region vars

        public readonly List<string> _listPostFix = new List<string>();

        #endregion

        #region postfix's function

        /// <summary>
        /// Convert infix to postfix.
        /// </summary>
        /// <param name="infix">Infix input.</param>
        /// <param name="postfix">Result from infix input.</param>
        /// <returns>The postfix value result.</returns>
        public bool Convert(ref string infix, out string postfix)
        {
            postfix = "";
            List<string> values = infix.Split(' ').ToList<string>();
            Stack<string> operators = new Stack<string>();
            foreach (string _item in values)
            {
                if (isOperand(_item))
                {
                    _listPostFix.Add(_item);
                }
                else if (operators.Count == 0 || operators.Peek().Equals("(") || operators.Peek().Equals(")"))
                {
                    operators.Push(_item);
                }
                else if (_item.Equals("("))
                {
                    operators.Push(_item);
                }
                else if (_item.Equals(")"))
                {
                    while (operators.Peek() != "(")
                    {
                        _listPostFix.Add(operators.Pop());
                    }
                    operators.Pop();
                }
                else if (isOperator(_item) && isOperator(operators.Peek()))
                {
                    if (Hierarchy(operators.Peek()) >= Hierarchy(_item))
                    {
                        bool isPushed = false;
                        while (!isPushed)
                        {
                            if (Hierarchy(operators.Peek()) >= Hierarchy(_item))
                            {
                                _listPostFix.Add(operators.Pop());
                            }
                            else
                            {
                                isPushed = !isPushed;
                            }
                        }
                        operators.Push(_item);
                    }
                    else
                    {
                        operators.Push(_item);
                    }
                }
                else if (_item.Equals(";"))
                {
                    while (operators.Count != 0)
                    {
                        _listPostFix.Add(operators.Pop());
                    }
                }
            }

            foreach (string _item in _listPostFix)
            {
                postfix += _item;
            }
            return true;
        }

        #endregion  

        #region class private functions

        /// <summary>
        /// Hierarchy pyramid from programming hierarchy, 4 is the highest priority, 1 is the minor priority; 0 is null.
        /// </summary>
        /// <param name="char">Character input to determinate priority.</param>
        /// <returns>The priority of the hierarchy.</returns>
        private int Hierarchy(string @char)
        {
            int _priority = 0;
            switch (@char)
            {
                case "++":
                case "--":
                    _priority = 9;
                    break;
                case "*":
                case "/":
                case "%":
                    _priority = 8;
                    break;
                case "+":
                case "-":
                    _priority = 7;
                    break;
                case "<":
                case ">":
                case "<=":
                case ">=":
                    _priority = 6;
                    break;
                case "==":
                case "!=":
                    _priority = 5;
                    break;
                case "^":
                    _priority = 4;
                    break;
                case "&&":
                    _priority = 3;
                    break;
                case "||":
                    _priority = 2;
                    break;
                case "=":
                    _priority = 1;
                    break;
                default:
                    _priority = 0;
                    break;
            }
            return _priority;
        }

        /// <summary>
        /// Determines if it's operator.
        /// </summary>
        /// <param name="char">Value operantor input.</param>
        /// <returns>Returns a boolean.</returns>
        private bool isOperator(string @char)
        {
            return (@char.Contains("^") || @char.Contains("&&") || @char.Contains("*") || 
                @char.Contains("/") || @char.Contains("||") || @char.Contains("+") || @char.Contains("-") || @char.Contains("=") || 
                @char.Contains(">=") || @char.Contains("<=") || @char.Contains("==") || @char.Contains("!=")) ? true : false;
        }

        /// <summary>
        /// Determines if it's operand from a word (including characters) or number.
        /// </summary>
        /// <param name="value">Value operand input.</param>
        /// <returns>Returns a boolean.</returns>
        private bool isOperand(string @value)
        {
            return new Regex(@"[A-Za-z0-9]").IsMatch(value) ? true : false;
        }

        #endregion
    }
}
