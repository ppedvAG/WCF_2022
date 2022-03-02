using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperChat.Server
{
    delegate void EinfacherDelegate();
    delegate void DelegateMitPara(string txt);
    delegate long CalcDelegate(int a, int b);

    internal class HalloDelegate
    {
        public HalloDelegate()
        {
            EinfacherDelegate einfacherDelegate = EinfacheMethode;
            Action eineAction = EinfacheMethode;
            Action eineActionAno = delegate () { Console.WriteLine("AAAA"); };
            Action eineActionAno2 = () => { Console.WriteLine("Hallo"); };
            Action eineActionAno3 = () => Console.WriteLine("Hallo");

            DelegateMitPara deleMitPara = MethodeMitPara;
            Action<string> actionMitPara = MethodeMitPara;
            DelegateMitPara deleMitParaAno = delegate (string txt) { Console.WriteLine(txt); };
            DelegateMitPara deleMitParaAno2 = (string txt) => { Console.WriteLine(txt); };
            DelegateMitPara deleMitParaAno3 = (string txt) => Console.WriteLine(txt);
            DelegateMitPara deleMitParaAno4 = (txt) => Console.WriteLine(txt);
            DelegateMitPara deleMitParaAno5 = x => Console.WriteLine(x);


            CalcDelegate calcDele = Mult;
            Func<int, int, long> calcDeleFunc = Sum;
            CalcDelegate calcDeleAno = delegate (int a, int b) { return a + b; };
            CalcDelegate calcDeleAno2 = (int a, int b) => { return a + b; };
            CalcDelegate calcDeleAno3 = (a, b) => { return a + b; };
            CalcDelegate calcDeleAno4 = (a, b) => a + b;

            long res = calcDele.Invoke(1, 2);

            List<string> texte = new List<string>();
            texte.Where(x => x.StartsWith("b"));
            texte.Where(Filter);
        }

        private bool Filter(string arg)
        {
            if(arg.StartsWith("b"))
                return true;    
            else
                return false;
        }

        private long Mult(int a, int b)
        {
            return a * b;
        }

        private long Sum(int a, int b)
        {
            return a + b;
        }

        void MethodeMitPara(string name)
        {
            Console.WriteLine($"Hallo {name}");
        }

        void EinfacheMethode()
        {
            Console.WriteLine("Hallo");
        }
    }
}
