using System;
using Cysharp.Threading.Tasks;

namespace OneDay.Ui
{
    public class CancelSettings
    {
        // after how many seconds will be timeout happen
        public int Duration;
        // what to call when 
        public Action Action;

        public CancelSettings(int duration, Action cancelAction)
        {
            Duration = duration;
            Action = cancelAction;
        }
    }
    
    public interface ILoading
    {
        UniTask Show(CancelSettings cancelSettings);
        UniTask Hide(bool showDone);
    }
}