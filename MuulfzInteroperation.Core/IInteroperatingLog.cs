namespace MuulfzInteroperation.Core
{
    public interface IInteroperatingLog
    {
        void Log(string str);
        void Log(string str, object o);
        void Error(string str);
    }
}