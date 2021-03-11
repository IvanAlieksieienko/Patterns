using System;

namespace Iterator
{
	class Profile
	{
		public string Id { get; set; }
		// some properties...
	}

	// Общий интерфейс итераторов
	interface ProfileIterator
	{
		Profile GetNext();
		bool HasMore { get; }
	}

	// Конкретный итератор.
	class FacebookIterator : ProfileIterator
	{
		// Итератору нужна ссылка на коллекцию, которую он обходит.
		private Facebook facebook;
		private string profileId, type;

		// Но каждый итератор обходит коллекцию, независимо от
		// остальных, поэтому он содержит информацию о текущей
		// позиции обхода.
		private int currentPosition;
		private Profile[] cache;

		public FacebookIterator(Facebook facebook, string profileId, string type)
		{
			this.facebook = facebook;
			this.profileId = profileId;
			this.type = type;
		}

		// Итератор реализует методы базового интерфейса по-своему.
		public bool HasMore
		{
			get
			{
				this.LazyInit();
				return this.currentPosition < cache.Length;
			}
		}

		public Profile GetNext()
		{
			if (this.HasMore) this.currentPosition++;
			return this.cache[this.currentPosition];
		}

		private void LazyInit()
		{
			if (this.cache == null) this.cache = this.facebook.SocialGraphRequest(this.profileId, this.type);
		}
	}

	// Общий интерфейс коллекций должен определить фабричный метод
	// для производства итератора. Можно определить сразу несколько
	// методов, чтобы дать пользователям различные варианты обхода
	// одной и той же коллекции.
	interface ISocialNetwork
	{
		ProfileIterator CreateFriendsIterator(string profileId);
		ProfileIterator CreateCoworkersIterator(string profileId);
	}

	// Конкретная коллекция знает, объекты каких итераторов нужно
	// создавать.
	class Facebook : ISocialNetwork
	{
		public ProfileIterator CreateCoworkersIterator(string profileId)
		{
			return new FacebookIterator(this, profileId, "coworkers");
		}

		public ProfileIterator CreateFriendsIterator(string profileId)
		{
			return new FacebookIterator(this, profileId, "friends");
		}

		public Profile[] SocialGraphRequest(string profileId, string type)
		{
			return new Profile[20];
		}
	}

	class SocialSpammer
	{
		public void Send(ProfileIterator iterator, string message)
		{
			while (iterator.HasMore)
			{
				var profile = iterator.GetNext();
				// System.sendEmail(profile.getEmail(), message)
			}
		}
	}

	// Класс приложение конфигурирует классы, как захочет.
	class Program
	{
		static ISocialNetwork network;
		static SocialSpammer spammer;

		static void Main(string[] args)
		{
			if (true)
			{
				network = new Facebook();
			}
		}

		static void SendSpamToFriends(Profile profile)
		{
			var iterator = network.CreateFriendsIterator(profile.Id);
			spammer.Send(iterator, "Some message");
		}

		static void SendSpamToCoworkers(Profile profile)
		{
			var iterator = network.CreateCoworkersIterator(profile.Id);
			spammer.Send(iterator, "Some message");
		}
	}
}
