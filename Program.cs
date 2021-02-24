using System;
using System.Collections.Generic;
using System.Linq;

namespace Flyweight
{
    // Этот класс-легковес содержит часть полей, которые описывают
    // деревья. Эти поля не уникальны для каждого дерева, в отличие,
    // например, от координат: несколько деревьев могут иметь ту же
    // текстуру.
    //
    // Поэтому мы переносим повторяющиеся данные в один-единственный
    // объект и ссылаемся на него из множества отдельных деревьев.
    class TreeType
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public string Texture { get; set; }

        public TreeType(string name, string color, string texture)
        {
            this.Name = name;
            this.Color = color;
            this.Texture = texture;
        }

        // 1. Создать картинку данного типа, цвета и текстуры.
        // 2. Нарисовать картинку на холсте в позиции X, Y.
        public void Draw(object canvas, int x, int y) => Console.WriteLine($"Drawing process at {x} : {y}");
    }

    // Фабрика легковесов решает, когда нужно создать новый
    // легковес, а когда можно обойтись существующим.   
    class TreeFactory
    {
        public static List<TreeType> CommonTreeData { get; set; }

        public static TreeType GetCommonTreeData(string name, string color, string textrue)
        {
            var tree = CommonTreeData.FirstOrDefault(t => t.Name == name && t.Color == color && t.Texture == textrue);
            if (tree == null)
            {
                tree = new TreeType(name, color, textrue);
                CommonTreeData.Add(tree);
            }
            return tree;
        }
    }

    // Контекстный объект, из которого мы выделили легковес
    // TreeType. В программе могут быть тысячи объектов Tree, так
    // как накладные расходы на их хранение совсем небольшие — в
    // памяти нужно держать всего три целых числа (две координаты и
    // ссылка).
    class Tree
    {
        private int x { get; set; }
        private int y { get; set; }
        private TreeType Type { get; set; }

        public Tree(int x, int y, TreeType type)
        {
            this.x = x;
            this.y = y;
            this.Type = type;
        }

        public void Draw(object canvas)
        {
            this.Type.Draw(canvas, this.x, this.y);
        }
    }

    // Классы Tree и Forest являются клиентами Легковеса. При
    // желании их можно слить в один класс, если вам не нужно
    // расширять класс деревьев далее.
    class Forest
    {
        private List<Tree> trees { get; set; }

        public void PlantTree(int x, int y, string name, string color, string texture)
        {
            var type = TreeFactory.GetCommonTreeData(name, color, texture);
            var tree = new Tree(x, y, type);
            trees.Add(tree);
        }

        public void Draw(object canvas)
        {
            foreach (var tree in trees)
            {
                tree.Draw(canvas);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
