using System;

namespace Adapter.Example
{
    // abstract, virtual, inheritance, override
    abstract class Printer {
        public virtual string Print(double side) {
            const string offset = "\t\t\t";
            string result = string.Empty;
            result += "\n\n";
            result += offset + side + "\n\n";
            for (int i = 1; i < side; i++)
            {
                result += offset + "|\n\n";
            }
            result += offset + "|" + "__";
            for (int i = 1; i < side; i++)
            {
                result += "   __";
            }
            result += "   " + side;
            result += "\n" + offset + "0";
            result += "\n\n";
            return result;
        }
    }

    class RoundHole : Printer
    {
        private double radius;
        public RoundHole() => radius = 0;
        public RoundHole(double radius) => this.radius = radius;

        public virtual double GetRadius() => this.radius;
        public bool Fits(RoundPeg peg) => this.GetRadius() >= peg.GetRadius();
    }

    class RoundPeg : Printer
    {
        private double radius;
        public RoundPeg() => radius = 0;
        public RoundPeg(double radius) => this.radius = radius;

        public virtual double GetRadius() => this.radius;
    }

    class SquarePeg : Printer
    {
        private double width;
        public SquarePeg() => width = 0;
        public SquarePeg(double width) => this.width = width;

        public double GetWidth() => this.width;

        public override string Print(double side) {
            const string offset = "\t\t\t";
            string result = string.Empty;
            result += "\n\n";
            result += offset + side + "\n";
            result += offset + " __";
            for (int i = 1; i < side; i++) {
                result += "   __";
            }
            result += "\n";
            for (int i = 1; i < side; i++)
            {
                result += offset + "|";
                for (int j = 1; j < side; j++) {
                    result += "     ";
                }
                result += "  ";
                result += "|\n\n";
            }
            result += offset + "|" + "__";
            for (int i = 1; i < side; i++)
            {
                result += "   __";
            }
            result += "|  " + side;
            result += "\n" + offset + "0";
            result += "\n\n";
            return result;
        } 
    }

    class SquarePegAdapter : RoundPeg
    {
        private SquarePeg peg;
        public SquarePegAdapter() => this.peg = new SquarePeg();
        public SquarePegAdapter(SquarePeg peg) => this.peg = peg;

        public override double GetRadius() => peg.GetWidth() * Math.Sqrt(2) / 2;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var hole = new RoundHole(5);
            var roundPeg = new RoundPeg(5);
            Console.WriteLine(hole.Print(hole.GetRadius()));
            Console.WriteLine(roundPeg.Print(roundPeg.GetRadius()));
            Console.WriteLine(hole.Fits(roundPeg));

            var smallSquarePeg = new SquarePeg(5);
            var largeSqurePeg = new SquarePeg(10);
            Console.WriteLine(smallSquarePeg.Print(smallSquarePeg.GetWidth()));
            Console.WriteLine(largeSqurePeg.Print(largeSqurePeg.GetWidth()));

            //  hole.Fits(smallSquarePeg)   cannot convert from 'Adapter.Example.SquarePeg' to 'Adapter.Example.RoundPeg' [Adapter]csharp(CS1503)
            var smallSquarePegAdapter = new SquarePegAdapter(smallSquarePeg);
            var largeSqurePegAdaprter = new SquarePegAdapter(largeSqurePeg);
            Console.WriteLine(hole.Fits(smallSquarePegAdapter));
            Console.WriteLine(hole.Fits(largeSqurePegAdaprter));
        }
    }
}
