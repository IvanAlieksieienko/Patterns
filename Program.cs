using System;
using System.Collections.Generic;

namespace Observer
{
	// Базовый класс-издатель. Содержит код управления подписчиками
	// и их оповещения
	class EventManager
	{
		private Dictionary<string, List<IListener>> listeners;

		public void Subscribe(string eventType, IListener listener)
		{
			if (!this.listeners.ContainsKey(eventType)) this.listeners.Add(eventType, new List<IListener>());
			this.listeners[eventType].Add(listener);
		}

		public void Unsibscribe(string eventType, IListener listener)
		{
			if (!this.listeners.ContainsKey(eventType) || !this.listeners[eventType].Contains(listener)) return;
			this.listeners[eventType].Remove(listener);
		}

		public void Notify(string eventType, object data)
		{
			this.listeners[eventType].ForEach(listener => listener.Update(data));
		}
	}

	// Конкретный класс-издатель, содержащий интересную для других
	// компонентов бизнес-логику. Мы могли бы сделать его прямым
	// потомком EventManager, но в реальной жизни это не всегда
	// возможно (например, если у класса уже есть родитель). Поэтому
	// здесь мы подключаем механизм подписки при помощи композиции.
	class Editor
	{
		private object data;
		public EventManager EventManager { get; set; }

		public Editor() => this.EventManager = new EventManager();

		// Методы бизнес-логики, которые оповещают подписчиков об
		// изменениях.
		public void OpenFile(string path)
		{
			this.data = path as object;
			this.EventManager.Notify("open", this.data);
		}

		public void SaveFile()
		{
			this.EventManager.Notify("save", this.data);
		}
	}

	// Общий интерфейс подписчиков. Во многих языках, поддерживающих
	// функциональные типы, можно обойтись без этого интерфейса и
	// конкретных классов, заменив объекты подписчиков функциями.
	interface IListener
	{
		void Update(object data);
	}

	// Набор конкретных подписчиков. Они реализуют добавочную
	// функциональность, реагируя на извещения от издателя.
	class LoggingListener : IListener
	{
		private object log;
		private string message;

		public LoggingListener(object logFileName, string message)
		{
			this.log = logFileName;
			this.message = message;
		}

		public void Update(object fileName)
		{
			// this.log.write(filename, message);
		}
	}

	class EmailAlertsListener : IListener
	{
		private string email;
		private string message;

		public EmailAlertsListener(string email, string message)
		{
			this.email = email;
			this.message = message;
		}

		public void Update(object data)
		{
			// system.email(email, data, message;
		}
	}

	// Приложение может сконфигурировать издателей и подписчиков как
	// угодно, в зависимости от целей и окружения.
	class Program
	{
		static void Main(string[] args)
		{
			var editor = new Editor();

			var logger = new LoggingListener("/path/to/log.txt", "Someone has opened the file: ");
			editor.EventManager.Subscribe("open", logger);

			var emailAlerts = new EmailAlertsListener("admin@example.com", "Someone has changed the file: ");
			editor.EventManager.Subscribe("save", emailAlerts);
		}
	}
}
