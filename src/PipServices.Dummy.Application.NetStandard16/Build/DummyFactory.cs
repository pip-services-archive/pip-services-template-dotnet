using PipServices.Commons.Build;
using PipServices.Commons.Refer;
using PipServices.Dummy.Logic;
using PipServices.Dummy.Memory;
using PipServices.Dummy.Rest;

namespace PipServices.Dummy.Build
{
    public class DummyFactory : IFactory, IDescriptable
    {
        public static DummyController Сontroller { get; private set; }
        public static Descriptor Descriptor { get; } = new Descriptor("pip-services-dummies", "factory", "default", "default", "1.0");

        public Descriptor GetDescriptor()
        {
            return Descriptor;
        }

        public bool CanCreate(object locator)
        {
            var descriptor = locator as Descriptor;

            if (descriptor == null)
                return false;

            if (descriptor.Match(DummyMemoryPersistence.Descriptor))
                return true;

            if (descriptor.Match(DummyController.Descriptor))
                return true;

            if (descriptor.Match(DummyRestService.Descriptor))
                return true;

            return false;
        }

        public object Create(object locator)
        {
            var descriptor = locator as Descriptor;

            if (descriptor == null)
                return null;

            if (descriptor.Match(DummyMemoryPersistence.Descriptor))
                return new DummyMemoryPersistence();

            if (descriptor.Match(DummyController.Descriptor))
            {
                Сontroller = Сontroller ?? new DummyController();
                return Сontroller;
            }

            if (descriptor.Match(DummyRestService.Descriptor))
                return new DummyRestService();

            return null;
        }
    }
}
