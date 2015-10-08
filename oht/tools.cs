using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace oht
{
    class tools
    {
        static public void SetMsg(string str, IWin32Window win)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.IsBalloon = true;
            toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            toolTip.Show(str, win, 0, -75, 5000);//toolTip.Show(str, win, 0, -70, 5000);
        }
    }
}
