using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


// Team members:
// Tim Claussen
// Syed Tabish Ahmed

public class MenuMaker
{
    //Variables
    //stores the base form
    public static Form targetMenu;
    //stores the playfieldcreator object
    public PlayfieldCreator pfc;
    //stores the filehelper object
    public FileHelper hlp;
    //strings for the solution, current uncompleted puzzle, etc
    public List<string> rawStringsFromFile;
    public List<string> puzzleStrings;
    public List<string> unsolved;
    public List<string> solution;
    public string fName;

    //default constructor takes the base form, and created Playfield and File helper objects
    public MenuMaker(Form trgt, PlayfieldCreator pfc_in, FileHelper fleHlp_in)
    {
        targetMenu = trgt;
        pfc = pfc_in;
        hlp = fleHlp_in;
    }

    //makes the dynamic menu form with buttons to each difficulty
    //input the base form to write on, and the playfield creator to use later
    public void MakeMenu(Form form, PlayfieldCreator pfc_1)
    {
        //set the form we are using for use in the events.
        targetMenu = form;
        pfc = pfc_1;
        //clear form, just in case I sleepily add stuff to base form
        PlayfieldCreator.ClearForm(form);
        //hlp.readDirectory();
        
        //Originally the code made by VS, form changed to playfield, but this copied code is used
        // to make the menu
        var MenuTitle = new System.Windows.Forms.Label();
        var NewEasyButton = new System.Windows.Forms.Button();
        var NewMedButton = new System.Windows.Forms.Button();
        var NewHardButton = new System.Windows.Forms.Button();
        var MenuLabel1 = new System.Windows.Forms.Label();
        var MenuLabel2 = new System.Windows.Forms.Label();
        var SavedEasyButton = new System.Windows.Forms.Button();
        var SavedMedButton = new System.Windows.Forms.Button();
        var SavedHardButton = new System.Windows.Forms.Button();
        
        // 
        // MenuTitle
        // 
        MenuTitle.AutoSize = true;
        MenuTitle.BackColor = System.Drawing.SystemColors.ControlLightLight;
        MenuTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        MenuTitle.Location = new System.Drawing.Point(29, 20);
        MenuTitle.Name = "MenuTitle";
        MenuTitle.Size = new System.Drawing.Size(454, 25);
        MenuTitle.TabIndex = 1;
        MenuTitle.Text = "Select a New Puzzle Or Continue a saved one";
        form.Controls.Add(MenuTitle);
        // 
        // NewEasyButton
        // 
        NewEasyButton.Location = new System.Drawing.Point(47, 347);
        NewEasyButton.Name = "NewEasyButton";
        NewEasyButton.Size = new System.Drawing.Size(98, 31);
        NewEasyButton.TabIndex = 2;
        NewEasyButton.Text = "Easy (3x3)";
        NewEasyButton.UseVisualStyleBackColor = true;
        form.Controls.Add(NewEasyButton);
        // 
        // NewMedButton
        // 
        NewMedButton.Location = new System.Drawing.Point(182, 347);
        NewMedButton.Name = "NewMedButton";
        NewMedButton.Size = new System.Drawing.Size(104, 31);
        NewMedButton.TabIndex = 3;
        NewMedButton.Text = "Medium (5x5)";
        NewMedButton.UseVisualStyleBackColor = true;
        form.Controls.Add(NewMedButton);
        // 
        // NewHardButton
        // 
        NewHardButton.Location = new System.Drawing.Point(324, 347);
        NewHardButton.Name = "NewHardButton";
        NewHardButton.Size = new System.Drawing.Size(110, 31);
        NewHardButton.TabIndex = 4;
        NewHardButton.Text = "Hard (7x7)";
        NewHardButton.UseVisualStyleBackColor = true;
        form.Controls.Add(NewHardButton);
        // 
        // MenuLabel1
        // 
        MenuLabel1.AutoSize = true;
        MenuLabel1.BackColor = System.Drawing.SystemColors.ControlLight;
        MenuLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        MenuLabel1.Location = new System.Drawing.Point(179, 300);
        MenuLabel1.Name = "MenuLabel1";
        MenuLabel1.Size = new System.Drawing.Size(122, 18);
        MenuLabel1.TabIndex = 5;
        MenuLabel1.Text = "Start New Puzzle";
        form.Controls.Add(MenuLabel1);
        // 
        // MenuLabel2
        // 
        MenuLabel2.AutoSize = true;
        MenuLabel2.BackColor = System.Drawing.SystemColors.ControlLightLight;
        MenuLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        MenuLabel2.Location = new System.Drawing.Point(53, 100);
        MenuLabel2.Name = "MenuLabel2";
        MenuLabel2.Size = new System.Drawing.Size(403, 18);
        MenuLabel2.TabIndex = 6;
        MenuLabel2.Text = "Resume Saved Puzzle (Starts New Puzzle if no saved exist)";
        form.Controls.Add(MenuLabel2);
        // 
        // SavedEasyButton
        // 
        SavedEasyButton.Location = new System.Drawing.Point(47, 153);
        SavedEasyButton.Name = "SavedEasyButton";
        SavedEasyButton.Size = new System.Drawing.Size(98, 31);
        SavedEasyButton.TabIndex = 7;
        SavedEasyButton.Text = "Easy (3x3)";
        SavedEasyButton.UseVisualStyleBackColor = true;
        form.Controls.Add(SavedEasyButton);
        // 
        // SavedMedButton
        // 
        SavedMedButton.Location = new System.Drawing.Point(182, 153);
        SavedMedButton.Name = "SavedMedButton";
        SavedMedButton.Size = new System.Drawing.Size(104, 31);
        SavedMedButton.TabIndex = 8;
        SavedMedButton.Text = "Medium (5x5)";
        SavedMedButton.UseVisualStyleBackColor = true;
        form.Controls.Add(SavedMedButton);
        // 
        // SavedHardButton
        // 
        SavedHardButton.Location = new System.Drawing.Point(324, 153);
        SavedHardButton.Name = "SavedHardButton";
        SavedHardButton.Size = new System.Drawing.Size(110, 31);
        SavedHardButton.TabIndex = 9;
        SavedHardButton.Text = "Hard (7x7)";
        SavedHardButton.UseVisualStyleBackColor = true;
        form.Controls.Add(SavedHardButton);
        // 
        // Form
        // 
        form.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        form.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        form.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
        form.ClientSize = new System.Drawing.Size(517, 460);
        form.Controls.Add(SavedHardButton);
        form.Controls.Add(SavedMedButton);
        form.Controls.Add(SavedEasyButton);
        form.Controls.Add(MenuLabel2);
        form.Controls.Add(MenuLabel1);
        form.Controls.Add(NewHardButton);
        form.Controls.Add(NewMedButton);
        form.Controls.Add(NewEasyButton);
        form.Controls.Add(MenuTitle);
        form.Name = "MenuForm";
        form.Text = "Sudoku Menu";
        form.ResumeLayout(false);
        form.PerformLayout();


        NewEasyButton.Click += new System.EventHandler(NewEasyButton_Click);
        NewMedButton.Click += new System.EventHandler(NewMedButton_Click);
        NewHardButton.Click += new System.EventHandler(NewHardButton_Click);
        SavedEasyButton.Click += new System.EventHandler(SavedEasyButton_Click);
        SavedMedButton.Click += new System.EventHandler(SavedMedButton_Click);
        SavedHardButton.Click += new System.EventHandler(SavedHardButton_Click);
    }

    //New and Easy button event handler, gets puzzle from random in the easy folder
    public void NewEasyButton_Click(object sender, EventArgs e)
    {
        //call populate the text boxes 
        rawStringsFromFile = hlp.getNewFile("easy");
        pfc.fileItComesFrom = hlp.getFileNameFromLists(rawStringsFromFile);
        //MessageBox.Show(fName);
        puzzleStrings = hlp.getPuzzleWithoutFileName(rawStringsFromFile);
        //MessageBox.Show();
        unsolved = FileHelper.getTheUnsolvedPart(puzzleStrings, 3);
        solution = FileHelper.getTheSolvedPart(puzzleStrings, 3);

        pfc.unsolved = unsolved;
        pfc.solution = solution;

        //MessageBox.Show("Button Pressed!");
        //look thru a5 for new puzzle, using file helper class
        pfc.createBaseForm(targetMenu);
        pfc.ChangePlayfieldToEasy(targetMenu, PlayfieldCreator.localBox);

    }

    //Saved and Easy button event handler, gets puzzle from random in the easy saved folder
    //if no saved games are there, just filehandler picks from the new ones
    public void SavedEasyButton_Click(object sender, EventArgs e)
    {
        rawStringsFromFile = hlp.getSavedFile("easy");
        pfc.fileItComesFrom = hlp.getFileNameFromLists(rawStringsFromFile);
        //MessageBox.Show(fName);
        puzzleStrings = hlp.getPuzzleWithoutFileName(rawStringsFromFile);
        //MessageBox.Show();
        unsolved = FileHelper.getTheUnsolvedPart(puzzleStrings, 3);
        solution = FileHelper.getTheSolvedPart(puzzleStrings, 3);

        pfc.unsolved = unsolved;
        pfc.solution = solution;

        //MessageBox.Show("Button Pressed!");
        //look thru a5 for new puzzle, using file helper class
        pfc.createBaseForm(targetMenu);
        pfc.ChangePlayfieldToEasy(targetMenu, PlayfieldCreator.localBox);
    }

    //Saved and Medium button event handler, gets puzzle from random in the Medium saved folder
    //if no saved games are there, just filehandler picks from the new ones
    public void SavedMedButton_Click(object sender, EventArgs e)
    {
        rawStringsFromFile = hlp.getSavedFile("medium");
        pfc.fileItComesFrom = hlp.getFileNameFromLists(rawStringsFromFile);
        //MessageBox.Show(fName);
        puzzleStrings = hlp.getPuzzleWithoutFileName(rawStringsFromFile);
        //MessageBox.Show();
        unsolved = FileHelper.getTheUnsolvedPart(puzzleStrings, 5);
        solution = FileHelper.getTheSolvedPart(puzzleStrings, 5);

        pfc.unsolved = unsolved;
        pfc.solution = solution;

        //MessageBox.Show("Button Pressed!");
        //look thru a5 for new puzzle, using file helper class
        pfc.createBaseForm(targetMenu);

        pfc.ChangePlayfieldToMed(targetMenu, PlayfieldCreator.localBox);
    }

    //Saved and Hard button event handler, gets puzzle from random in the Hard saved folder
    //if no saved games are there, just filehandler picks from the new ones
    public void SavedHardButton_Click(object sender, EventArgs e)
    {
        rawStringsFromFile = hlp.getSavedFile("hard");
        pfc.fileItComesFrom = hlp.getFileNameFromLists(rawStringsFromFile);
        //MessageBox.Show(fName);
        puzzleStrings = hlp.getPuzzleWithoutFileName(rawStringsFromFile);
        //MessageBox.Show();
        unsolved = FileHelper.getTheUnsolvedPart(puzzleStrings, 7);
        solution = FileHelper.getTheSolvedPart(puzzleStrings, 7);

        pfc.unsolved = unsolved;
        pfc.solution = solution;
        pfc.createBaseForm(targetMenu);
        pfc.ChangePlayfieldToHard(targetMenu, PlayfieldCreator.localBox);
    }

    //New and Medium button event handler, gets puzzle from random in the Medium folder
    public void NewMedButton_Click(object sender, EventArgs e)
    {
        //MessageBox.Show("Button Pressed!");
        rawStringsFromFile = hlp.getNewFile("medium");
        pfc.fileItComesFrom = hlp.getFileNameFromLists(rawStringsFromFile);
        //MessageBox.Show(fName);
        puzzleStrings = hlp.getPuzzleWithoutFileName(rawStringsFromFile);
        //MessageBox.Show();
        unsolved = FileHelper.getTheUnsolvedPart(puzzleStrings, 5);
        solution = FileHelper.getTheSolvedPart(puzzleStrings, 5);

        pfc.unsolved = unsolved;
        pfc.solution = solution;

        pfc.createBaseForm(targetMenu);
        pfc.ChangePlayfieldToMed(targetMenu, PlayfieldCreator.localBox);
    }

    //New and hard button event handler, gets puzzle from random in the hard folder
    public void NewHardButton_Click(object sender, EventArgs e)
    {
        //MessageBox.Show("Button Pressed!");
        rawStringsFromFile = hlp.getNewFile("hard");
        pfc.fileItComesFrom = hlp.getFileNameFromLists(rawStringsFromFile);
        //MessageBox.Show(fName);
        puzzleStrings = hlp.getPuzzleWithoutFileName(rawStringsFromFile);
        //MessageBox.Show();
        unsolved = FileHelper.getTheUnsolvedPart(puzzleStrings, 7);
        solution = FileHelper.getTheSolvedPart(puzzleStrings, 7);

        pfc.unsolved = unsolved;
        pfc.solution = solution;
        pfc.createBaseForm(targetMenu);
        pfc.ChangePlayfieldToHard(targetMenu, PlayfieldCreator.localBox);
        
    }
}
