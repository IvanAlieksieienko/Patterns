using System;
using System.Collections.Generic;

namespace Composite
{
    public abstract class GraphicComponent
    {
        public GraphicComponent() { }

        public abstract void Move(int x, int y);

        public abstract void Draw();

        public virtual void Add(GraphicComponent component) => throw new NotImplementedException();

        public virtual void Remove(GraphicComponent component) => throw new NotImplementedException();

        public virtual bool IsComposite() => true;
    }

    public class Dot : GraphicComponent
    {
        protected int x;
        protected int y;

        public Dot() => this.x = this.y = 0;

        public Dot(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override void Move(int x, int y)
        {
            this.x += x;
            this.y += y;
        }

        public override void Draw() => Console.WriteLine($"Dot is printed on {this.x} : {this.y} coordinate.\n");
    }

    public class Circle : Dot
    {
        private int radius;

        public Circle() : base() { }
        public Circle(int x, int y) : base(x, y) => this.radius = 1;
        public Circle(int x, int y, int radius) : base(x, y) => this.radius = radius;

        public override void Draw() => Console.WriteLine($"Circle is printed with center in {this.x} - {this.y} coordinate with {this.radius} radius.\n");
    }

    public class CompositeGraphicComponent : GraphicComponent
    {
        private List<GraphicComponent> Drawing;

        public CompositeGraphicComponent() => this.Drawing = new List<GraphicComponent>();

        public override void Add(GraphicComponent component) => this.Drawing.Add(component);

        public override void Remove(GraphicComponent component) => this.Drawing.Remove(component);

        public override void Move(int x, int y) => this.Drawing.ForEach(graphicComponent => graphicComponent.Move(x, y));

        public override void Draw() => this.Drawing.ForEach(graphicComponent => graphicComponent.Draw());
    }

    class Program
    {
        private static GraphicComponent All;
        static void Main(string[] args)
        {
            All = new CompositeGraphicComponent();
            var dot1 = new Dot(1, 2);
            var circle1 = new Circle(5, 3, 10);
            var dot2 = new Dot(3, 3);
            All.Add(dot1);
            All.Add(circle1);

            All.Draw();
            All.Move(2, 2);
            All.Draw();

            All.Add(dot2);
            var group = new List<GraphicComponent>();
            group.Add(dot1);
            group.Add(dot2);

            GroupSelected(group);
        }

        private static void GroupSelected(List<GraphicComponent> drawings) {
            var group = new CompositeGraphicComponent();
            drawings.ForEach(component => {
                group.Add(component);
                All.Remove(component);
            });
            All.Add(group);
            All.Draw();
        }
    }
}
