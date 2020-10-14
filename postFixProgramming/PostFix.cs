using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace postFixProgramming
{
    class PostFix
    {
        public readonly List<string> _listPostFix = new List<string>();
        
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
                        _listPostFix.Add(operators.Pop());
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

        private int Hierarchy(string @char)
        {
            int _priority = 0;
            if (@char.Contains("^"))
            {
                _priority = 4;
            }
            else if (@char.Contains("&&") || @char.Contains("*") || @char.Contains("/"))
            {
                _priority = 3;

            }
            else if (@char.Contains("||") || @char.Contains("+") || @char.Contains("-"))
            {
                _priority = 2;
            }
            else if (@char.Contains("=") || @char.Contains(">") || @char.Contains("<") || @char.Contains(">=") || 
                @char.Contains("<=") || @char.Contains("==") || @char.Contains("!="))
            {
                _priority = 1;
            }
            return _priority;
        }

        private bool isOperator(string @char)
        {
            return (@char.Contains("^") || @char.Contains("&&") || @char.Contains("*") || 
                @char.Contains("/") || @char.Contains("||") || @char.Contains("+") || @char.Contains("-") || @char.Contains("=") || 
                @char.Contains(">=") || @char.Contains("<=") || @char.Contains("==") || @char.Contains("!=")) ? true : false;
        }

        private bool isOperand(string @value)
        {
            return (new Regex(@"[A-Za-z0-9]").IsMatch(value)) ? true : false;
        }

    }
}
