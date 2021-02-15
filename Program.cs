using System;

namespace Singleton.Example
{
    public class Government
    {
        private static Government instance;

        private Government()
        {
            O.C("Private constructor fired!");
        }

        public static Government GetInstance()
        {
            if (Government.instance == null)
                Government.instance = new Government();
            return Government.instance;
        }

        public void SomeAction()
        {
            O.C("Some action from government!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            O.C("Start of the programm!!!");
            // Government government = new Government();    -> impossible because of private constructor
            Government government = Government.GetInstance();
            government.SomeAction();
        }
    }

    static class O
    {
        public static void C(string something)
        {
            Console.WriteLine(something);
        }
    }
}
