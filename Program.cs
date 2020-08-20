using System;

namespace Builder.Example
{
    abstract class Car
    {
        public int NumberOfSits;
        public EngineType TypeOfEngine;
        public bool GPS;
    }

    interface Builder
    {
        void Reset();
        void SetSeats(int numberOfSits);
        void SetEngine(EngineType type);
        void SetGPS(bool isOrNo);
        Car GetCar();
    }

    class SportCar : Car { }

    class CheapCar : Car { }

    class SportCarBuilder : Builder
    {
        private Car car;

        public Car GetCar() => car;
        public void Reset() => car = new SportCar();
        public void SetEngine(EngineType type) => car.TypeOfEngine = type;
        public void SetGPS(bool isOrNo) => car.GPS = isOrNo;
        public void SetSeats(int numberOfSits) => car.NumberOfSits = numberOfSits;
    }

    class CheapCarBuilder : Builder
    {
        private Car car;

        public Car GetCar() => car;
        public void Reset() => car = new CheapCar();
        public void SetEngine(EngineType type) => car.TypeOfEngine = type;
        public void SetGPS(bool isOrNo) => car.GPS = isOrNo;
        public void SetSeats(int numberOfSits) => car.NumberOfSits = numberOfSits;
    }

    class Director
    {
        private Builder builder;

        public void SetBuilder(Builder builder)
        {
            this.builder = builder;
        }

        public void MakeSportCar()
        {
            builder.Reset();
            builder.SetEngine(EngineType.Powerful);
            builder.SetSeats(2);
            builder.SetGPS(true);
        }

        public void MakeChipCar()
        {
            builder.Reset();
            builder.SetEngine(EngineType.Sick);
            builder.SetSeats(4);
            builder.SetGPS(false);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("App: sport car builder created.");
            ClientCode(new SportCarBuilder());

            Console.WriteLine("App: chip car builder created.");
            ClientCode(new CheapCarBuilder());
        }

        public static void ClientCode(Builder builder)
        {
            Director director = new Director();
            director.SetBuilder(builder);
            if (builder is CheapCarBuilder)
                director.MakeChipCar();
            else
                director.MakeSportCar();
            var resultCar = builder.GetCar();
            PrintCar(resultCar);
        }

        public static void PrintCar(Car car) {
            Console.WriteLine("Engine: " + car.TypeOfEngine.ToString() + "\n" + 
                                "Number of sits: " + car.NumberOfSits + "\n" +
                                "Is gps or not: " + (car.GPS ? "Yes" : "No") + "\n");
        }
    }

    enum EngineType
    {
        Powerful,
        Sick
    }
}