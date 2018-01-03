namespace Ninject
{
    public class MicrosoftProgrammer:Programmer
    {
        public MicrosoftProgrammer(IDrink drink, IEditor editor) : base(drink, editor)
        {
        }
    }
}