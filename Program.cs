using System;
using System.Collections.Generic;

namespace Prototype.Example
{
    interface IPrototype<T> where T : class
    {
        T Clone();
    }

    // Clone method is used for copying instead constuctor
    class Car : IPrototype<Car>
    {
        private string Model;
        private int Cost;
        public string Brand;
        public int NumberOfSits;

        public Car Clone()
        {
            return new Car()
            {
                Model = this.Model,
                Cost = this.Cost,
                Brand = this.Brand,
                NumberOfSits = this.NumberOfSits
            };
        }

        public void SetPrivateFields(string model, int cost)
        {
            this.Model = model;
            this.Cost = cost;
        }

        public string Print()
        {
            return $"Brand: {this.Brand}\n" +
                    $"Model: {this.Model}\n" +
                    $"Cost: {this.Cost}\n" +
                    $"Number of sits : {this.NumberOfSits}\n";
        }
    }

    // Constructor is used to make copy, Clone method just
    // calls constructor
    class Computer : IPrototype<Computer>
    {
        private bool New;
        private int Cost;
        public string Brand;
        public string Model;

        public Computer() { }

        public Computer(Computer computer)
        {
            this.New = computer.New;
            this.Cost = computer.Cost;
            this.Brand = computer.Brand;
            this.Model = computer.Model;
        }

        public Computer Clone()
        {
            return new Computer(this);
        }

        public void SetPrivateFields(bool newOrNot, int cost)
        {
            this.New = newOrNot;
            this.Cost = cost;
        }

        public string Print()
        {
            return $"Brand: {this.Brand}\n" +
                    $"Model: {this.Model}\n" +
                    $"Cost: {this.Cost}\n" +
                    $"New : {this.New}\n";
        }
    }

    class Program
    {
        public static List<object> CarsAndComputers = new List<object>();

        static void Main(string[] args)
        {
            Car OneCar = new Car();
            OneCar.Brand = "Aston Martin";
            OneCar.NumberOfSits = 2;
            OneCar.SetPrivateFields("DB9", 15000);
            Computer OneComputer = new Computer();
            OneComputer.Brand = "Apple";
            OneComputer.Model = "MacBook Air";
            OneComputer.SetPrivateFields(true, 1000);
            CarsAndComputers.Add(OneComputer);
            CarsAndComputers.Add(OneCar);

            PrintList(CarsAndComputers);

            List<object> listToCopy = new List<object>();
            foreach (var thing in CarsAndComputers)
            {
                if (thing is Car car)
                    listToCopy.Add(car.Clone());
                else if (thing is Computer computer)
                    listToCopy.Add(computer.Clone());
                else continue;
            }

            PrintList(listToCopy);
        }

        static void PrintList(List<object> list)
        {
            foreach (var thing in list)
            {
                string stringToPrint = string.Empty;
                if (thing is Car car)
                    stringToPrint = car.Print();
                else if (thing is Computer computer)
                    stringToPrint = computer.Print();
                Console.WriteLine(stringToPrint);
            }
        }
    }
}