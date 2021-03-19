using System;
using System.Collections.Generic;

namespace Visitor
{
	// Сложная иерархия элементов.
	interface IShape
	{
		void Move(int x, int y);
		void Draw();
		void Accept(IVisitor visitor);
	}

	// Метод принятия посетителя должен быть реализован в каждом
	// элементе, а не только в базовом классе. Это поможет программе
	// определить, какой метод посетителя нужно вызвать, если вы не
	// знаете тип элемента.
	class Dot : IShape
	{
		public void Accept(IVisitor visitor) => visitor.Visit(this);

		public void Draw() => throw new NotImplementedException();

		public void Move(int x, int y) => throw new NotImplementedException();
	}

	class Circle : IShape
	{
		public void Accept(IVisitor visitor) => visitor.Visit(this);

		public void Draw() => throw new NotImplementedException();

		public void Move(int x, int y) => throw new NotImplementedException();
	}

	class Rectangle : IShape
	{
		public void Accept(IVisitor visitor) => visitor.Visit(this);

		public void Draw() => throw new NotImplementedException();

		public void Move(int x, int y) => throw new NotImplementedException();
	}

	class CompoundShape : IShape
	{
		public void Accept(IVisitor visitor) => visitor.Visit(this);

		public void Draw() => throw new NotImplementedException();

		public void Move(int x, int y) => throw new NotImplementedException();
	}

	// Интерфейс посетителей должен содержать методы посещения
	// каждого элемента. Важно, чтобы иерархия элементов менялась
	// редко, так как при добавлении нового элемента придётся менять
	// всех существующих посетителей.
	interface IVisitor
	{
		void Visit(Dot dot);
		void Visit(Circle circle);
		void Visit(Rectangle rectangle);
		void Visit(CompoundShape compoundShape);
	}

	// Конкретный посетитель реализует одну операцию для всей
	// иерархии элементов. Новая операция = новый посетитель.
	// Посетитель выгодно применять, когда новые элементы
	// добавляются очень редко, а новые операции — часто.
	class XMLExportVisitor : IVisitor
	{
		public void Visit(Dot dot)
		{
			// Экспорт id и координат центра точки.
		}

		public void Visit(Circle circle)
		{
			// Экспорт id, кординат центра и радиуса окружности.
		}

		public void Visit(Rectangle rectangle)
		{
			// Экспорт id, кординат левого-верхнего угла, ширины и
			// высоты прямоугольника.

		}

		public void Visit(CompoundShape compoundShape)
		{
			// Экспорт id составной фигуры, а также списка id
			// подфигур, из которых она состоит.
		}
	}

	// Приложение может применять посетителя к любому набору
	// объектов элементов, даже не уточняя их типы. Нужный метод
	// посетителя будет выбран благодаря проходу через метод accept.
	class Program
	{
		static List<IShape> AllShapes { get; set; }

		static void Main(string[] args) => Program.Export();

		static void Export()
		{
			var exportVisitor = new XMLExportVisitor();
			Program.AllShapes.ForEach(shape => shape.Accept(exportVisitor));
		}
	}
}
