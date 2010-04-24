#include "main.h"
#include "reNotedMainWin.h"

IMPLEMENT_APP(MyApp)

bool MyApp::OnInit()
{

    reNotedMainWin *rnmw = new reNotedMainWin(wxT("reNotedPre"));
    rnmw->Show(true);

    return true;
}