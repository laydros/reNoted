#include "reNotedMainWin.h"

reNotedMainWin::reNotedMainWin(const wxString& title)
       : wxFrame(NULL, -1, title, wxPoint(-1, -1), wxSize(300, 400))
{
	menubar = new wxMenuBar;
	file = new wxMenu;
	edit = new wxMenu;
	tools = new wxMenu;
	help = new wxMenu;

	menubar->Append(file, "&File");
	menubar->Append(edit, "&Edit");
	menubar->Append(tools, "&Tools");
	menubar->Append(help, "&Help");
	SetMenuBar(menubar);

	wxSplitterWindow *windowSplit = new wxSplitterWindow(this, -1);

	wxPanel *topPanel = new wxPanel(windowSplit, wxID_ANY);

	wxBoxSizer *topVertBox = new wxBoxSizer(wxVERTICAL);

	wxSearchCtrl *searchBox = new wxSearchCtrl(topPanel, wxID_ANY);
	searchBox->ShowCancelButton(true);

	topVertBox->Add(searchBox, 0, wxEXPAND, 5);

	fileListCtrl = new wxListCtrl(topPanel, wxID_ANY, wxDefaultPosition, wxDefaultSize, wxLC_REPORT | wxVSCROLL | wxALWAYS_SHOW_SB);
	
	topVertBox->Add(fileListCtrl, 1, wxEXPAND, 5);
	
	setupListCtrlColumns();

	for(int i = 0; i < 5; i++)
	{
		loadListCtrl(i);
	}

	//wxTextCtrl *searchBox = new wxTextCtrl(basePanel, wxID_ANY);
	//searchHorizontalBox->Add(searchBox, 1, wxRIGHT, 8);
	//wxButton *clearButton = new wxButton(basePanel, wxID_ANY, "Clear");
	//searchHorizontalBox->Add(clearButton, 0);

	//mainVertBox->Add(searchBox, 0, wxEXPAND | wxLEFT | wxRIGHT | wxTOP, 5);

	//mainVertBox->Add(-1, 5);

	//wxBoxSizer *listBoxSizer = new wxBoxSizer(wxHORIZONTAL);
	//wxListBox *noteListBox = new wxListBox(basePanel, wxID_ANY);

	//listBoxSizer->Add(noteListBox, 1, wxEXPAND | wxALL, 5);
	//mainVertBox->Add(listBoxSizer, 1, /*wxLEFT | wxRIGHT |*/wxEXPAND | wxTOP, 5);

	//mainVertBox->Add(-1, 5);


	//noteListBox->Append("first item");

  //wxBoxSizer *vbox = new wxBoxSizer(wxVERTICAL);

  //wxBoxSizer *hbox1 = new wxBoxSizer(wxHORIZONTAL);
  //wxStaticText *st1 =  new wxStaticText(panel, wxID_ANY, 
  //    wxT("Class Name"));

  //hbox1->Add(st1, 0, wxRIGHT, 8);
  //wxTextCtrl *tc = new wxTextCtrl(panel, wxID_ANY);
  //hbox1->Add(tc, 1);
  //vbox->Add(hbox1, 0, wxEXPAND | wxLEFT | wxRIGHT | wxTOP, 10);

  //vbox->Add(-1, 10);

  //wxBoxSizer *hbox2 = new wxBoxSizer(wxHORIZONTAL);
  //wxStaticText *st2 = new wxStaticText(panel, wxID_ANY, 
  //    wxT("Matching Classes"));

  //hbox2->Add(st2, 0);
  //vbox->Add(hbox2, 0, wxLEFT | wxTOP, 10);

  //vbox->Add(-1, 10);

  //wxBoxSizer *hbox3 = new wxBoxSizer(wxHORIZONTAL);
  //wxTextCtrl *tc2 = new wxTextCtrl(panel, wxID_ANY, wxT(""), 
  //    wxPoint(-1, -1), wxSize(-1, -1), wxTE_MULTILINE);

  //hbox3->Add(tc2, 1, wxEXPAND);
  //vbox->Add(hbox3, 1, wxLEFT | wxRIGHT | wxEXPAND, 10);

  //vbox->Add(-1, 25);

  //wxBoxSizer *hbox4 = new wxBoxSizer(wxHORIZONTAL);
  //wxCheckBox *cb1 = new wxCheckBox(panel, wxID_ANY, 
  //    wxT("Case Sensitive"));

  //hbox4->Add(cb1);
  //wxCheckBox *cb2 = new wxCheckBox(panel, wxID_ANY, 
  //    wxT("Nested Classes"));

  //hbox4->Add(cb2, 0, wxLEFT, 10);
  //wxCheckBox *cb3 = new wxCheckBox(panel, wxID_ANY, 
  //    wxT("Non-Project Classes"));

  //hbox4->Add(cb3, 0, wxLEFT, 10);
  //vbox->Add(hbox4, 0, wxLEFT, 10);

  //vbox->Add(-1, 25);

  //wxBoxSizer *hbox5 = new wxBoxSizer(wxHORIZONTAL);
  //wxButton *btn1 = new wxButton(panel, wxID_ANY, wxT("Ok"));
  //hbox5->Add(btn1, 0);
  //wxButton *btn2 = new wxButton(panel, wxID_ANY, wxT("Close"));
  //hbox5->Add(btn2, 0, wxLEFT | wxBOTTOM , 5);
  //vbox->Add(hbox5, 0, wxALIGN_RIGHT | wxRIGHT, 10);

  //panel->SetSizer(vbox);

	//basePanel->SetSizer(mainVertBox);

	wxPanel *bottomPanel = new wxPanel(windowSplit, wxID_ANY);

	wxBoxSizer *bottomWindowSizer = new wxBoxSizer(wxVERTICAL);

	wxTextCtrl *editTextCtrl = new wxTextCtrl(bottomPanel, wxID_ANY, wxEmptyString, wxDefaultPosition, wxDefaultSize, wxTE_MULTILINE);

	bottomWindowSizer->Add(editTextCtrl, 1, wxEXPAND);

	topPanel->SetSizer(topVertBox);
	bottomPanel->SetSizer(bottomWindowSizer);

	windowSplit->SplitHorizontally(topPanel, bottomPanel, 100);
	windowSplit->SetMinimumPaneSize(50);
	windowSplit->SetSashGravity(0.0);

	Center();
    
}

void reNotedMainWin::loadListCtrl(int i)
{
	wxString buf;
	buf.Printf("This is list item %d", i);
	long tmp = fileListCtrl->InsertItem(i, buf, 0);
	fileListCtrl->SetItemData(tmp, i);

	buf.Printf("Col1 item %d", i);
	fileListCtrl->SetItem(tmp, 1, buf);

	buf.Printf("Item %d in second column", i);
	fileListCtrl->SetItem(tmp, 2, buf);	
}

void reNotedMainWin::setupListCtrlColumns()
{
	wxListItem itemCol;
	itemCol.SetText("Title");
	itemCol.SetImage(-1);
	fileListCtrl->InsertColumn(0, itemCol);

	itemCol.SetText("Column 2");
	itemCol.SetImage(-1);
	//itemCol.SetAlign(wxLIST_FORMAT_CENTER);
	fileListCtrl->InsertColumn(1, itemCol);

	itemCol.SetText("Column 3");
	itemCol.SetImage(-1);
	//itemCol.SetAlign(wxLIST_FORMAT_RIGHT);
	fileListCtrl->InsertColumn(2, itemCol);
}