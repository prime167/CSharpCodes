using Ninject.Modules;

namespace Ninject
{
    class CustomModule:NinjectModule
    {
        public override void Load()
        {
            Bind<IDrink>().To<Milk>();
            Bind<IEditor>().To<Vi>().WhenInjectedInto<LinuxProgrammer>();
            Bind<IEditor>().To<VisualStudio>().WhenInjectedInto<MicrosoftProgrammer>();
        }
    }
}
