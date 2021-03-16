using System;

namespace Strategy
{
	class Program
	{
		interface IStrategy
		{
			int Execute(int a, int b);
		}

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

		class Context
		{
			private IStrategy strategy;

			public void SetStrategy(IStrategy strategy) => this.strategy = strategy;
			public int ExecuteStrategy(int a, int b) => this.strategy.Execute(a, b);
		}

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
