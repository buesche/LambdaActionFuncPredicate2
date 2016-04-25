using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        delegate double CalArepointer(int r);

        /// <summary>
        /// From Video: https://www.youtube.com/watch?v=8o0O-vBS8W0
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //methode 1: Deklaration Delegate + Invoke
            CalArepointer calPointer = CalculateArea;
            double area = calPointer(4); //gleich wie calPointer.Invoke(4);


            //methode 2: Lambda Anonymous Methods, code shorter
            CalArepointer calPointer2 = new CalArepointer(
                delegate (int r)
                {
                    return 3.14 * r * r;
                });
            double area3 = calPointer2(4);

            //methode 3: Lambda Expression, code short and sweet
            CalArepointer calPointer3 = (int r) => 3.14 * r * r;
            double area2 = calPointer3(4);

            //methode 4: Lambda Expression + Func, code shorter and even more sweeter
            Func<int,Double> calPointer4 = (int r) => 3.14 * r * r;
            double area4 = calPointer4(4);

            // Action Delegate (FUNC ohne Return-Value, VOID)
            Action<string> myAction = y => Console.WriteLine(y);
            myAction("Saliii");

            // Predicate (Extension zur Func), for checking purpose
            // you can replace Predicate with Func, in other words predicate delegate can be avoided.
            Predicate<string> CheckGreaterThan5 = x => x.Length > 5;
            CheckGreaterThan5("test"); //--> gibt 'false' zurück


            ///////////////////
            // Warum delegates verwenden?

            List<string> lString = new List<string>();
            lString.Add("Sali");
            lString.Add("Sali Fabian");
            lString.Add("Sali Thomas");

            //Find-predicate gibt hier jeden value der Liste in das Predicate rein und checkt, ob die Bedingung zutrifft. Falls ja, wird dieses zurückgegeben
            lString.Find(CheckGreaterThan5); //gibt Sali Fabian zurück, weil es der erste Value ist, der dem Predicate entspricht.
            lString.FindAll(CheckGreaterThan5); // gibt alle zurück
            lString.Any<string>(); //true
            lString.Any<string>((s) => s.Length > 5); //true


            //////////////////
            /// Important use of Lambda Expression: Expression Trees 
            /// for example (10+20) - (5 + 3)
             
            // 10 + 20
            BinaryExpression b1 = Expression.MakeBinary(ExpressionType.Add,
                Expression.Constant(10),
                Expression.Constant(20));

            // 5 + 3
            BinaryExpression b2 = Expression.MakeBinary(ExpressionType.Add,
                Expression.Constant(5),
                Expression.Constant(3));

            // (10 + 20) - (5 + 3)
            BinaryExpression b3 = Expression.MakeBinary(ExpressionType.Subtract,
                b1,
                b2);

            // This statement will create a delegate by parsing the expression tree
            int result = Expression.Lambda<Func<int>>(b3).Compile()();

            // LINQ Expression Tree
            var x = (from c in lString
                     where c.)
        }

        static double CalculateArea(int r)
        {
            return 3.14 * r * r;
        }
    }
}
