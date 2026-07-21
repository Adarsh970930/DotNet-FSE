using System;

namespace DesignPatterns
{
    public interface ICommand
    {
        void Execute();
    }

    // Receiver
    public class Light
    {
        public void TurnOn() => Console.WriteLine("Light is ON.");
        public void TurnOff() => Console.WriteLine("Light is OFF.");
    }

    // Concrete Command 1
    public class LightOnCommand : ICommand
    {
        private readonly Light _light;
        public LightOnCommand(Light light) => _light = light;
        public void Execute() => _light.TurnOn();
    }

    // Concrete Command 2
    public class LightOffCommand : ICommand
    {
        private readonly Light _light;
        public LightOffCommand(Light light) => _light = light;
        public void Execute() => _light.TurnOff();
    }

    // Invoker
    public class RemoteControl
    {
        private ICommand? _command;
        public void SetCommand(ICommand command) => _command = command;
        public void PressButton() => _command?.Execute();
    }

    public static class CommandDemo
    {
        public static void Run()
        {
            Console.WriteLine("--- Command Pattern ---");
            Light livingRoomLight = new();
            ICommand lightOn = new LightOnCommand(livingRoomLight);
            ICommand lightOff = new LightOffCommand(livingRoomLight);

            RemoteControl remote = new();

            remote.SetCommand(lightOn);
            remote.PressButton();

            remote.SetCommand(lightOff);
            remote.PressButton();
        }
    }
}