#include <wx/wx.h>
#include <wx/srchctrl.h>
#include <wx/splitter.h>
#include <wx/listctrl.h>


class reNotedMainWin : public wxFrame
{
public:
  reNotedMainWin(const wxString& title);

  wxMenuBar *menubar;
  wxMenu *file;
  wxMenu *edit;
  wxMenu *tools;
  wxMenu *help;
  wxTextCtrl *textctrl;
  wxListCtrl *fileListCtrl;

	void setupListCtrlColumns();
	void loadListCtrl(int i);
};
