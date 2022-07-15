
namespace DAS
{
    public interface IController
    {
        void PreLoad();
        void Setup();
        ControllerKey GetKey();

    }
}