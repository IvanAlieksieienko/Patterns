using System;

namespace Strategy
{
	// Общий интерфейс всех стратегий.
	interface IStrategy
	{
		int Execute(int a, int b);
	}

	// Каждая конкретная стратегия реализует общий интерфейс своим
	// способом.
	class SpecificStrategyAdd : IStrategy
	{
		public int Execute(int a, int b) => a + b;
	}

	class SpecificStrategySubstract : IStrategy
	{
		public int Execute(int a, int b) => a - b;
	}

	class SpecificStrategyMultiply : IStrategy
	{
		public int Execute(int a, int b) => a * b;
	}

	// Контекст всегда работает со стратегиями через общий
	// интерфейс. Он не знает, какая именно стратегия ему подана.
	class Context
	{
		private IStrategy strategy;

		public void SetStrategy(IStrategy strategy) => this.strategy = strategy;
		public int ExecuteStrategy(int a, int b) => this.strategy.Execute(a, b);
	}

	class Program
	{
		// Конкретная стратегия выбирается на более высоком уровне,
		// например, конфигуратором всего приложения. Готовый объект-
		// стратегия подаётся в клиентский объект, а затем может быть
		// заменён другой стратегией в любой момент на лету.
		static void Main(string[] args)
		{
			var input = OperationType.Add;

			var operationType = input;

			Context context = new Context();
			int a = 10;
			int b = 45;

			context.SetStrategy(operationType switch
			{
				OperationType.Add => new SpecificStrategyAdd(),
				OperationType.Substract => new SpecificStrategySubstract(),
				OperationType.Multiply => new SpecificStrategyMultiply(),
				_ => new SpecificStrategyAdd()
			});

			var result = context.ExecuteStrategy(a, b);

			Console.WriteLine(result);
		}

		enum OperationType
		{
			Add = 0,
			Substract = 1,
			Multiply = 2
		}
	}
}
