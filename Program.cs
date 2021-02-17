using System;

namespace Bridge
{
    interface IDevice
    {
        bool IsEnabled();
        void Enable();
        void Disable();
        int GetVolume();
        void SetVolume(int volumeValue);
    }

    class RemoteInstrument
    {
        protected IDevice controlDevice;

        public RemoteInstrument() { }

        public RemoteInstrument(IDevice device) => this.controlDevice = device;

        public void TogglePower()
        {
            if (controlDevice.IsEnabled())
                controlDevice.Disable();
            else
                controlDevice.Enable();
        }

        public void IncreaseVolume() => this.controlDevice.SetVolume(this.controlDevice.GetVolume() + 2);

        public void DecreaseVolume() => this.controlDevice.SetVolume(this.controlDevice.GetVolume() - 2);
    }

    class AdvancedRemoteInstrument : RemoteInstrument
    {
        public AdvancedRemoteInstrument() : base() {}

        public AdvancedRemoteInstrument(IDevice device) : base(device) {}
        
        public void MuteVolume() => this.controlDevice.SetVolume(0);
    }

    class TV : IDevice
    {
        private bool isEnabled = false;
        private int volume = 5;

        public void Disable() => this.isEnabled = false;

        public void Enable() => this.isEnabled = true;

        public int GetVolume() => this.volume;

        public bool IsEnabled() => this.isEnabled;

        public void SetVolume(int volumeValue)
        {
            if (volumeValue < 0) this.volume = 0;
            else if (volumeValue > 50) this.volume = 50;
            else this.volume = volumeValue;
        }
    }

    class Radio : IDevice
    {
        private bool isEnabled = false;
        private int volume = 5;

        public void Disable() => this.isEnabled = false;

        public void Enable() => this.isEnabled = true;

        public int GetVolume() => this.volume;

        public bool IsEnabled() => this.isEnabled;

        public void SetVolume(int volumeValue)
        {
            if (volumeValue < 0) this.volume = 0;
            else if (volumeValue > 10) this.volume = 10;
            else this.volume = volumeValue;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            TV tv = new TV();
            var remote = new RemoteInstrument(tv);
            Console.WriteLine(tv.IsEnabled());
            remote.TogglePower();
            Console.WriteLine(tv.IsEnabled());
            Radio radio = new Radio();
            var advancedRemote = new AdvancedRemoteInstrument(radio);
            Console.WriteLine(radio.GetVolume());
            advancedRemote.MuteVolume();
            Console.WriteLine(radio.GetVolume());
        }
    }
}
