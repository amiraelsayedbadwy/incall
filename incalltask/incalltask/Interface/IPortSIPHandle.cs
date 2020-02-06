using System;
using System.Collections.Generic;
using System.Text;

namespace incalltask.Interface
{
   public interface IPortSIPHandle
    {
        void pressNumpadButton(int dtmf);
        long makeCall(string callee, bool videoCall);
        void hungUpCall();
        void holdCall();
        void unholdCall();
        void referCall(string referTo);
        void muteCall(bool mute);
        void setLoudspeakerStatus(bool enable);
        void didSelectLine(int activeLine);
        void switchSessionLine();


       // bool createConference(PortSIPVideoRenderView confVideoWindow, PortSIPVideoRenderView nullVideoWindow);
        void destoryConference();

    }
}

