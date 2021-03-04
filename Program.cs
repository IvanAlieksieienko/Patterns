using System;
using System.Collections.Generic;

namespace ChainOfResponsibillity
{
    interface IComponentWithContextualHelp
    {
        void ShowHelp();
    }

    abstract class Component : IComponentWithContextualHelp
    {
        public string ToolTipText { get; set; }
        public Component NextComponent { get; set; }

        public virtual void ShowHelp()
        {
            if (this.ToolTipText != null && this.ToolTipText != string.Empty)
                Console.WriteLine(this.ToolTipText);
            else this.NextComponent.ShowHelp();
        }
    }

    abstract class Container : Component
    {
        protected List<Component> Children { get; set; }

        public void Add(Component child)
        {
            if (this.Children == null) this.Children = new List<Component>();

            this.Children.Add(child);
            child.NextComponent = this;
        }
    }

    class Button : Component { }

    class Panel : Container
    {
        public string modalHelpText { get; set; }

        public override void ShowHelp()
        {
            if (this.modalHelpText != null && this.modalHelpText != string.Empty)
                Console.WriteLine(this.modalHelpText);
            else base.ShowHelp();
        }
    }

    class Dialog : Container
    {
        public string wikiPageUrl { get; set; }

        public override void ShowHelp()
        {
            if (this.wikiPageUrl != null && this.wikiPageUrl != string.Empty)
                Console.WriteLine(this.wikiPageUrl);
            else base.ShowHelp();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CreateUI();
        }

        static void CreateUI()
        {
            var dialog = new Dialog() { wikiPageUrl = @"https://ru.wikipedia.org/wiki/Беэр-Шева" };
            var panel = new Panel() { modalHelpText = @"This panel shows currency state for last 5 years." };
            var button = new Button() { ToolTipText = @"This button confirms your approve to share data with us." };
            panel.Add(button);
            dialog.Add(panel);

            // ... some process and time
            OnF1KeyPress(panel); // This panel shows currency state for last 5 years.

        }

        static void OnF1KeyPress(Container container) => container.ShowHelp();
    }
}
