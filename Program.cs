using System;
using System.Collections.Generic;

namespace Command
{
    abstract class Command
    {
        protected Application App { get; set; }
        protected Editor Editor { get; set; }
        protected string Backup { get; set; }

        public Command(Application app, Editor editor)
        {
            this.App = app;
            this.Editor = editor;
        }

        public void SaveBackup()
        {
            this.Backup = this.Editor.Text;
        }

        public void Undo()
        {
            this.Editor.Text = this.Backup;
        }

        public abstract bool Execute();
    }

    class CopyCommand : Command
    {
        public CopyCommand(Application application, Editor editor) : base(application, editor) { }

        public override bool Execute()
        {
            this.App.Clipboard = this.Editor.GetSelection();
            return false;
        }
    }

    class CutCommand : Command
    {
        public CutCommand(Application application, Editor editor) : base(application, editor) { }

        public override bool Execute()
        {
            this.SaveBackup();
            this.App.Clipboard = this.Editor.GetSelection();
            this.Editor.DeleteSelection();
            return true;
        }
    }

    class PasteCommand : Command
    {
        public PasteCommand(Application application, Editor editor) : base(application, editor) { }

        public override bool Execute()
        {
            this.SaveBackup();
            this.Editor.ReplaceSelection(this.App.Clipboard);
            return true;
        }
    }

    class UndoCommand : Command
    {
        public UndoCommand(Application application, Editor editor) : base(application, editor) { }

        public override bool Execute()
        {
            this.App.Undo();
        }
    }

    class CommandHistory
    {
        private Stack<Command> history { get; set; }

        public void Push(Command command) => this.history.Push(command);

        public Command Pop() => this.history.Pop();
    }

    class Application
    {
        public string Clipboard { get; set; }
        public Editor[] Editors { get; set; }
        public Editor ActiveEditor { get; set; }
        public CommandHistory History { get; set; }

        public void CreateUI ()
        {
            /*
             copy = function() {executeCommand(
                new CopyCommand(this, activeEditor)) }
            copyButton.setCommand(copy)
            shortcuts.onKeyPress("Ctrl+C", copy)

            cut = function() { executeCommand(
                new CutCommand(this, activeEditor)) }
            cutButton.setCommand(cut)
            shortcuts.onKeyPress("Ctrl+X", cut)

            paste = function() { executeCommand(
                new PasteCommand(this, activeEditor)) }
            pasteButton.setCommand(paste)
            shortcuts.onKeyPress("Ctrl+V", paste)

            undo = function() { executeCommand(
                new UndoCommand(this, activeEditor)) }
            undoButton.setCommand(undo)
            shortcuts.onKeyPress("Ctrl+Z", undo)
            */
        }

        public void ExecuteCommand(Command command)
        {
            if (command.Execute())
            {
                this.History.Push(command);
            }
        }

        public void Undo()
        {
            var command = this.History.Pop();
            if (command != null) command.Undo();
        }
    }

    public class Editor
    {
        public string Text { get; set; }

        public string GetSelection()
        {
            return string.Empty; // get selection of this.Text ...
        }

        public void DeleteSelection()
        {
            // delete selected from this.Text ...
        }

        public void ReplaceSelection(string text)
        {
            // replace selection in this.Text with text ...
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
