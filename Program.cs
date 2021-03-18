using System;

namespace TemplateMethod
{
	class Program
	{
		abstract class GameAI
		{
			protected object[] buildStructures;
			protected object[] scouts;
			protected object[] warriors;
			protected dynamic closestEnemy;

			// Шаблонный метод должен быть задан в базовом классе. Он
			// состоит из вызовов методов в определённом порядке. Чаще
			// всего эти методы являются шагами некоего алгоритма.
			public void Turn()
			{

			}

			// Некоторые из этих методов могут быть реализованы прямо в
			// базовом классе.
			public void CollectResources()
			{
				foreach (var s in buildStructures)
				{
					// s.Collect();
				}
			}

			// А некоторые могут быть полностью абстрактными.
			public abstract void BuildStructures();
			public abstract void BuildUnits();

			// Кстати, шаблонных методов в классе может быть несколько.
			public virtual void Attack()
			{
				var enemy = this.closestEnemy;
				if (enemy == null) this.SendScouts(this.closestEnemy.Position);
				else this.SendWarriors(this.closestEnemy.Position);
			}

			public abstract void SendScouts(dynamic position);
			public abstract void SendWarriors(dynamic position);
		}

		class OrcsAI : GameAI
		{
			public override void BuildStructures()
			{
				// Строить фермы, затем бараки, а потом цитадель.
			}

			public override void BuildUnits()
			{
				/*

				 if (there are plenty of resources) then
					if (there are no scouts)
						// Построить раба и добавить в группу
						// разведчиков.
					else
						// Построить пехотинца и добавить в группу
						// воинов.

				*/
			}

			public override void SendScouts(dynamic position)
			{
				if (this.scouts.Length > 0)
				{
					// Отправить разведчиков на позицию.
				}
			}

			public override void SendWarriors(dynamic position)
			{
				if (this.warriors.Length > 0)
				{
					// Отправить воинов на позицию.
				}
			}
		}

		// Подклассы могут не только реализовывать абстрактные шаги, но
		// и переопределять шаги, уже реализованные в базовом классе.
		class MonstersAI : GameAI
		{
			public override void BuildStructures()
			{
				throw new NotImplementedException();
			}

			public override void BuildUnits()
			{
				throw new NotImplementedException();
			}

			public override void SendScouts(dynamic position)
			{
				throw new NotImplementedException();
			}

			public override void SendWarriors(dynamic position)
			{
				throw new NotImplementedException();
			}
		}

		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
		}
	}
}
