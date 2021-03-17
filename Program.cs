using System;

namespace State
{
	// Общий интерфейс всех состояний.
	abstract class State
	{
		protected Player player;

		// Контекст передаёт себя в конструктор состояния, чтобы
		// состояние могло обращаться к его данным и методам в
		// будущем, если потребуется.
		public State(Player player) => this.player = player;

		public abstract void ClickLock();
		public abstract void ClickPlay();
		public abstract void ClickNext();
		public abstract void ClickPrevious();
	}

	// Конкретные состояния реализуют методы абстрактного состояния
	// по-своему.
	class LockedState : State
	{
		public LockedState(Player player) : base(player) { }

		// При разблокировке проигрователя с заблокированными
		// клавишами он может принять одно из двух состояний.
		public override void ClickLock()
		{
			State nextState = this.player.Playing ? new PlayingState(this.player) : new ReadyState(player);
			this.player.ChangeState(nextState);
		}

		// Ничего не делать.
		public override void ClickNext()
		{
			throw new NotImplementedException();
		}

		public override void ClickPlay()
		{
			throw new NotImplementedException();
		}

		public override void ClickPrevious()
		{
			throw new NotImplementedException();
		}
	}

	// Конкретные состояния сами могут переводить контекст в другое
	// состояние.
	class ReadyState : State
	{
		public ReadyState(Player player) : base(player) { }

		public override void ClickLock() => this.player.ChangeState(new LockedState(this.player));

		public override void ClickNext() => this.player.NextSong();

		public override void ClickPlay()
		{
			this.player.StartPlayback();
			this.player.ChangeState(new PlayingState(this.player));
		}

		public override void ClickPrevious() => this.player.PreviousSong();
	}

	class PlayingState : State
	{
		public PlayingState(Player player) : base(player) { }

		public override void ClickLock() => this.player.ChangeState(new LockedState(this.player));

		public override void ClickNext()
		{
			if (true /*event.doubleClick()*/) this.player.NextSong();
			else this.player.FastForward(5);
		}

		public override void ClickPlay()
		{
			this.player.StopPlayback();
			this.player.ChangeState(new ReadyState(this.player));
		}

		public override void ClickPrevious()
		{
			if (true /* even.doubleClick() */) this.player.PreviousSong();
			else this.player.Rewind(5);
		}
	}

	// Проигрыватель выступает в роли контекста
	class Player
	{
		private State state;
		private object UI, volume, playlist, currentSong;

		public bool Playing { get; set; }

		public Player()
		{
			this.state = new ReadyState(this);

			// Контекст заставляет состояние реагировать на
			// пользовательский ввод вместо себя. Реакция может быть
			// разной, в зависимости от того, какое состояние сейчас
			// активно.
			/*
				UI = new UserInterface()
				UI.lockButton.onClick(this.clickLock)
				UI.playButton.onClick(this.clickPlay)
				UI.nextButton.onClick(this.clickNext)
				UI.prevButton.onClick(this.clickPrevious)
			*/
		}

		// Другие объекты тоже должны иметь возможность заменять
		// состояние проигрывателя.
		public void ChangeState(State state) => this.state = state;

		// Методы UI будут делегировать работу активному состоянию.
		private void ClickLock() => this.state.ClickLock();
		private void ClickPlay() => this.state.ClickPlay();
		private void ClickNext() => this.state.ClickNext();
		private void ClickPrevious() => this.state.ClickPrevious();

		// Сервисные методы контекста, вызываемые состояниями.
		public void StartPlayback() { }
		public void StopPlayback() { }
		public void NextSong() { }
		public void PreviousSong() { }
		public void FastForward(int sec) { }
		public void Rewind(int sec) { }
	}
}
