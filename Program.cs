using System;

namespace FactoryMethod.Example
{
    abstract class VehicleCreator
    {
        public abstract IVehicle CreateVehicle();

        public string SomeOperation()
        {
            var vehicle = CreateVehicle();
            var result = vehicle.Move();
            return result;
        }
    }

    class CarCreator : VehicleCreator
    {
        public override IVehicle CreateVehicle()
        {
            return new Car();
        }
    }

    class BusCreator : VehicleCreator
    {
        public override IVehicle CreateVehicle()
        {
            return new Bus();
        }
    }

    public interface IVehicle
    {
        string Move();
    }

    class Car : IVehicle
    {
        public string Move()
        {
            return "Car is moving!";
        }
    }

    class Bus : IVehicle
    {
        public string Move()
        {
            return "Bus is moving!";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("App: car created.");
            ClientCode(new CarCreator());

            Console.WriteLine("App: bus created.");
            ClientCode(new BusCreator());
        }

        /*
        Клиентский код работает с экземпляром конкретного создателя, хотя и
        через его базовый интерфейс. Пока клиент продолжает работать с
        создателем через базовый интерфейс, вы можете передать ему любой
        подкласс создателя.
        */
        public static void ClientCode(VehicleCreator creator)
        {
            Console.WriteLine("Client: I'm not aware of the creator's class," +
                "but it still works.\n" + creator.SomeOperation());
        }
    }
}