using System;

namespace AbstractFactory.Example
{

    abstract class VehicleCreator
    {
        public abstract ICar CreateCar();

        public abstract IBus CreateBus();
    }

    class ExpensiveVehicleCreator : VehicleCreator
    {
        public override ICar CreateCar()
        {
            return new ExpensiveCar();
        }

        public override IBus CreateBus()
        {
            return new ExpensiveBus();
        }
    }

    class ChipVehicleCreator : VehicleCreator
    {
        public override ICar CreateCar()
        {
            return new ChipCar();
        }

        public override IBus CreateBus()
        {
            return new ChipBus();
        }
    }

    public interface ICar
    {
        string Move();
    }

    class ExpensiveCar : ICar
    {
        public string Move()
        {
            return "Car moving expensively!";
        }
    }

    class ChipCar : ICar
    {
        public string Move()
        {
            return "Car moving chip!";
        }
    }

    public interface IBus
    {
        string Move();
    }

    class ExpensiveBus : IBus
    {
        public string Move()
        {
            return "Bus moving expensively!";
        }
    }

    class ChipBus : IBus
    {
        public string Move()
        {
            return "Bus moving chip!";
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("App: expensive vehicles created.");
            ClientCode(new ExpensiveVehicleCreator());

            Console.WriteLine("App: chip vehicles created.");
            ClientCode(new ChipVehicleCreator());
        }

        /*
        Клиентский код работает с экземпляром конкретного создателя, хотя и
        через его базовый интерфейс. Пока клиент продолжает работать с
        создателем через базовый интерфейс, вы можете передать ему любой
        подкласс создателя.
        */
        public static void ClientCode(VehicleCreator creator)
        {
            var car = creator.CreateCar();
            var bus = creator.CreateBus();
            var carMovingResult = car.Move();
            var busMovingResult = bus.Move();

            Console.WriteLine(carMovingResult + "\n" + busMovingResult);
        }
    }
}