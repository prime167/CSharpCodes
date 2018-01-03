using System;

namespace Ninject
{
    public abstract class Programmer
    {
        private readonly IDrink _drink;
        private readonly IEditor _editor;

        protected Programmer(IDrink drink, IEditor editor)
        {
            _drink = drink;
            _editor = editor;
        }

        public void WriteCode()
        {
            Console.WriteLine(_drink.Drink());
            Console.WriteLine(_editor.Code());
        }
    }
}