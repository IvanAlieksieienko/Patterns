using System;

namespace Memento
{
	// Класс создателя должен иметь специальный метод, который
	// сохраняет состояние создателя в новом объекте-снимке.
	class Editor
	{
		private string text;
		private double curX, curY, selectionWidth;

		public void SetText(string text) => this.text = text;
		public void SetCursor(double curX, double curY)
		{
			this.curX = curX;
			this.curY = curY;
		}
		public void SetSelectionWidth(double selectionWidth) => this.selectionWidth = selectionWidth;

		// Снимок — неизменяемый объект, поэтому Создатель
		// передаёт все своё состояние через параметры
		// конструктора.
		public ISnapshot CreateSnapshot() => new Snapshot(this, this.text, this.curX, this.curY, this.selectionWidth);
	}

	interface ISnapshot
	{
		void Restore();
	}

	// Снимок хранит прошлое состояние редактора.
	class Snapshot : ISnapshot
	{
		private Editor editor;
		private string text;
		private double curX, curY, selectionWidth;

		public Snapshot(Editor editor, string text, double curX, double curY, double selectionWidth)
		{
			this.editor = editor;
			this.text = text;
			this.curX = curX;
			this.curY = curY;
			this.selectionWidth = selectionWidth;
		}

		// В нужный момент владелец снимка может восстановить
		// состояние редактора.
		public void Restore()
		{
			this.editor.SetText(this.text);
			this.editor.SetCursor(this.curX, this.curY);
			this.editor.SetSelectionWidth(this.selectionWidth);
		}
	}

	// Опекуном может выступать класс команд (см. паттерн Команда).
	// В этом случае команда сохраняет снимок состояния объекта-
	// получателя, перед тем как передать ему своё действие. А в
	// случае отмены команда вернёт объект в прежнее состояние.
	class Command
	{
		private ISnapshot backup;
		private Editor editor;

		public void MakeBackup() => this.backup = this.editor.CreateSnapshot();

		public void Undo() => this.backup?.Restore();
	}
}
