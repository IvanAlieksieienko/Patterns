using System;

namespace Decorator
{
    public abstract class Notifier
    {
        public abstract void SendMessage();
    }

    public class SMSNotifier : Notifier
    {
        public override void SendMessage() => Console.WriteLine("Message sent by SMS!\n");
    }

    public class SMSDecorator : Notifier
    {
        private Notifier _notifier;

        protected Notifier Notifier
        {
            get => this._notifier;
            set => this.Notifier = value;
        }

        public SMSDecorator() { }

        public SMSDecorator(Notifier notifier) => this._notifier = notifier;

        public override void SendMessage() => this.Notifier.SendMessage();
    }

    public class FacebookDecorator : SMSDecorator
    {

        public FacebookDecorator() { }

        public FacebookDecorator(Notifier notifier) : base(notifier) { }

        public override void SendMessage()
        {
            this.Notifier.SendMessage();
            Console.WriteLine("Message sent by Facebook\n");
        }
    }

    public class SlackDecorator : SMSDecorator
    {
        public SlackDecorator() { }

        public SlackDecorator(Notifier notifier) : base(notifier) { }

        public override void SendMessage() {
            this.Notifier.SendMessage();
            Console.WriteLine("Message sent by Slack\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var smsNotifier = new SMSNotifier();
            FacebookDecorator facebook = new FacebookDecorator(smsNotifier);
            SlackDecorator slack = new SlackDecorator(facebook);
            slack.SendMessage();
        }
    }
}
