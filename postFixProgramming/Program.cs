using System;
using System.Collections.Generic;

namespace postFixProgramming
{
    class Program
    {  
        static void Main(string[] args)
        {
            PostFix postFixClass = new PostFix();
            Console.WriteLine("Type a infix expression:");
            string infix = Console.ReadLine(), postfix;
            postFixClass.Convert(ref infix,out postfix);
            Console.WriteLine(string.Format("\n {0}", postfix));
        }
    }
}
