using System;

namespace Mediator
{
	class Program
	{
		// Общий интерфейс посредников.
		interface IMediator
		{
			void Notify(Component sender, string eventName);
		}

		// Конкретный посредник. Все связи между конкретными
		// компонентами переехали в код посредника. Он получает
		// извещения от своих компонентов и знает, как на них
		// реагировать.
		class AuthenticationDialog : IMediator
		{
			private string title;
			private Component loginOrRegisterCheckBox;
			private Component loginUserNameTextBox, loginPasswordTextBox;
			private Component registrationUsernameTextBox, registrationPasswordTextBox, registrationEmailTextBox;
			private Component okButton, cancelButton;

			public AuthenticationDialog()
			{
				// Здесь нужно создать объекты всех компонентов, подав
				// текущий объект-посредник в их конструктор.

				this.loginOrRegisterCheckBox = new Checkbox(this);
				this.loginUserNameTextBox = new Textbox(this);
				this.loginPasswordTextBox = new Textbox(this);
				this.registrationUsernameTextBox = new Textbox(this);
				this.registrationPasswordTextBox = new Textbox(this);
				this.registrationEmailTextBox = new Textbox(this);
				this.okButton = new Button(this);
				this.cancelButton = new Button(this);
			}

			// Когда что-то случается с компонентом, он шлёт посреднику
			// оповещение. После получения извещения посредник может
			// либо сделать что-то самостоятельно, либо перенаправить
			// запрос другому компоненту.
			public void Notify(Component sender, string eventName)
			{
				if (sender == this.loginOrRegisterCheckBox && eventName == "Check")
				{
					if ((this.loginOrRegisterCheckBox as Checkbox).Checked)
					{
						this.title = "Log in";
						// 1. Показать компоненты формы входа.
						// 2. Скрыть компоненты формы регистрации.
					}
					else
					{
						this.title = "Register";
						// 1. Показать компоненты формы регистрации.
						// 2. Скрыть компоненты формы входа.
					}
				}
				if (sender == this.okButton && eventName == "Click")
				{
					if ((this.loginOrRegisterCheckBox as Checkbox).Checked)
					{
						// 1. Показать компоненты формы регистрации.
						// 2. Скрыть компоненты формы входа.
						bool found = true;

						if (!found) { /* Показать ошибку над формой логина.*/ }
						else
						{
							// 1. Создать пользовательский аккаунт с данными
							// из формы регистрации.
							// 2. Авторизировать этого пользователя.
							// ...
						}
					}
				}
			}
		}

		// Классы компонентов общаются с посредниками через их общий
		// интерфейс. Благодаря этому одни и те же компоненты можно
		// использовать в разных посредниках.
		class Component
		{
			protected IMediator dialog;

			public Component(IMediator mediator) => this.dialog = mediator;

			public virtual void Click()
			{
				this.dialog.Notify(this, "Click");
			}

			public virtual void KeyPress()
			{
				this.dialog.Notify(this, "Keypress");
			}
		}

		// Конкретные компоненты не связаны между собой напрямую. У них
		// есть только один канал общения — через отправку уведомлений
		// посреднику.
		class Button : Component
		{
			public Button(IMediator mediator) : base(mediator) { }
		}
		class Textbox : Component
		{
			public Textbox(IMediator mediator) : base(mediator) { }
		}
		class Checkbox : Component
		{
			public bool Checked { get; set; }

			public Checkbox(IMediator mediator) : base(mediator) { }

			public virtual void Check()
			{
				this.dialog.Notify(this, "Check");
			}
		}

		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
		}
	}
}
