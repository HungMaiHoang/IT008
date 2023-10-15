using System;
using System.Reflection.PortableExecutable;

namespace Bai6
{
   public interface IThinking
    {
        void thinking_behavior();
    }
   public interface IIntelligent
    {
        void intelligent_behavior();
    }
    interface IAbility : IThinking, IIntelligent
    {

    }
    abstract class Mamal
    {
        public string characteristics;
    }
    class Whale : Mamal
    {
        Whale() 
        {
            characteristics = "whale";
        }
    }
    class Human : Mamal, IAbility
    {
        Human()
        {
            characteristics = "human";
        }
        public void thinking_behavior()
        { }
        public void intelligent_behavior()
        { }
        static void Main(string[] args)
        { }
    }
}