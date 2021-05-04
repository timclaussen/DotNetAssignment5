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

//Class to create the playfield, handle game
//lots of variables, but names are reused
public class PlayfieldCreator
{
    public static Form targetPlay;
    public static PictureBox localBox;
    public PictureBox localBoxPaused;
    public static Bitmap bm;
    public static float rightBoundary;
    public static float bottomBoundary;
    public static int playLen = 0;
    public int lastKeyhit;
    public List<string> unsolved;
    public List<string> solution;
    public List<string> tempSave;
    public int correctSum = 0;
    public bool playfieldInit = true;
    public int timeElapsed = 0;
    public string fileItComesFrom = "";
    //make the text boxes available to the rest of the context
    //all 7 rows, 7 columns...
    //not very clean, but these get reused for each difficulty
    #region
    public TextBox textBox1_1;
    public TextBox textBox1_2;
    public TextBox textBox1_3;
    public TextBox textBox1_4;
    public TextBox textBox1_5;
    public TextBox textBox1_6;
    public TextBox textBox1_7;

    public TextBox textBox2_1;
    public TextBox textBox2_2;
    public TextBox textBox2_3;
    public TextBox textBox2_4;
    public TextBox textBox2_5;
    public TextBox textBox2_6;
    public TextBox textBox2_7;

    public TextBox textBox3_1;
    public TextBox textBox3_2;
    public TextBox textBox3_3;
    public TextBox textBox3_4;
    public TextBox textBox3_5;
    public TextBox textBox3_6;
    public TextBox textBox3_7;

    public TextBox textBox4_1;
    public TextBox textBox4_2;
    public TextBox textBox4_3;
    public TextBox textBox4_4;
    public TextBox textBox4_5;
    public TextBox textBox4_6;
    public TextBox textBox4_7;

    public TextBox textBox5_1;
    public TextBox textBox5_2;
    public TextBox textBox5_3;
    public TextBox textBox5_4;
    public TextBox textBox5_5;
    public TextBox textBox5_6;
    public TextBox textBox5_7;

    public TextBox textBox6_1;
    public TextBox textBox6_2;
    public TextBox textBox6_3;
    public TextBox textBox6_4;
    public TextBox textBox6_5;
    public TextBox textBox6_6;
    public TextBox textBox6_7;

    public TextBox textBox7_1;
    public TextBox textBox7_2;
    public TextBox textBox7_3;
    public TextBox textBox7_4;
    public TextBox textBox7_5;
    public TextBox textBox7_6;
    public TextBox textBox7_7;
    #endregion

    //And a helper variable for keeping which textbox was clicked last
    public TextBox selectedTxtBx;

    //7 row sums label, 7 column sums label, and associated correct sums
    #region
    public static Label row1Sum;
    public static Label row2Sum;
    public static Label row3Sum;
    public static Label row4Sum;
    public static Label row5Sum;
    public static Label row6Sum;
    public static Label row7Sum;
    public static Label col1Sum;
    public static Label col2Sum;
    public static Label col3Sum;
    public static Label col4Sum;
    public static Label col5Sum;
    public static Label col6Sum;
    public static Label col7Sum;


    #endregion

    //7 row correct sums, 7 column correct sums
    #region
    public static int row1SumCorr = 0;
    public static int row2SumCorr = 0;
    public static int row3SumCorr = 0;
    public static int row4SumCorr = 0;
    public static int row5SumCorr = 0;
    public static int row6SumCorr = 0;
    public static int row7SumCorr = 0;
    public static int col1SumCorr = 0;
    public static int col2SumCorr = 0;
    public static int col3SumCorr = 0;
    public static int col4SumCorr = 0;
    public static int col5SumCorr = 0;
    public static int col6SumCorr = 0;
    public static int col7SumCorr = 0;
    #endregion
    
    //boolean flags
    public bool listenToKeyPress = false;
    public bool isPaused = false;
    public bool gotHint = false;

    //global controls for events to mess with
    PictureBox pictureBox1 = new System.Windows.Forms.PictureBox();
    PictureBox pictureBox2 = new System.Windows.Forms.PictureBox();
    Label TimeLabel1 = new System.Windows.Forms.Label();

    public Timer playTime;

    //default constructor makes empty objects for later
    public PlayfieldCreator()
    {
        //we wait for menu to call create base form
        selectedTxtBx = new TextBox(); //setting selected textbox to an empty one to prevent null references later
        unsolved = new List<string>(); //empty for now
        solution = new List<string>();
        tempSave = new List<string>(); //empty for now
        playTime = new Timer();
        playTime.Interval = 1000;


    }

    //this creates the base form, buttons, and play area for us to make the game in
    public void createBaseForm(Form form)
    {
        //set local form to app form, for use in this context
        targetPlay = form;

        //clear the menu
        ClearForm(form);

        //make same general form containing the playfield
        //var pictureBox1 = new System.Windows.Forms.PictureBox();
        //var pictureBox2 = new System.Windows.Forms.PictureBox();
        var ProgressButton1 = new System.Windows.Forms.Button();
        var PauseButton = new System.Windows.Forms.Button();
        var HintButton = new System.Windows.Forms.Button();
        var SaveButton = new System.Windows.Forms.Button();
        var SaveAndQuitbutton5 = new System.Windows.Forms.Button();
        var ResetButton = new System.Windows.Forms.Button();
        //var TimeLabel1 = new System.Windows.Forms.Label();
        //var ProgressLabel = new System.Windows.Forms.Label();
        ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();
        form.SuspendLayout();
        // 
        // pictureBox1
        // 
        pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
        pictureBox1.Location = new System.Drawing.Point(31, 25);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new System.Drawing.Size(500, 500);
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        pictureBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(pictureBox1_KeyDown);

        // 
        // pictureBox2
        // 
        pictureBox2.BackColor = System.Drawing.SystemColors.ControlLightLight;
        pictureBox2.Location = new System.Drawing.Point(31, 25);
        pictureBox2.Name = "pictureBox2";
        pictureBox2.Size = new System.Drawing.Size(500, 500);
        pictureBox2.TabIndex = 0;
        pictureBox2.TabStop = false;
        pictureBox2.Hide();
        //pictureBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(pictureBox1_KeyDown);
        // 
        // ProgressButton1
        // 
        ProgressButton1.Location = new System.Drawing.Point(594, 208);
        ProgressButton1.Name = "ProgressButton1";
        ProgressButton1.Size = new System.Drawing.Size(100, 35);
        ProgressButton1.TabIndex = 1;
        ProgressButton1.Text = "Progress";
        ProgressButton1.UseVisualStyleBackColor = true;
        ProgressButton1.Click += new System.EventHandler(ProgressButton1_Click);
        // 
        // PauseButton
        // 
        PauseButton.Location = new System.Drawing.Point(719, 208);
        PauseButton.Name = "PauseButton";
        PauseButton.Size = new System.Drawing.Size(100, 35);
        PauseButton.TabIndex = 2;
        PauseButton.Text = "Pause";
        PauseButton.UseVisualStyleBackColor = true;
        PauseButton.Click += new System.EventHandler(PauseButton_Click);
        // 
        // HintButton
        // 
        HintButton.Location = new System.Drawing.Point(594, 301);
        HintButton.Name = "HintButton";
        HintButton.Size = new System.Drawing.Size(100, 35);
        HintButton.TabIndex = 3;
        HintButton.Text = "Hint";
        HintButton.UseVisualStyleBackColor = true;
        HintButton.Click += new System.EventHandler(HintButton_Click);
        // 
        // SaveButton
        // 
        SaveButton.Location = new System.Drawing.Point(719, 301);
        SaveButton.Name = "SaveButton";
        SaveButton.Size = new System.Drawing.Size(100, 35);
        SaveButton.TabIndex = 4;
        SaveButton.Text = "Save";
        SaveButton.UseVisualStyleBackColor = true;
        SaveButton.Click += new System.EventHandler(SaveButton_Click);
        // 
        // SaveAndQuitbutton5
        // 
        SaveAndQuitbutton5.Location = new System.Drawing.Point(719, 590);
        SaveAndQuitbutton5.Name = "SaveAndQuitbutton5";
        SaveAndQuitbutton5.Size = new System.Drawing.Size(100, 35);
        SaveAndQuitbutton5.TabIndex = 5;
        SaveAndQuitbutton5.Text = "Save And Quit";
        SaveAndQuitbutton5.UseVisualStyleBackColor = true;
        SaveAndQuitbutton5.Click += new System.EventHandler(SaveAndQuitbutton5_Click);
        // 
        // SaveAndQuitbutton5
        // 
        ResetButton.Location = new System.Drawing.Point(620, 590);
        ResetButton.Name = "ResetButton";
        ResetButton.Size = new System.Drawing.Size(100, 35);
        ResetButton.TabIndex = 15;
        ResetButton.Text = "Reset";
        ResetButton.UseVisualStyleBackColor = true;
        ResetButton.Click += new System.EventHandler(Reset_Click);

        // 
        // TimeLabel1
        // 
        TimeLabel1.AutoSize = true;
        TimeLabel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
        TimeLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        TimeLabel1.Location = new System.Drawing.Point(591, 137);
        TimeLabel1.Name = "TimeLabel1";
        TimeLabel1.Size = new System.Drawing.Size(42, 16);
        TimeLabel1.TabIndex = 6;
        TimeLabel1.Text = "Time:";
        // 
        // Form1
        // 
        form.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        form.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        form.BackColor = System.Drawing.SystemColors.ActiveBorder;
        form.ClientSize = new System.Drawing.Size(884, 661);
        //form.Controls.Add(ProgressLabel);
        form.Controls.Add(TimeLabel1);
        form.Controls.Add(SaveAndQuitbutton5);
        form.Controls.Add(SaveButton);
        form.Controls.Add(HintButton);
        form.Controls.Add(PauseButton);
        form.Controls.Add(ResetButton);
        form.Controls.Add(ProgressButton1);
        form.Controls.Add(pictureBox2);
        form.Controls.Add(pictureBox1);
        form.Name = "Form1";
        form.Text = "Sudoku";
        ((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
        form.ResumeLayout(false);
        form.PerformLayout();

        localBox = pictureBox1; // set local context picture box for use later

        //init the picturebox
        bm = new System.Drawing.Bitmap(pictureBox1.Width, pictureBox1.Height);
        pictureBox1.Image = bm;

       // localBoxPaused.Image = null; //erases the image
        //localBoxPaused.Image = new Bitmap(box.Width, box.Height);

        rightBoundary = (27.0f / 30.0f) * (float)pictureBox1.Width;
        bottomBoundary = (27.0f / 30.0f) * (float)pictureBox1.Height;

        playTime.Tick += new EventHandler(TimerEventProcessor);
        playTime.Start();
        DrawBasePlayField(form, pictureBox1);


    }

    //reset button clicked, instead of clearing everything we just repopulate from the unsolved list
    private void Reset_Click(object sender, EventArgs e)
    {
        //throw new NotImplementedException();
        //ClearPictureBox(targetPlay, localBox);
        populatePlayField(targetPlay, localBox, playLen);

    }


    //timer event called on every tick
    private void TimerEventProcessor(object sender, EventArgs e)
    {
        //throw new NotImplementedException();
        timeElapsed++;
        TimeLabel1.Text = "Time: " + timeElapsed.ToString();


    }

    //collecting key presses in the play area
    private void pictureBox1_KeyDown(object sender, KeyPressEventArgs e)
    {
        // MessageBox.Show("KeyDown Event");
        int i;
       //if(e.k > 0)
       if(int.TryParse(e.KeyChar.ToString(), out i))
        {
            lastKeyhit = i;
            //MessageBox.Show("KeyDown Event");
            if (lastKeyhit == 0)
            {
                selectedTxtBx.Text = "";
                //MessageBox.Show("Last Key hit is 0");
            }
            else
            {
                //int inNum = Pfc.lastKeyhit;
                //set the textbox to that number
                //MessageBox.Show("Last Key hit is " + lastKeyhit.ToString());
                selectedTxtBx.Text = lastKeyhit.ToString();
            }
            //Call the sum function
            ComputeAllSum();
        }
    }


    //clears the playfield picture box
    //input form it is contained in, and the picturebox to clear
    public static void ClearPictureBox(Form form, PictureBox box)
    {
        box.Image = null; //erases the image
        box.Image = new Bitmap(box.Width, box.Height);

        //redraw it
        DrawBasePlayField(form, box);
    }

    //draws the base play area, the surrounding lines and sum area
    //input form it is contained in, and the picturebox to draw in
    #region
    public static void DrawBasePlayField(Form form, PictureBox box)
    {

        Color black = Color.Black;
        Pen pineapple = new Pen(black, 0);
        SolidBrush tickBrush = new SolidBrush(Color.Black);
        Font drawFont = new Font("Arial", 9);
        //make the graphics
        //draw the playfield with a margin 1/5 of the len on the bottom and right side for the sums
        using (var g = Graphics.FromImage(box.Image))
        {
            //3 points, [(4/5)w, 0] & [0, (4/5)h] and common endpoint [(4/5)w, (4/5h)]
            PointF px = new PointF(rightBoundary, 0); //top right boundary of the play field
            PointF py = new PointF(0, bottomBoundary); //bottom left point
            PointF pp = new PointF(rightBoundary, bottomBoundary); //shared bottom right point
            PointF pS = new PointF(rightBoundary + 10.0f, (float)box.Height - 20.0f);


            g.DrawLine(pineapple, px, pp);
            g.DrawLine(pineapple, py, pp);


            //Label the sum area

            g.DrawString("Sum", drawFont, tickBrush, pS);
        }

        pineapple.Dispose();
    }
    #endregion
    //clears all controls from the inputted form using the clear method
    //input the form to clear
    public static void ClearForm(Form form)
    {
        form.Controls.Clear();

    }

    //event handler for progress button hit. Compares each textbox with the appropriate character in the solution
    #region
    private void ProgressButton1_Click(object sender, EventArgs e)
    {
        //loop thru text boxes
        int xPos = 1;
        int yPos = 1;
        string tempStr;
        TextBox tb;

        if (solution.Count < 1)
        {
            //MessageBox.Show("Return Called");
            return; //empty string list, no need to continue
        }

        //loop thru textboxes
        foreach (Control t in localBox.Controls)
        {
            //MessageBox.Show("Control " + t.Name + " was fouind");
            if (t is TextBox)
            {
                //MessageBox.Show("Found Textbox");
                tb = t as TextBox;
                tempStr = FileHelper.getTheRow(solution, yPos);
                tempStr = FileHelper.getElement(tempStr, xPos);
                //tb.Text = tempStr;
                //Do the check here
                if(!isTextBoxCorrect(tb, Convert.ToInt32(tempStr)))
                {
                    //wrong box found
                    //ProgressLabel.
                    MessageBox.Show("Wrong number in cell " + xPos.ToString() + "x" + yPos.ToString());
                    return;
                }

                if (xPos == playLen)
                {
                    yPos++;//end of row, go to next y value
                    xPos = 1;
                }
                else { xPos++; }
            }

        }
        MessageBox.Show("Doing good so far!");

    }
    #endregion
    //event handler for the pause button. Brings up a blank picturebox over the play area picturebox
    private void PauseButton_Click(object sender, EventArgs e)
    {
        Button tempButton = sender as Button;
        PictureBox tempBox = localBox; 
        if (!isPaused)
        {
            playTime.Stop();
            tempButton.Text = "Resume";
            isPaused = true;
            pictureBox2.Visible = true;
            //pictureBox2.


        }
        else
        {
            //unpause\
            tempButton.Text = "Pause";
            pictureBox2.Hide();
            isPaused = false;
            playTime.Start();
        }

    }

    //event handler for the hint button, uses the solution to correct the first incorrect or blank textbox
    //searches from left to right, starting at the top left
    #region
    private void HintButton_Click(object sender, EventArgs e)
    {
        gotHint = true;
        //loop thru text boxes
        int xPos = 1;
        int yPos = 1;
        string tempStr;
        TextBox tb;

        if (solution.Count < 1)
        {
            //MessageBox.Show("Return Called");
            return; //empty string list, no need to continue
        }

        //loop thru textboxes
        foreach (Control t in localBox.Controls)
        {
            //MessageBox.Show("Control " + t.Name + " was fouind");
            if (t is TextBox)
            {
                //MessageBox.Show("Found Textbox");
                tb = t as TextBox;
                tempStr = FileHelper.getTheRow(solution, yPos);
                tempStr = FileHelper.getElement(tempStr, xPos);
                //tb.Text = tempStr;
                //Do the check here
                if (!isTextBoxCorrect(tb, Convert.ToInt32(tempStr)) || tb.Text == "")
                {
                    //wrong box found
                    tb.Text = tempStr;
                    ComputeAllSum();
                    return;
                }

                if (xPos == playLen)
                {
                    yPos++;//end of row, go to next y value
                    xPos = 1;
                }
                else { xPos++; }
            }

        }

    }
    #endregion

    //Event handler for the save button, saves the current playfield and solution to a saved file in a different directory 
    //Does not exit the game
    private void SaveButton_Click(object sender, EventArgs e)
    {
        //save to file in saved folder

        //update saved strings, but only write to file on save and quit
        saveNumbersFromPlayfield(localBox);
        FileHelper.savePuzzle(fileItComesFrom, tempSave, solution);
        MessageBox.Show("Saved!");
    }

    //Event handler, same as the save button, but also quits the game
    private void SaveAndQuitbutton5_Click(object sender, EventArgs e)
    {
        //make dialog, are you sure you want to quit?
        //MessageBox.Show("Saved!");
        //update saved strings, but only write to file on save and quit
        saveNumbersFromPlayfield(localBox);
        FileHelper.savePuzzle(fileItComesFrom, tempSave, solution);
        MessageBox.Show("Saved!");
        targetPlay.Close(); // exit
    }

    //Make the 3x3 lined box with large font text boxes inside
    //input the form to put the controls are in, and the picturebox we are drawing in
    #region
    public void ChangePlayfieldToEasy(Form form, PictureBox box)
    {
        Color black = Color.Black;
        Pen pineapple = new Pen(black, 0);
        playLen = 3; //set global for rest of functions
        //MessageBox.Show("file it came from: " + fileItComesFrom);
        // 1/3 for until the play area
        float horzDiv = bottomBoundary / 3.0f; // Lines divide the field in 3
        int yTextP = Convert.ToInt32(horzDiv / 2.5f); //Center the text boxes in the division
        float vertDiv = rightBoundary / 3.0f;
        int xTextP = Convert.ToInt32(vertDiv / 3.3f); // numbers found by trail and error

        int horzDivInt = Convert.ToInt32(horzDiv); //convert to int for help with textbox setting
        int vertDivInt = Convert.ToInt32(vertDiv);
        //Horizontal first
        PointF px1_1 = new PointF(0, horzDiv);
        PointF px1_2 = new PointF(rightBoundary, horzDiv);

        PointF px2_1 = new PointF(0, horzDiv * 2);
        PointF px2_2 = new PointF(rightBoundary, horzDiv * 2);

        //Vertical lines

        PointF py1_1 = new PointF(vertDiv, 0);
        PointF py1_2 = new PointF(vertDiv, bottomBoundary);

        PointF py2_1 = new PointF(vertDiv * 2, 0);
        PointF py2_2 = new PointF(vertDiv * 2, bottomBoundary);

        using (var g = Graphics.FromImage(box.Image))
        {
            //draw horizontal
            g.DrawLine(pineapple, px1_1, px1_2);
            g.DrawLine(pineapple, px2_1, px2_2);

            g.DrawLine(pineapple, py1_1, py1_2);
            g.DrawLine(pineapple, py2_1, py2_2);
        }

        //textBox1.Size = new System.Drawing.Size((horzDivInt / 2) - 10, vertDivInt / 2);
        //textBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
        //textBox1.Multiline = true;

        //Add text boxes, 3 per row, PointText(row#)_(column#)
        //Row 1, column 1
        Point pT1_1 = new Point(xTextP, yTextP);
        textBox1_1 = new TextBox();
        textBoxCreator(box, textBox1_1, pT1_1);
        textBoxEasySizeSetter(textBox1_1, horzDivInt, vertDivInt);

        //add text changed event handlers to update the SUM of the row
        textBox1_1.TextChanged += new System.EventHandler(textBox1_1_TextChanged);

        //Row 1, column 2
        Point pT1_2 = new Point(xTextP + vertDivInt, yTextP);
        textBox1_2 = new TextBox();
        textBoxCreator(box, textBox1_2, pT1_2);
        textBoxEasySizeSetter(textBox1_2, horzDivInt, vertDivInt);

        textBox1_2.TextChanged += new System.EventHandler(textBox1_2_TextChanged);

        //Row 1, column 3
        Point pT1_3 = new Point(xTextP + vertDivInt * 2, yTextP);
        textBox1_3 = new TextBox();
        textBoxCreator(box, textBox1_3, pT1_3);
        textBoxEasySizeSetter(textBox1_3, horzDivInt, vertDivInt);

        textBox1_3.TextChanged += new System.EventHandler(textBox1_3_TextChanged);

        //Row 2, column 1
        Point pT2_1 = new Point(xTextP, yTextP + horzDivInt);
        textBox2_1 = new TextBox();
        textBoxCreator(box, textBox2_1, pT2_1);
        textBoxEasySizeSetter(textBox2_1, horzDivInt, vertDivInt);

        textBox2_1.TextChanged += new System.EventHandler(textBox2_1_TextChanged);

        //Row 2, column 2
        Point pT2_2 = new Point(xTextP + vertDivInt, yTextP + horzDivInt);
        textBox2_2 = new TextBox();
        textBoxCreator(box, textBox2_2, pT2_2);
        textBoxEasySizeSetter(textBox2_2, horzDivInt, vertDivInt);

        textBox2_2.TextChanged += new System.EventHandler(textBox2_2_TextChanged);

        //Row 2, column 3
        Point pT2_3 = new Point(xTextP + vertDivInt * 2, yTextP + horzDivInt);
        textBox2_3 = new TextBox();
        textBoxCreator(box, textBox2_3, pT2_3);
        textBoxEasySizeSetter(textBox2_3, horzDivInt, vertDivInt);

        textBox2_3.TextChanged += new System.EventHandler(textBox2_3_TextChanged);

        //Row 3, column 1
        Point pT3_1 = new Point(xTextP, yTextP + horzDivInt * 2);
        textBox3_1 = new TextBox();
        textBoxCreator(box, textBox3_1, pT3_1);
        textBoxEasySizeSetter(textBox3_1, horzDivInt, vertDivInt);

        textBox3_1.TextChanged += new System.EventHandler(textBox3_1_TextChanged);

        //Row 3, column 2
        Point pT3_2 = new Point(xTextP + vertDivInt, yTextP + horzDivInt * 2);
        textBox3_2 = new TextBox();
        textBoxCreator(box, textBox3_2, pT3_2);
        textBoxEasySizeSetter(textBox3_2, horzDivInt, vertDivInt);

        textBox3_2.TextChanged += new System.EventHandler(textBox3_2_TextChanged);

        //Row 3, column 3
        Point pT3_3 = new Point(xTextP + vertDivInt * 2, yTextP + horzDivInt * 2);
        textBox3_3 = new TextBox();
        textBoxCreator(box, textBox3_3, pT3_3);
        textBoxEasySizeSetter(textBox3_3, horzDivInt, vertDivInt);

        textBox3_3.TextChanged += new System.EventHandler(textBox3_3_TextChanged);


        //populate the textboxes IN THE MENU MAKER, they choose what to load
        populatePlayField(targetPlay, box, 3);
        //populatePlayField(form, box, 3, "123456789");



        //Add sum labels to playfield
        #region
        row1Sum = new Label();
        Point rS1 = new Point((int)box.Width - 30, yTextP);
        sumSetter(box, row1Sum, rS1, "row1Sum");


        row2Sum = new Label();
        Point rS2 = new Point((int)box.Width - 30, yTextP + horzDivInt);
        sumSetter(box, row2Sum, rS2, "row2Sum");

        row3Sum = new Label();
        Point rS3 = new Point((int)box.Width - 30, yTextP + horzDivInt * 2);
        sumSetter(box, row3Sum, rS3, "row3Sum");

        col1Sum = new Label();
        Point cS1 = new Point(30 + xTextP, (int)box.Height - 30);
        sumSetter(box, col1Sum, cS1, "col1Sum");

        col2Sum = new Label();
        Point cS2 = new Point(30 + xTextP + vertDivInt, (int)box.Height - 30);
        sumSetter(box, col2Sum, cS2, "col2Sum");

        col3Sum = new Label();
        Point cS3 = new Point(30 + xTextP + vertDivInt * 2, (int)box.Height - 30);
        sumSetter(box, col3Sum, cS3, "col3Sum");

        #endregion

        //calc sum when populating the thingy

        //after populating and adding them
        ComputeAllSum();

        pineapple.Dispose();
    }
    #endregion

    //sets the sum label and adds the control to the box
    //input where the label will be set, the Picturebox to set it in, the name to give it, and the label object
    #region
    public static void sumSetter(PictureBox box, Label l, Point location, string name)
    {
        l.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        l.Location = location;
        l.Name = name;
        l.Size = new System.Drawing.Size(42, 16);
        l.Text = "Txt";
        box.Controls.Add(l);
    }
    #endregion

    //Sets the common parts of the textboxes, adds control to the picturebox
    //input the picture box to set the control to, the textbox object, and the point where to put it
    public void textBoxCreator(PictureBox box, TextBox tb, Point loc)
    {
        tb.Location = loc;
        tb.BringToFront();
        tb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        tb.Click += new System.EventHandler(tb_Click);
        //tb.KeyDown += new System.Windows.Forms.KeyEventHandler(tb_KeyDown);
        box.Controls.Add(tb);

    }

    //quick function to leave the game
    public void EscapePlayfield()
    {
        //do the same code as save and quit
        saveNumbersFromPlayfield(localBox);
        FileHelper.savePuzzle(fileItComesFrom, tempSave, solution);
        MessageBox.Show("Saved!");
        targetPlay.Close(); // exit
    }



    //textbox clicked on event, generally pull focus away from textbox to prevent blinking I-cursor
    private void tb_Click(object sender, EventArgs e)
    {
        //targetPlay.Controls.Find("SaveButton", true).Focus();
        //try to find out what textbox they clicked,
        selectedTxtBx = (TextBox)sender;

        localBox.Focus(); //cause textbox to lose the focus
                          

        listenToKeyPress = true;
        
    }

    //Sets the textbox to the size for the easy difficulty
    //input the textbox object, the horizontal and vertical size factors
    public static void textBoxEasySizeSetter(TextBox tb, int hDI, int vDI)
    {
        tb.Size = new System.Drawing.Size((hDI / 2) - 10, vDI / 2);
        tb.Font = new Font("Arial", 24);
    }

    //Sets the textbox to the size for the medium difficulty
    //input the textbox object, the horizontal and vertical size factors
    public static void textBoxMedSizeSetter(TextBox tb, int hDI, int vDI)
    {
        tb.Size = new System.Drawing.Size((hDI / 2) - 10, vDI / 2);
        tb.Font = new Font("Arial", 20);

    }

    //Sets the textbox to the size for the hard difficulty
    //input the textbox object, the horizontal and vertical size factors
    public static void textBoxHardSizeSetter(TextBox tb, int hDI, int vDI)
    {
        tb.Size = new System.Drawing.Size((hDI / 2) - 10, vDI / 2);
        tb.Font = new Font("Arial", 18);
    }

    //make 5x5 lined box with medium font text boxes inside
    //input the form to put the controls are in, and the picturebox we are drawing in
    #region
    public void ChangePlayfieldToMed(Form form, PictureBox box)
	{
        Color black = Color.Black;
        Pen pineapple = new Pen(black, 0);
        playLen = 5; //set global for rest of functions
        // 1/5 for until the play area
        float horzDiv = bottomBoundary / 5.0f;
        float vertDiv = rightBoundary / 5.0f;
        int yTextP = Convert.ToInt32(horzDiv / 3.3f); //Center the text boxes in the division
        int xTextP = Convert.ToInt32(vertDiv / 3.3f); // numbers found by trail and error

        int horzDivInt = Convert.ToInt32(horzDiv); //convert to int for help with textbox setting
        int vertDivInt = Convert.ToInt32(vertDiv);
        
        //Horizontal first
        //first row
        PointF px1_1 = new PointF(0, horzDiv);
        PointF px1_2 = new PointF(rightBoundary, horzDiv);

        //2nd row
        PointF px2_1 = new PointF(0, horzDiv * 2);
        PointF px2_2 = new PointF(rightBoundary, horzDiv * 2);

        //3rd row
        PointF px3_1 = new PointF(0, horzDiv * 3);
        PointF px3_2 = new PointF(rightBoundary, horzDiv * 3);

        //4th
        PointF px4_1 = new PointF(0, horzDiv * 4);
        PointF px4_2 = new PointF(rightBoundary, horzDiv * 4);

        //Vertical lines

        PointF py1_1 = new PointF(vertDiv, 0);
        PointF py1_2 = new PointF(vertDiv, bottomBoundary);

        PointF py2_1 = new PointF(vertDiv * 2, 0);
        PointF py2_2 = new PointF(vertDiv * 2, bottomBoundary);

        PointF py3_1 = new PointF(vertDiv * 3, 0);
        PointF py3_2 = new PointF(vertDiv * 3, bottomBoundary);

        PointF py4_1 = new PointF(vertDiv * 4, 0);
        PointF py4_2 = new PointF(vertDiv * 4, bottomBoundary);



        using (var g = Graphics.FromImage(box.Image))
        {
            //draw horizontal
            g.DrawLine(pineapple, px1_1, px1_2);
            g.DrawLine(pineapple, px2_1, px2_2);
            g.DrawLine(pineapple, px3_1, px3_2);
            g.DrawLine(pineapple, px4_1, px4_2);


            //draw vertical
            g.DrawLine(pineapple, py1_1, py1_2);
            g.DrawLine(pineapple, py2_1, py2_2);
            g.DrawLine(pineapple, py3_1, py3_2);
            g.DrawLine(pineapple, py4_1, py4_2);

        }

        //Add text boxes, 5 per row, PointText(row#)_(column#)
        //Row1
        #region
        //Row 1, column 1
        Point pT1_1 = new Point(xTextP, yTextP);
        textBox1_1 = new TextBox();
        textBoxCreator(box, textBox1_1, pT1_1);
        textBoxMedSizeSetter(textBox1_1, horzDivInt, vertDivInt);

        //add text changed event handlers to update the SUM of the row
        textBox1_1.TextChanged += new System.EventHandler(textBox1_1_TextChanged);

        //Row 1, column 2
        Point pT1_2 = new Point(xTextP + vertDivInt, yTextP);
        textBox1_2 = new TextBox();
        textBoxCreator(box, textBox1_2, pT1_2);
        textBoxMedSizeSetter(textBox1_2, horzDivInt, vertDivInt);

        textBox1_2.TextChanged += new System.EventHandler(textBox1_2_TextChanged);

        //Row 1, column 3
        Point pT1_3 = new Point(xTextP + vertDivInt * 2, yTextP);
        textBox1_3 = new TextBox();
        textBoxCreator(box, textBox1_3, pT1_3);
        textBoxMedSizeSetter(textBox1_3, horzDivInt, vertDivInt);

        textBox1_3.TextChanged += new System.EventHandler(textBox1_3_TextChanged);

        //Row 1, column 4
        Point pT1_4 = new Point(xTextP + vertDivInt * 3, yTextP);
        textBox1_4 = new TextBox();
        textBoxCreator(box, textBox1_4, pT1_4);
        textBoxMedSizeSetter(textBox1_4, horzDivInt, vertDivInt);

        textBox1_4.TextChanged += new System.EventHandler(textBox1_4_TextChanged);

        //Row 1, column 5
        Point pT1_5 = new Point(xTextP + vertDivInt * 4, yTextP);
        textBox1_5 = new TextBox();
        textBoxCreator(box, textBox1_5, pT1_5);
        textBoxMedSizeSetter(textBox1_5, horzDivInt, vertDivInt);

        textBox1_5.TextChanged += new System.EventHandler(textBox1_5_TextChanged);
        #endregion

        //Row 2
        #region
        //Row 2, column 1
        Point pT2_1 = new Point(xTextP, yTextP + horzDivInt);
        textBox2_1 = new TextBox();
        textBoxCreator(box, textBox2_1, pT2_1);
        textBoxMedSizeSetter(textBox2_1, horzDivInt, vertDivInt);

        textBox2_1.TextChanged += new System.EventHandler(textBox2_1_TextChanged);


        //Row 2, column 2
        Point pT2_2 = new Point(xTextP + vertDivInt, yTextP + horzDivInt);
        textBox2_2 = new TextBox();
        textBoxCreator(box, textBox2_2, pT2_2);
        textBoxMedSizeSetter(textBox2_2, horzDivInt, vertDivInt);

        textBox2_2.TextChanged += new System.EventHandler(textBox2_2_TextChanged);

        //Row 2, column 3
        Point pT2_3 = new Point(xTextP + vertDivInt * 2, yTextP + horzDivInt);
        textBox2_3 = new TextBox();
        textBoxCreator(box, textBox2_3, pT2_3);
        textBoxMedSizeSetter(textBox2_3, horzDivInt, vertDivInt);

        textBox2_3.TextChanged += new System.EventHandler(textBox2_3_TextChanged);

        //Row 2, column 4
        Point pT2_4 = new Point(xTextP + vertDivInt * 3, yTextP + horzDivInt);
        textBox2_4 = new TextBox();
        textBoxCreator(box, textBox2_4, pT2_4);
        textBoxMedSizeSetter(textBox2_4, horzDivInt, vertDivInt);

        textBox2_4.TextChanged += new System.EventHandler(textBox2_4_TextChanged);

        //Row 2, column 5
        Point pT2_5 = new Point(xTextP + vertDivInt * 4, yTextP + horzDivInt);
        textBox2_5 = new TextBox();
        textBoxCreator(box, textBox2_5, pT2_5);
        textBoxMedSizeSetter(textBox2_5, horzDivInt, vertDivInt);

        textBox2_5.TextChanged += new System.EventHandler(textBox2_5_TextChanged);
        #endregion

        //Row 3
        #region
        //Row 3, column 1
        Point pT3_1 = new Point(xTextP, yTextP + horzDivInt * 2);
        textBox3_1 = new TextBox();
        textBoxCreator(box, textBox3_1, pT3_1);
        textBoxMedSizeSetter(textBox3_1, horzDivInt, vertDivInt);

        textBox3_1.TextChanged += new System.EventHandler(textBox3_1_TextChanged);

        //Row 3, column 2
        Point pT3_2 = new Point(xTextP + vertDivInt, yTextP + horzDivInt * 2);
        textBox3_2 = new TextBox();
        textBoxCreator(box, textBox3_2, pT3_2);
        textBoxMedSizeSetter(textBox3_2, horzDivInt, vertDivInt);

        textBox3_2.TextChanged += new System.EventHandler(textBox3_2_TextChanged);

        //Row 3, column 3
        Point pT3_3 = new Point(xTextP + vertDivInt * 2, yTextP + horzDivInt * 2);
        textBox3_3 = new TextBox();
        textBoxCreator(box, textBox3_3, pT3_3);
        textBoxMedSizeSetter(textBox3_3, horzDivInt, vertDivInt);

        textBox3_3.TextChanged += new System.EventHandler(textBox3_3_TextChanged);

        //Row 3, column 4
        Point pT3_4 = new Point(xTextP + vertDivInt * 3, yTextP + horzDivInt * 2);
        textBox3_4 = new TextBox();
        textBoxCreator(box, textBox3_4, pT3_4);
        textBoxMedSizeSetter(textBox3_4, horzDivInt, vertDivInt);

        textBox3_4.TextChanged += new System.EventHandler(textBox3_4_TextChanged);

        //Row 3, column 5
        Point pT3_5 = new Point(xTextP + vertDivInt * 4, yTextP + horzDivInt * 2);
        textBox3_5 = new TextBox();
        textBoxCreator(box, textBox3_5, pT3_5);
        textBoxMedSizeSetter(textBox3_5, horzDivInt, vertDivInt);

        textBox3_5.TextChanged += new System.EventHandler(textBox3_5_TextChanged);
        #endregion

        //Row 4
        #region
        //Row 4, column 1
        Point pT4_1 = new Point(xTextP, yTextP + horzDivInt * 3);
        textBox4_1 = new TextBox();
        textBoxCreator(box, textBox4_1, pT4_1);
        textBoxMedSizeSetter(textBox4_1, horzDivInt, vertDivInt);

        textBox4_1.TextChanged += new System.EventHandler(textBox4_1_TextChanged);

        //Row 4, column 2
        Point pT4_2 = new Point(xTextP + vertDivInt, yTextP + horzDivInt * 3);
        textBox4_2 = new TextBox();
        textBoxCreator(box, textBox4_2, pT4_2);
        textBoxMedSizeSetter(textBox4_2, horzDivInt, vertDivInt);

        textBox4_2.TextChanged += new System.EventHandler(textBox4_2_TextChanged);

        //Row 4, column 3
        Point pT4_3 = new Point(xTextP + vertDivInt * 2, yTextP + horzDivInt * 3);
        textBox4_3 = new TextBox();
        textBoxCreator(box, textBox4_3, pT4_3);
        textBoxMedSizeSetter(textBox4_3, horzDivInt, vertDivInt);

        textBox4_3.TextChanged += new System.EventHandler(textBox4_3_TextChanged);

        //Row 4, column 4
        Point pT4_4 = new Point(xTextP + vertDivInt * 3, yTextP + horzDivInt * 3);
        textBox4_4 = new TextBox();
        textBoxCreator(box, textBox4_4, pT4_4);
        textBoxMedSizeSetter(textBox4_4, horzDivInt, vertDivInt);

        textBox4_4.TextChanged += new System.EventHandler(textBox4_4_TextChanged);

        //Row 4, column 5
        Point pT4_5 = new Point(xTextP + vertDivInt * 4, yTextP + horzDivInt * 3);
        textBox4_5 = new TextBox();
        textBoxCreator(box, textBox4_5, pT4_5);
        textBoxMedSizeSetter(textBox4_5, horzDivInt, vertDivInt);

        textBox4_5.TextChanged += new System.EventHandler(textBox4_5_TextChanged);

        #endregion

        //Row 5
        #region
        //Row 5, column 1
        Point pT5_1 = new Point(xTextP, yTextP + horzDivInt * 4);
        textBox5_1 = new TextBox();
        textBoxCreator(box, textBox5_1, pT5_1);
        textBoxMedSizeSetter(textBox5_1, horzDivInt, vertDivInt);

        textBox5_1.TextChanged += new System.EventHandler(textBox5_1_TextChanged);

        //Row 5, column 2
        Point pT5_2 = new Point(xTextP + vertDivInt, yTextP + horzDivInt * 4);
        textBox5_2 = new TextBox();
        textBoxCreator(box, textBox5_2, pT5_2);
        textBoxMedSizeSetter(textBox5_2, horzDivInt, vertDivInt);

        textBox5_2.TextChanged += new System.EventHandler(textBox5_2_TextChanged);

        //Row 5, column 3
        Point pT5_3 = new Point(xTextP + vertDivInt * 2, yTextP + horzDivInt * 4);
        textBox5_3 = new TextBox();
        textBoxCreator(box, textBox5_3, pT5_3);
        textBoxMedSizeSetter(textBox5_3, horzDivInt, vertDivInt);

        textBox5_3.TextChanged += new System.EventHandler(textBox5_3_TextChanged);

        //Row 5, column 4
        Point pT5_4 = new Point(xTextP + vertDivInt * 3, yTextP + horzDivInt * 4);
        textBox5_4 = new TextBox();
        textBoxCreator(box, textBox5_4, pT5_4);
        textBoxMedSizeSetter(textBox5_4, horzDivInt, vertDivInt);

        textBox5_4.TextChanged += new System.EventHandler(textBox5_4_TextChanged);

        //Row 5, column 5
        Point pT5_5 = new Point(xTextP + vertDivInt * 4, yTextP + horzDivInt * 4);
        textBox5_5 = new TextBox();
        textBoxCreator(box, textBox5_5, pT5_5);
        textBoxMedSizeSetter(textBox5_5, horzDivInt, vertDivInt);

        textBox5_5.TextChanged += new System.EventHandler(textBox5_5_TextChanged);

        #endregion


        //populate the textboxes IN THE MENU MAKER, they choose what to load
        //populatePlayField(form, box, 3, "123456789");

        //Add sum labels to playfield
        #region
        row1Sum = new Label();
        Point rS1 = new Point((int)box.Width - 30, yTextP);
        sumSetter(box, row1Sum, rS1, "row1Sum");

        row2Sum = new Label();
        Point rS2 = new Point((int)box.Width - 30, yTextP + horzDivInt);
        sumSetter(box, row2Sum, rS2, "row2Sum");

        row3Sum = new Label();
        Point rS3 = new Point((int)box.Width - 30, yTextP + horzDivInt * 2);
        sumSetter(box, row3Sum, rS3, "row3Sum");

        row4Sum = new Label();
        Point rS4 = new Point((int)box.Width - 30, yTextP + horzDivInt * 3);
        sumSetter(box, row4Sum, rS4, "row4Sum");

        row5Sum = new Label();
        Point rS5 = new Point((int)box.Width - 30, yTextP + horzDivInt * 4);
        sumSetter(box, row5Sum, rS5, "row5Sum");



        //column sum labels
        col1Sum = new Label();
        Point cS1 = new Point(xTextP, (int)box.Height - 30);
        sumSetter(box, col1Sum, cS1, "col1Sum");

        col2Sum = new Label();
        Point cS2 = new Point(xTextP + vertDivInt, (int)box.Height - 30);
        sumSetter(box, col2Sum, cS2, "col2Sum");

        col3Sum = new Label();
        Point cS3 = new Point(xTextP + vertDivInt * 2, (int)box.Height - 30);
        sumSetter(box, col3Sum, cS3, "col3Sum");

        col4Sum = new Label();
        Point cS4 = new Point(xTextP + vertDivInt * 3, (int)box.Height - 30);
        sumSetter(box, col4Sum, cS4, "col4Sum");

        col5Sum = new Label();
        Point cS5 = new Point(xTextP + vertDivInt * 4, (int)box.Height - 30);
        sumSetter(box, col5Sum, cS5, "col5Sum");

        #endregion
        //calc sum when populating the thingy
        populatePlayField(targetPlay, box, 5);
        //after populating and adding them
        ComputeAllSum();
        playfieldInit = false; //we are done making the playfield, stop supression of compute sum on change
        pineapple.Dispose();
    }
    #endregion

    //make 7x7 lined box with appropriate font sized text boxes
    //input the form to put the controls are in, and the picturebox we are drawing in
    #region
    public void ChangePlayfieldToHard(Form form, PictureBox box)
	{
        Color black = Color.Black;
        Pen pineapple = new Pen(black, 0);
        playLen = 7; //set global for rest of functions

        // 1/7 for until the play area
        float horzDiv = bottomBoundary / 7.0f; // Lines divide the field in 7
        int yTextP = Convert.ToInt32(horzDiv / 3.3f); //Center the text boxes in the division
        float vertDiv = rightBoundary / 7.0f;
        int xTextP = Convert.ToInt32(vertDiv / 3.3f); // numbers found by trail and error

        int horzDivInt = Convert.ToInt32(horzDiv); //convert to int for help with textbox setting
        int vertDivInt = Convert.ToInt32(vertDiv);
        //Horizontal first
        //first row
        PointF px1_1 = new PointF(0, horzDiv);
        PointF px1_2 = new PointF(rightBoundary, horzDiv);

        //2nd row
        PointF px2_1 = new PointF(0, horzDiv * 2);
        PointF px2_2 = new PointF(rightBoundary, horzDiv * 2);

        //3rd row
        PointF px3_1 = new PointF(0, horzDiv * 3);
        PointF px3_2 = new PointF(rightBoundary, horzDiv * 3);

        //4th
        PointF px4_1 = new PointF(0, horzDiv * 4);
        PointF px4_2 = new PointF(rightBoundary, horzDiv * 4);

        PointF px5_1 = new PointF(0, horzDiv * 5);
        PointF px5_2 = new PointF(rightBoundary, horzDiv * 5);

        PointF px6_1 = new PointF(0, horzDiv * 6);
        PointF px6_2 = new PointF(rightBoundary, horzDiv * 6);
        //Vertical lines

        PointF py1_1 = new PointF(vertDiv, 0);
        PointF py1_2 = new PointF(vertDiv, bottomBoundary);

        PointF py2_1 = new PointF(vertDiv * 2, 0);
        PointF py2_2 = new PointF(vertDiv * 2, bottomBoundary);

        PointF py3_1 = new PointF(vertDiv * 3, 0);
        PointF py3_2 = new PointF(vertDiv * 3, bottomBoundary);

        PointF py4_1 = new PointF(vertDiv * 4, 0);
        PointF py4_2 = new PointF(vertDiv * 4, bottomBoundary);

        PointF py5_1 = new PointF(vertDiv * 5, 0);
        PointF py5_2 = new PointF(vertDiv * 5, bottomBoundary);

        PointF py6_1 = new PointF(vertDiv * 6, 0);
        PointF py6_2 = new PointF(vertDiv * 6, bottomBoundary);


        using (var g = Graphics.FromImage(box.Image))
        {
            //draw horizontal
            g.DrawLine(pineapple, px1_1, px1_2);
            g.DrawLine(pineapple, px2_1, px2_2);
            g.DrawLine(pineapple, px3_1, px3_2);
            g.DrawLine(pineapple, px4_1, px4_2);
            g.DrawLine(pineapple, px5_1, px5_2);
            g.DrawLine(pineapple, px6_1, px6_2);

            //draw vertical
            g.DrawLine(pineapple, py1_1, py1_2);
            g.DrawLine(pineapple, py2_1, py2_2);
            g.DrawLine(pineapple, py3_1, py3_2);
            g.DrawLine(pineapple, py4_1, py4_2);
            g.DrawLine(pineapple, py5_1, py5_2);
            g.DrawLine(pineapple, py6_1, py6_2);
        }

        //textBox1.Size = new System.Drawing.Size((horzDivInt / 2) - 10, vertDivInt / 2);
        //textBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
        //textBox1.Multiline = true;

        //Add text boxes, 7 per row, PointText(row#)_(column#)
        //Row1
        #region
        //Row 1, column 1
        Point pT1_1 = new Point(xTextP, yTextP);
        textBox1_1 = new TextBox();
        textBoxCreator(box, textBox1_1, pT1_1);
        textBoxHardSizeSetter(textBox1_1, horzDivInt, vertDivInt);

        //add text changed event handlers to update the SUM of the row
        textBox1_1.TextChanged += new System.EventHandler(textBox1_1_TextChanged);

        //Row 1, column 2
        Point pT1_2 = new Point(xTextP + vertDivInt, yTextP);
        textBox1_2 = new TextBox();
        textBoxCreator(box, textBox1_2, pT1_2);
        textBoxHardSizeSetter(textBox1_2, horzDivInt, vertDivInt);

        textBox1_2.TextChanged += new System.EventHandler(textBox1_2_TextChanged);

        //Row 1, column 3
        Point pT1_3 = new Point(xTextP + vertDivInt * 2, yTextP);
        textBox1_3 = new TextBox();
        textBoxCreator(box, textBox1_3, pT1_3);
        textBoxHardSizeSetter(textBox1_3, horzDivInt, vertDivInt);

        textBox1_3.TextChanged += new System.EventHandler(textBox1_3_TextChanged);

        //Row 1, column 4
        Point pT1_4 = new Point(xTextP + vertDivInt * 3, yTextP);
        textBox1_4 = new TextBox();
        textBoxCreator(box, textBox1_4, pT1_4);
        textBoxHardSizeSetter(textBox1_4, horzDivInt, vertDivInt);

        textBox1_4.TextChanged += new System.EventHandler(textBox1_4_TextChanged);

        //Row 1, column 5
        Point pT1_5 = new Point(xTextP + vertDivInt * 4, yTextP);
        textBox1_5 = new TextBox();
        textBoxCreator(box, textBox1_5, pT1_5);
        textBoxHardSizeSetter(textBox1_5, horzDivInt, vertDivInt);

        textBox1_5.TextChanged += new System.EventHandler(textBox1_5_TextChanged);

        //Row 1, column 6
        Point pT1_6 = new Point(xTextP + vertDivInt * 5, yTextP);
        textBox1_6 = new TextBox();
        textBoxCreator(box, textBox1_6, pT1_6);
        textBoxHardSizeSetter(textBox1_6, horzDivInt, vertDivInt);

        textBox1_6.TextChanged += new System.EventHandler(textBox1_6_TextChanged);

        //Row 1, column 7
        Point pT1_7 = new Point(xTextP + vertDivInt * 6, yTextP);
        textBox1_7 = new TextBox();
        textBoxCreator(box, textBox1_7, pT1_7);
        textBoxHardSizeSetter(textBox1_7, horzDivInt, vertDivInt);

        textBox1_7.TextChanged += new System.EventHandler(textBox1_7_TextChanged);

        #endregion

        //Row 2
        #region
        //Row 2, column 1
        Point pT2_1 = new Point(xTextP, yTextP + horzDivInt);
        textBox2_1 = new TextBox();
        textBoxCreator(box, textBox2_1, pT2_1);
        textBoxHardSizeSetter(textBox2_1, horzDivInt, vertDivInt);

        textBox2_1.TextChanged += new System.EventHandler(textBox2_1_TextChanged);


        //Row 2, column 2
        Point pT2_2 = new Point(xTextP + vertDivInt, yTextP + horzDivInt);
        textBox2_2 = new TextBox();
        textBoxCreator(box, textBox2_2, pT2_2);
        textBoxHardSizeSetter(textBox2_2, horzDivInt, vertDivInt);

        textBox2_2.TextChanged += new System.EventHandler(textBox2_2_TextChanged);

        //Row 2, column 3
        Point pT2_3 = new Point(xTextP + vertDivInt * 2, yTextP + horzDivInt);
        textBox2_3 = new TextBox();
        textBoxCreator(box, textBox2_3, pT2_3);
        textBoxHardSizeSetter(textBox2_3, horzDivInt, vertDivInt);

        textBox2_3.TextChanged += new System.EventHandler(textBox2_3_TextChanged);

        //Row 2, column 4
        Point pT2_4 = new Point(xTextP + vertDivInt * 3, yTextP + horzDivInt);
        textBox2_4 = new TextBox();
        textBoxCreator(box, textBox2_4, pT2_4);
        textBoxHardSizeSetter(textBox2_4, horzDivInt, vertDivInt);

        textBox2_4.TextChanged += new System.EventHandler(textBox2_4_TextChanged);

        //Row 2, column 5
        Point pT2_5 = new Point(xTextP + vertDivInt * 4, yTextP + horzDivInt);
        textBox2_5 = new TextBox();
        textBoxCreator(box, textBox2_5, pT2_5);
        textBoxHardSizeSetter(textBox2_5, horzDivInt, vertDivInt);

        textBox2_5.TextChanged += new System.EventHandler(textBox2_5_TextChanged);

        //Row 2, column 6
        Point pT2_6 = new Point(xTextP + vertDivInt * 5, yTextP + horzDivInt);
        textBox2_6 = new TextBox();
        textBoxCreator(box, textBox2_6, pT2_6);
        textBoxHardSizeSetter(textBox2_6, horzDivInt, vertDivInt);

        textBox2_6.TextChanged += new System.EventHandler(textBox2_6_TextChanged);

        //Row 2, column 7
        Point pT2_7 = new Point(xTextP + vertDivInt * 6, yTextP + horzDivInt);
        textBox2_7 = new TextBox();
        textBoxCreator(box, textBox2_7, pT2_7);
        textBoxHardSizeSetter(textBox2_7, horzDivInt, vertDivInt);

        textBox2_7.TextChanged += new System.EventHandler(textBox2_7_TextChanged);


        #endregion

        //Row 3
        #region
        //Row 3, column 1
        Point pT3_1 = new Point(xTextP, yTextP + horzDivInt * 2);
        textBox3_1 = new TextBox();
        textBoxCreator(box, textBox3_1, pT3_1);
        textBoxHardSizeSetter(textBox3_1, horzDivInt, vertDivInt);

        textBox3_1.TextChanged += new System.EventHandler(textBox3_1_TextChanged);

        //Row 3, column 2
        Point pT3_2 = new Point(xTextP + vertDivInt, yTextP + horzDivInt * 2);
        textBox3_2 = new TextBox();
        textBoxCreator(box, textBox3_2, pT3_2);
        textBoxHardSizeSetter(textBox3_2, horzDivInt, vertDivInt);

        textBox3_2.TextChanged += new System.EventHandler(textBox3_2_TextChanged);

        //Row 3, column 3
        Point pT3_3 = new Point(xTextP + vertDivInt * 2, yTextP + horzDivInt * 2);
        textBox3_3 = new TextBox();
        textBoxCreator(box, textBox3_3, pT3_3);
        textBoxHardSizeSetter(textBox3_3, horzDivInt, vertDivInt);

        textBox3_3.TextChanged += new System.EventHandler(textBox3_3_TextChanged);

        //Row 3, column 4
        Point pT3_4 = new Point(xTextP + vertDivInt * 3, yTextP + horzDivInt * 2);
        textBox3_4 = new TextBox();
        textBoxCreator(box, textBox3_4, pT3_4);
        textBoxHardSizeSetter(textBox3_4, horzDivInt, vertDivInt);

        textBox3_4.TextChanged += new System.EventHandler(textBox3_4_TextChanged);

        //Row 3, column 5
        Point pT3_5 = new Point(xTextP + vertDivInt * 4, yTextP + horzDivInt * 2);
        textBox3_5 = new TextBox();
        textBoxCreator(box, textBox3_5, pT3_5);
        textBoxHardSizeSetter(textBox3_5, horzDivInt, vertDivInt);

        textBox3_5.TextChanged += new System.EventHandler(textBox3_5_TextChanged);

        //Row 3, column 6
        Point pT3_6 = new Point(xTextP + vertDivInt * 5, yTextP + horzDivInt * 2);
        textBox3_6 = new TextBox();
        textBoxCreator(box, textBox3_6, pT3_6);
        textBoxHardSizeSetter(textBox3_6, horzDivInt, vertDivInt);

        textBox3_6.TextChanged += new System.EventHandler(textBox3_6_TextChanged);

        //Row 3, column 7
        Point pT3_7 = new Point(xTextP + vertDivInt * 6, yTextP + horzDivInt * 2);
        textBox3_7 = new TextBox();
        textBoxCreator(box, textBox3_7, pT3_7);
        textBoxHardSizeSetter(textBox3_7, horzDivInt, vertDivInt);

        textBox3_7.TextChanged += new System.EventHandler(textBox3_7_TextChanged);

        #endregion

        //Row 4
        #region
        //Row 4, column 1
        Point pT4_1 = new Point(xTextP, yTextP + horzDivInt * 3);
        textBox4_1 = new TextBox();
        textBoxCreator(box, textBox4_1, pT4_1);
        textBoxHardSizeSetter(textBox4_1, horzDivInt, vertDivInt);

        textBox4_1.TextChanged += new System.EventHandler(textBox4_1_TextChanged);

        //Row 4, column 2
        Point pT4_2 = new Point(xTextP + vertDivInt, yTextP + horzDivInt * 3);
        textBox4_2 = new TextBox();
        textBoxCreator(box, textBox4_2, pT4_2);
        textBoxHardSizeSetter(textBox4_2, horzDivInt, vertDivInt);

        textBox4_2.TextChanged += new System.EventHandler(textBox4_2_TextChanged);

        //Row 4, column 3
        Point pT4_3 = new Point(xTextP + vertDivInt * 2, yTextP + horzDivInt * 3);
        textBox4_3 = new TextBox();
        textBoxCreator(box, textBox4_3, pT4_3);
        textBoxHardSizeSetter(textBox4_3, horzDivInt, vertDivInt);

        textBox4_3.TextChanged += new System.EventHandler(textBox4_3_TextChanged);

        //Row 4, column 4
        Point pT4_4 = new Point(xTextP + vertDivInt * 3, yTextP + horzDivInt * 3);
        textBox4_4 = new TextBox();
        textBoxCreator(box, textBox4_4, pT4_4);
        textBoxHardSizeSetter(textBox4_4, horzDivInt, vertDivInt);

        textBox4_4.TextChanged += new System.EventHandler(textBox4_4_TextChanged);

        //Row 4, column 5
        Point pT4_5 = new Point(xTextP + vertDivInt * 4, yTextP + horzDivInt * 3);
        textBox4_5 = new TextBox();
        textBoxCreator(box, textBox4_5, pT4_5);
        textBoxHardSizeSetter(textBox4_5, horzDivInt, vertDivInt);

        textBox4_5.TextChanged += new System.EventHandler(textBox4_5_TextChanged);

        //Row 4, column 6
        Point pT4_6 = new Point(xTextP + vertDivInt * 5, yTextP + horzDivInt * 3);
        textBox4_6 = new TextBox();
        textBoxCreator(box, textBox4_6, pT4_6);
        textBoxHardSizeSetter(textBox4_6, horzDivInt, vertDivInt);

        textBox4_6.TextChanged += new System.EventHandler(textBox4_6_TextChanged);

        //Row 4, column 7
        Point pT4_7 = new Point(xTextP + vertDivInt * 6, yTextP + horzDivInt * 3);
        textBox4_7 = new TextBox();
        textBoxCreator(box, textBox4_7, pT4_7);
        textBoxHardSizeSetter(textBox4_7, horzDivInt, vertDivInt);

        textBox4_7.TextChanged += new System.EventHandler(textBox4_7_TextChanged);

        #endregion

        //Row 5
        #region
        //Row 5, column 1
        Point pT5_1 = new Point(xTextP, yTextP + horzDivInt * 4);
        textBox5_1 = new TextBox();
        textBoxCreator(box, textBox5_1, pT5_1);
        textBoxHardSizeSetter(textBox5_1, horzDivInt, vertDivInt);

        textBox5_1.TextChanged += new System.EventHandler(textBox5_1_TextChanged);

        //Row 5, column 2
        Point pT5_2 = new Point(xTextP + vertDivInt, yTextP + horzDivInt * 4);
        textBox5_2 = new TextBox();
        textBoxCreator(box, textBox5_2, pT5_2);
        textBoxHardSizeSetter(textBox5_2, horzDivInt, vertDivInt);

        textBox5_2.TextChanged += new System.EventHandler(textBox5_2_TextChanged);

        //Row 5, column 3
        Point pT5_3 = new Point(xTextP + vertDivInt * 2, yTextP + horzDivInt * 4);
        textBox5_3 = new TextBox();
        textBoxCreator(box, textBox5_3, pT5_3);
        textBoxHardSizeSetter(textBox5_3, horzDivInt, vertDivInt);

        textBox5_3.TextChanged += new System.EventHandler(textBox5_3_TextChanged);

        //Row 5, column 4
        Point pT5_4 = new Point(xTextP + vertDivInt * 3, yTextP + horzDivInt * 4);
        textBox5_4 = new TextBox();
        textBoxCreator(box, textBox5_4, pT5_4);
        textBoxHardSizeSetter(textBox5_4, horzDivInt, vertDivInt);

        textBox5_4.TextChanged += new System.EventHandler(textBox5_4_TextChanged);

        //Row 5, column 5
        Point pT5_5 = new Point(xTextP + vertDivInt * 4, yTextP + horzDivInt * 4);
        textBox5_5 = new TextBox();
        textBoxCreator(box, textBox5_5, pT5_5);
        textBoxHardSizeSetter(textBox5_5, horzDivInt, vertDivInt);

        textBox5_5.TextChanged += new System.EventHandler(textBox5_5_TextChanged);

        //Row 5, column 6
        Point pT5_6 = new Point(xTextP + vertDivInt * 5, yTextP + horzDivInt * 4);
        textBox5_6 = new TextBox();
        textBoxCreator(box, textBox5_6, pT5_6);
        textBoxHardSizeSetter(textBox5_6, horzDivInt, vertDivInt);

        textBox5_6.TextChanged += new System.EventHandler(textBox5_6_TextChanged);

        //Row 5, column 7
        Point pT5_7 = new Point(xTextP + vertDivInt * 6, yTextP + horzDivInt * 4);
        textBox5_7 = new TextBox();
        textBoxCreator(box, textBox5_7, pT5_7);
        textBoxHardSizeSetter(textBox5_7, horzDivInt, vertDivInt);

        textBox5_7.TextChanged += new System.EventHandler(textBox5_7_TextChanged);

        #endregion


        //Row 6
        #region
        //Row 6, column 1
        Point pT6_1 = new Point(xTextP, yTextP + horzDivInt * 5);
        textBox6_1 = new TextBox();
        textBoxCreator(box, textBox6_1, pT6_1);
        textBoxHardSizeSetter(textBox6_1, horzDivInt, vertDivInt);

        textBox6_1.TextChanged += new System.EventHandler(textBox6_1_TextChanged);

        //Row 6, column 2
        Point pT6_2 = new Point(xTextP + vertDivInt, yTextP + horzDivInt * 5);
        textBox6_2 = new TextBox();
        textBoxCreator(box, textBox6_2, pT6_2);
        textBoxHardSizeSetter(textBox6_2, horzDivInt, vertDivInt);

        textBox6_2.TextChanged += new System.EventHandler(textBox6_2_TextChanged);

        //Row 6, column 3
        Point pT6_3 = new Point(xTextP + vertDivInt * 2, yTextP + horzDivInt * 5);
        textBox6_3 = new TextBox();
        textBoxCreator(box, textBox6_3, pT6_3);
        textBoxHardSizeSetter(textBox6_3, horzDivInt, vertDivInt);

        textBox6_3.TextChanged += new System.EventHandler(textBox6_3_TextChanged);

        //Row 6, column 4
        Point pT6_4 = new Point(xTextP + vertDivInt * 3, yTextP + horzDivInt * 5);
        textBox6_4 = new TextBox();
        textBoxCreator(box, textBox6_4, pT6_4);
        textBoxHardSizeSetter(textBox6_4, horzDivInt, vertDivInt);

        textBox6_4.TextChanged += new System.EventHandler(textBox6_4_TextChanged);

        //Row 6, column 5
        Point pT6_5 = new Point(xTextP + vertDivInt * 4, yTextP + horzDivInt * 5);
        textBox6_5 = new TextBox();
        textBoxCreator(box, textBox6_5, pT6_5);
        textBoxHardSizeSetter(textBox6_5, horzDivInt, vertDivInt);

        textBox6_5.TextChanged += new System.EventHandler(textBox6_5_TextChanged);

        //Row 6, column 6
        Point pT6_6 = new Point(xTextP + vertDivInt * 5, yTextP + horzDivInt * 5);
        textBox6_6 = new TextBox();
        textBoxCreator(box, textBox6_6, pT6_6);
        textBoxHardSizeSetter(textBox6_6, horzDivInt, vertDivInt);

        textBox6_6.TextChanged += new System.EventHandler(textBox6_6_TextChanged);

        //Row 6, column 7
        Point pT6_7 = new Point(xTextP + vertDivInt * 6, yTextP + horzDivInt * 5);
        textBox6_7 = new TextBox();
        textBoxCreator(box, textBox6_7, pT6_7);
        textBoxHardSizeSetter(textBox6_7, horzDivInt, vertDivInt);

        textBox6_7.TextChanged += new System.EventHandler(textBox6_7_TextChanged);

        #endregion

        //Row 7
        #region
        //Row 7, column 1
        Point pT7_1 = new Point(xTextP, yTextP + horzDivInt * 6);
        textBox7_1 = new TextBox();
        textBoxCreator(box, textBox7_1, pT7_1);
        textBoxHardSizeSetter(textBox7_1, horzDivInt, vertDivInt);

        textBox7_1.TextChanged += new System.EventHandler(textBox7_1_TextChanged);

        //Row 7, column 2
        Point pT7_2 = new Point(xTextP + vertDivInt, yTextP + horzDivInt * 6);
        textBox7_2 = new TextBox();
        textBoxCreator(box, textBox7_2, pT7_2);
        textBoxHardSizeSetter(textBox7_2, horzDivInt, vertDivInt);

        textBox7_2.TextChanged += new System.EventHandler(textBox7_2_TextChanged);

        //Row 7, column 3
        Point pT7_3 = new Point(xTextP + vertDivInt * 2, yTextP + horzDivInt * 6);
        textBox7_3 = new TextBox();
        textBoxCreator(box, textBox7_3, pT7_3);
        textBoxHardSizeSetter(textBox7_3, horzDivInt, vertDivInt);

        textBox7_3.TextChanged += new System.EventHandler(textBox7_3_TextChanged);

        //Row 7, column 4
        Point pT7_4 = new Point(xTextP + vertDivInt * 3, yTextP + horzDivInt * 6);
        textBox7_4 = new TextBox();
        textBoxCreator(box, textBox7_4, pT7_4);
        textBoxHardSizeSetter(textBox7_4, horzDivInt, vertDivInt);

        textBox7_4.TextChanged += new System.EventHandler(textBox7_4_TextChanged);

        //Row 7, column 5
        Point pT7_5 = new Point(xTextP + vertDivInt * 4, yTextP + horzDivInt * 6);
        textBox7_5 = new TextBox();
        textBoxCreator(box, textBox7_5, pT7_5);
        textBoxHardSizeSetter(textBox7_5, horzDivInt, vertDivInt);

        textBox7_5.TextChanged += new System.EventHandler(textBox7_5_TextChanged);

        //Row 7, column 6
        Point pT7_6 = new Point(xTextP + vertDivInt * 5, yTextP + horzDivInt * 6);
        textBox7_6 = new TextBox();
        textBoxCreator(box, textBox7_6, pT7_6);
        textBoxHardSizeSetter(textBox7_6, horzDivInt, vertDivInt);

        textBox7_6.TextChanged += new System.EventHandler(textBox7_6_TextChanged);

        //Row 7, column 7
        Point pT7_7 = new Point(xTextP + vertDivInt * 6, yTextP + horzDivInt * 6);
        textBox7_7 = new TextBox();
        textBoxCreator(box, textBox7_7, pT7_7);
        textBoxHardSizeSetter(textBox7_7, horzDivInt, vertDivInt);

        textBox7_7.TextChanged += new System.EventHandler(textBox7_7_TextChanged);

        #endregion

        //populate the textboxes IN THE MENU MAKER, they choose what to load
        //populatePlayField(form, box, 3, "123456789");



        //Add sum labels to playfield
        #region
        row1Sum = new Label();
        Point rS1 = new Point((int)box.Width - 30, yTextP);
        sumSetter(box, row1Sum, rS1, "row1Sum");

        row2Sum = new Label();
        Point rS2 = new Point((int)box.Width - 30, yTextP + horzDivInt);
        sumSetter(box, row2Sum, rS2, "row2Sum");

        row3Sum = new Label();
        Point rS3 = new Point((int)box.Width - 30, yTextP + horzDivInt * 2);
        sumSetter(box, row3Sum, rS3, "row3Sum");

        row4Sum = new Label();
        Point rS4 = new Point((int)box.Width - 30, yTextP + horzDivInt * 3);
        sumSetter(box, row4Sum, rS4, "row4Sum");

        row5Sum = new Label();
        Point rS5 = new Point((int)box.Width - 30, yTextP + horzDivInt * 4);
        sumSetter(box, row5Sum, rS5, "row5Sum");

        row6Sum = new Label();
        Point rS6 = new Point((int)box.Width - 30, yTextP + horzDivInt * 5);
        sumSetter(box, row6Sum, rS6, "row6Sum");

        row7Sum = new Label();
        Point rS7 = new Point((int)box.Width - 30, yTextP + horzDivInt * 6);
        sumSetter(box, row7Sum, rS7, "row7Sum");


        //column sum labels
        col1Sum = new Label();
        Point cS1 = new Point( xTextP, (int)box.Height - 30);
        sumSetter(box, col1Sum, cS1, "col1Sum");

        col2Sum = new Label();
        Point cS2 = new Point( xTextP + vertDivInt, (int)box.Height - 30);
        sumSetter(box, col2Sum, cS2, "col2Sum");

        col3Sum = new Label();
        Point cS3 = new Point( xTextP + vertDivInt * 2, (int)box.Height - 30);
        sumSetter(box, col3Sum, cS3, "col3Sum");

        col4Sum = new Label();
        Point cS4 = new Point(xTextP + vertDivInt * 3, (int)box.Height - 30);
        sumSetter(box, col4Sum, cS4, "col4Sum");
        
        col5Sum = new Label();
        Point cS5 = new Point(xTextP + vertDivInt * 4, (int)box.Height - 30);
        sumSetter(box, col5Sum, cS5, "col5Sum");

        col6Sum = new Label();
        Point cS6 = new Point(xTextP + vertDivInt * 5, (int)box.Height - 30);
        sumSetter(box, col6Sum, cS6, "col6Sum");
        
        col7Sum = new Label();
        Point cS7 = new Point(xTextP + vertDivInt * 6, (int)box.Height - 30);
        sumSetter(box, col7Sum, cS7, "col7Sum");

        #endregion
        populatePlayField(targetPlay, box, 7);
        //after populating and adding them
        ComputeAllSum();
        playfieldInit = false; //we are done making the playfield, stop supression of compute sum on change

        pineapple.Dispose();
    }
    #endregion

    // populate the non-zero places from the file into the corresponding text boxes
    //input the form to put the controls are in, and the picturebox we are writing to, and the length of each side of the square
    #region
    public void populatePlayField(Form form, PictureBox box, int sideLen)
    {
        int xPos = 1;
        int yPos = 1;
        string tempStr;
        TextBox tb;
        //loop thru this
        //FileHelper.getTheRow();

        //MessageBox.Show("populate Called");
        if (unsolved.Count < 1)
        {
            //MessageBox.Show("Return Called");
            return; //empty string list, no need to continue
        }

        //loop thru textboxes
        foreach (Control t in box.Controls)
        {
            //MessageBox.Show("Control " + t.Name + " was fouind");
            if (t is TextBox)
            {
                //MessageBox.Show("Found Textbox");
                tb = t as TextBox;
                tempStr = FileHelper.getTheRow(unsolved, yPos);
                tempStr = FileHelper.getElement(tempStr, xPos);

                if(tempStr == "0")
                {
                    tb.Text = "";
                }
                else
                {
                    tb.Text = tempStr;
                }

                if (xPos == sideLen)
                {
                    yPos++;//end of row, go to next y value
                    xPos = 1;
                }
                else { xPos++; }
            }

        }
        initCheckSum(solution);

    }
    #endregion

    //Computes the sum of each row and column for each difficulty, then put the result in the sum labels
    #region
    public void ComputeAllSum()
    {
        //make lists for each difficulty, call ComputeDir Sum for each row & col
        if(playLen == 3)
        {
            //easy difficulty, 3 rows, 3 columns
            #region
            List<TextBox> row1Txt = new List<TextBox>() { textBox1_1, textBox1_2, textBox1_3 };
            List<TextBox> row2Txt = new List<TextBox>() { textBox2_1, textBox2_2, textBox2_3 };
            List<TextBox> row3Txt = new List<TextBox>() { textBox3_1, textBox3_2, textBox3_3 };
            List<TextBox> col1Txt = new List<TextBox>() { textBox1_1, textBox2_1, textBox3_1 };
            List<TextBox> col2Txt = new List<TextBox>() { textBox1_2, textBox2_2, textBox3_2 };
            List<TextBox> col3Txt = new List<TextBox>() { textBox1_3, textBox2_3, textBox3_3 };
            row1Sum.Text = Convert.ToString(ComputeDirectionSum(row1Txt));
            isSumCorrect(row1Sum, row1SumCorr);
            row2Sum.Text = Convert.ToString(ComputeDirectionSum(row2Txt));
            isSumCorrect(row2Sum, row2SumCorr);
            row3Sum.Text = Convert.ToString(ComputeDirectionSum(row3Txt));
            isSumCorrect(row3Sum, row3SumCorr);


            col1Sum.Text = Convert.ToString(ComputeDirectionSum(col1Txt));

            col2Sum.Text = Convert.ToString(ComputeDirectionSum(col2Txt));

            col3Sum.Text = Convert.ToString(ComputeDirectionSum(col3Txt));


            isSumCorrect(col1Sum, col1SumCorr);
            isSumCorrect(col2Sum, col2SumCorr);
            isSumCorrect(col3Sum, col3SumCorr);

            //do the check and color the labels if they are incorrect
            #endregion

        }
        else if(playLen == 5)
        {
            //5 rows, 5 columns
            #region
            List<TextBox> row1Txt = new List<TextBox>() { textBox1_1, textBox1_2, textBox1_3, textBox1_4, textBox1_5 };
            List<TextBox> row2Txt = new List<TextBox>() { textBox2_1, textBox2_2, textBox2_3, textBox2_4, textBox2_5 };
            List<TextBox> row3Txt = new List<TextBox>() { textBox3_1, textBox3_2, textBox3_3, textBox3_4, textBox3_5 };
            List<TextBox> row4Txt = new List<TextBox>() { textBox4_1, textBox4_2, textBox4_3, textBox4_4, textBox4_5 };
            List<TextBox> row5Txt = new List<TextBox>() { textBox5_1, textBox5_2, textBox5_3, textBox5_4, textBox5_5 };


            List<TextBox> col1Txt = new List<TextBox>() { textBox1_1, textBox2_1, textBox3_1, textBox4_1, textBox5_1};
            List<TextBox> col2Txt = new List<TextBox>() { textBox1_2, textBox2_2, textBox3_2, textBox4_2, textBox5_2};
            List<TextBox> col3Txt = new List<TextBox>() { textBox1_3, textBox2_3, textBox3_3, textBox4_3, textBox5_3};
            List<TextBox> col4Txt = new List<TextBox>() { textBox1_4, textBox2_4, textBox3_4, textBox4_4, textBox5_4};
            List<TextBox> col5Txt = new List<TextBox>() { textBox1_5, textBox2_5, textBox3_5, textBox4_5, textBox5_5};


            row1Sum.Text = Convert.ToString(ComputeDirectionSum(row1Txt));
            isSumCorrect(row1Sum, row1SumCorr);
            row2Sum.Text = Convert.ToString(ComputeDirectionSum(row2Txt));
            isSumCorrect(row2Sum, row2SumCorr);
            row3Sum.Text = Convert.ToString(ComputeDirectionSum(row3Txt));
            isSumCorrect(row3Sum, row3SumCorr);
            row4Sum.Text = Convert.ToString(ComputeDirectionSum(row4Txt));
            isSumCorrect(row4Sum, row4SumCorr);
            row5Sum.Text = Convert.ToString(ComputeDirectionSum(row5Txt));
            isSumCorrect(row5Sum, row5SumCorr);


            col1Sum.Text = Convert.ToString(ComputeDirectionSum(col1Txt));

            col2Sum.Text = Convert.ToString(ComputeDirectionSum(col2Txt));

            col3Sum.Text = Convert.ToString(ComputeDirectionSum(col3Txt));

            col4Sum.Text = Convert.ToString(ComputeDirectionSum(col4Txt));

            col5Sum.Text = Convert.ToString(ComputeDirectionSum(col5Txt));


            isSumCorrect(col1Sum, col1SumCorr);
            isSumCorrect(col2Sum, col2SumCorr);
            isSumCorrect(col3Sum, col3SumCorr);
            isSumCorrect(col4Sum, col4SumCorr);
            isSumCorrect(col5Sum, col5SumCorr);



            //do the check and color the labels if they are incorrect
            #endregion
        }
        else if(playLen == 7)
        {
            //7 rows, 7 columns
            #region
            List<TextBox> row1Txt = new List<TextBox>() { textBox1_1, textBox1_2, textBox1_3, textBox1_4, textBox1_4, textBox1_6, textBox1_7 };
            List<TextBox> row2Txt = new List<TextBox>() { textBox2_1, textBox2_2, textBox2_3, textBox2_4, textBox2_4, textBox2_6, textBox2_7 };
            List<TextBox> row3Txt = new List<TextBox>() { textBox3_1, textBox3_2, textBox3_3, textBox3_4, textBox3_4, textBox3_6, textBox3_7 };
            List<TextBox> row4Txt = new List<TextBox>() { textBox4_1, textBox4_2, textBox4_3, textBox4_4, textBox4_4, textBox4_6, textBox4_7 };
            List<TextBox> row5Txt = new List<TextBox>() { textBox5_1, textBox5_2, textBox5_3, textBox5_4, textBox5_4, textBox5_6, textBox5_7 };
            List<TextBox> row6Txt = new List<TextBox>() { textBox6_1, textBox6_2, textBox6_3, textBox6_4, textBox6_4, textBox6_6, textBox6_7 };
            List<TextBox> row7Txt = new List<TextBox>() { textBox7_1, textBox7_2, textBox7_3, textBox7_4, textBox7_4, textBox7_6, textBox7_7 };

            List<TextBox> col1Txt = new List<TextBox>() { textBox1_1, textBox2_1, textBox3_1, textBox4_1, textBox5_1, textBox6_1, textBox7_1 };
            List<TextBox> col2Txt = new List<TextBox>() { textBox1_2, textBox2_2, textBox3_2, textBox4_2, textBox5_2, textBox6_2, textBox7_2 };
            List<TextBox> col3Txt = new List<TextBox>() { textBox1_3, textBox2_3, textBox3_3, textBox4_3, textBox5_3, textBox6_3, textBox7_3 };
            List<TextBox> col4Txt = new List<TextBox>() { textBox1_4, textBox2_4, textBox3_4, textBox4_4, textBox5_4, textBox6_4, textBox7_4 };
            List<TextBox> col5Txt = new List<TextBox>() { textBox1_5, textBox2_5, textBox3_5, textBox4_5, textBox5_5, textBox6_5, textBox7_5 };
            List<TextBox> col6Txt = new List<TextBox>() { textBox1_6, textBox2_6, textBox3_6, textBox4_6, textBox5_6, textBox6_6, textBox7_6 };
            List<TextBox> col7Txt = new List<TextBox>() { textBox1_7, textBox2_7, textBox3_7, textBox4_7, textBox5_7, textBox6_7, textBox7_7 };


            //isSumCorrect(Label sumLabel, int CorrectSum)
            row1Sum.Text = Convert.ToString(ComputeDirectionSum(row1Txt));
            isSumCorrect(row1Sum, row1SumCorr);
            row2Sum.Text = Convert.ToString(ComputeDirectionSum(row2Txt));
            isSumCorrect(row2Sum, row2SumCorr);
            row3Sum.Text = Convert.ToString(ComputeDirectionSum(row3Txt));
            isSumCorrect(row3Sum, row3SumCorr);
            row4Sum.Text = Convert.ToString(ComputeDirectionSum(row4Txt));
            isSumCorrect(row4Sum, row4SumCorr);
            row5Sum.Text = Convert.ToString(ComputeDirectionSum(row5Txt));
            isSumCorrect(row5Sum, row5SumCorr);
            row6Sum.Text = Convert.ToString(ComputeDirectionSum(row6Txt));
            isSumCorrect(row6Sum, row6SumCorr);
            row7Sum.Text = Convert.ToString(ComputeDirectionSum(row7Txt));
            isSumCorrect(row7Sum, row7SumCorr);

            col1Sum.Text = Convert.ToString(ComputeDirectionSum(col1Txt));
            
            col2Sum.Text = Convert.ToString(ComputeDirectionSum(col2Txt));

            col3Sum.Text = Convert.ToString(ComputeDirectionSum(col3Txt));

            col4Sum.Text = Convert.ToString(ComputeDirectionSum(col4Txt));

            col5Sum.Text = Convert.ToString(ComputeDirectionSum(col5Txt));

            col6Sum.Text = Convert.ToString(ComputeDirectionSum(col6Txt));

            col7Sum.Text = Convert.ToString(ComputeDirectionSum(col7Txt));
            isSumCorrect(col1Sum, col1SumCorr);
            isSumCorrect(col2Sum, col2SumCorr);
            isSumCorrect(col3Sum, col3SumCorr);
            isSumCorrect(col4Sum, col4SumCorr);
            isSumCorrect(col5Sum, col5SumCorr);
            isSumCorrect(col6Sum, col6SumCorr);
            isSumCorrect(col7Sum, col7SumCorr);
            #endregion
        }
        checkForWin();
    }
    #endregion

    //Computes the sum of the list of textbox elements, can be either a row or column, just depends on how the list is made
    public static int ComputeDirectionSum(List<TextBox> row)
    {
        int sum = 0;
        //loop thru inputted row
        foreach(var val in row)
        {
            //make sure text is not blank, events keep it valid otherwise
            if(val.Text != "")
            {
                sum += int.Parse(val.Text);
            }
        }

        return sum;
    }

    //Checks the inputted textbox's number vs the input solution's number
    #region
    public bool isTextBoxCorrect(TextBox tb, int solNum)
    {
        if(tb.Text == "")
        {
            //blank square, return
            return true;
        }

        int i;
        if(int.TryParse(tb.Text, out i))
        {
            if(i == solNum)
            {
                //number is correct
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    #endregion

    //Checks the number value of the inputted label with the inputted integer from the solution
    //changes the color to green for ok and red for wrong
    public static void isSumCorrect(Label sumLabel, int CorrectSum)
    {
        //Font drawFont = new Font("Arial", 9);
        //Color corrColor = System.Drawing.Color.Green;
       // Color wrongColor = System.Drawing.Color.Red;
        if (int.Parse(sumLabel.Text) == CorrectSum)
        {
            //Correct sum hit, turn label green
            sumLabel.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            sumLabel.ForeColor = System.Drawing.Color.Red;
        }
    }

    //does the inital calculation of the sum of each row and column based on the solution
    //input the solution string list
    public void initCheckSum(List<string> inSolution)
    {

        int xPos;
        int yPos = 1;
        int tempInt = 0;
        int tempXSum = 0;
        //loop thu rows to get x sums
        foreach (string s in inSolution)
        {
            tempXSum = 0;
            tempInt = 0;
            for (xPos = 1; xPos <= playLen; xPos++)
            {
                tempInt = Convert.ToInt32(FileHelper.getElement(s, xPos));

                //switch to determine which y sum gets this
                switch(xPos)
                {
                   case (1): col1SumCorr += tempInt; break;
                   case (2): col2SumCorr += tempInt; break;
                   case (3): col3SumCorr += tempInt; break;
                   case (4): col4SumCorr += tempInt; break;
                   case (5): col5SumCorr += tempInt; break;
                   case (6): col6SumCorr += tempInt; break;
                   case (7): col7SumCorr += tempInt; break;
                                         
                }
                tempXSum += tempInt;
            }

            //switch to determine what global x sum gets added to
            switch(yPos)
            {
               case(1): row1SumCorr = tempXSum; break;
               case(2): row2SumCorr = tempXSum; break;
               case(3): row3SumCorr = tempXSum; break;
               case(4): row4SumCorr = tempXSum; break;
               case(5): row5SumCorr = tempXSum; break;
               case(6): row6SumCorr = tempXSum; break;
               case(7): row7SumCorr = tempXSum; break;
            }
            yPos++;
        }

    }

    //Collects the current numbers from the textboxes and creates a "current number string" then puts them in list to give to FileHelper
    // input the picturebox playfield for us to collect numbers from
    public void saveNumbersFromPlayfield(PictureBox box)
    {
        int xPos = 1;
        int yPos = 1;
        string tempStr = "";
        TextBox tb;
        //loop thru textboxes
        foreach (Control t in box.Controls)
        {
            //MessageBox.Show("Control " + t.Name + " was fouind");
            if (t is TextBox)
            {
                //MessageBox.Show("Found Textbox");
                tb = t as TextBox;
                if(tb.Text != "")
                {
                    tempStr = tempStr + tb.Text;
                }
                else { tempStr = tempStr + "0"; }
                
                if (xPos == playLen)
                {
                    yPos++;//end of row, go to next y value
                    xPos = 1;
                    //next line, add to sting list
                    tempSave.Add(tempStr);
                    tempStr = "";
                }
                else { xPos++; }
            }

        }

    }


    //check for win status
    //compares each textbox with the solution, stops game and gives win/saves time if player won
    #region
    public void checkForWin()
    {

        int completionTime = timeElapsed;
        
        int xPos = 1;
        int yPos = 1;
        int i;
        string tempStr;
        bool completed = true;
        TextBox tb;


        foreach (Control t in localBox.Controls)
        {
            //MessageBox.Show("Control " + t.Name + " was fouind");
            if (t is TextBox)
            {
                //MessageBox.Show("Found Textbox");
                tb = t as TextBox;
                tempStr = FileHelper.getTheRow(solution, yPos);
                tempStr = FileHelper.getElement(tempStr, xPos);
                //tb.Text = tempStr;
                //Do the check here
                int.TryParse(tempStr, out i);
                if (!isTextBoxCorrect(tb, i))
                {
                    //MessageBox.Show("Wrong number in cell " + xPos.ToString() + "x" + yPos.ToString());
                    completed = false;

                }
                if(tb.Text == "")
                {
                    completed = false;
                    return;
                }

                if (xPos == playLen)
                {
                    yPos++;//end of row, go to next y value
                    xPos = 1;
                }
                else { xPos++; }
            }

        }

        if(completed)
        {
            playTime.Stop();
            if (!gotHint)
            {
                FileHelper.updateCompletionTimes(playLen, completionTime);
            }
            MessageBox.Show("Congrats! You Won!\n " + FileHelper.getCompletionTimes(playLen)+ "\nYour Time: " + completionTime.ToString());
            targetPlay.Close();
        }
    }
    #endregion


    //TextChanged Event functions... all 49 of them!
    #region
    private void textBox1_1_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox1_1);
    }

    private void textBox2_1_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox2_1);
    }

    private void textBox1_3_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox1_3);
    }

    private void textBox1_2_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox1_2);
    }

    private void textBox3_3_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox3_3);
    }

    private void textBox3_2_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox3_2);
    }

    private void textBox3_1_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox3_1);
    }

    private void textBox2_3_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox2_3);
    }

    private  void textBox2_2_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox2_2);
    }


    private  void textBox7_1_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox7_1);
    }

    private void textBox7_2_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox7_2);
    }

    private  void textBox7_3_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox7_3);
    }

    private  void textBox7_4_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox7_4);
    }

    private  void textBox7_5_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox7_5);
    }

    private  void textBox7_7_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox7_7);
    }

    private  void textBox7_6_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox7_6);
    }

    private  void textBox6_7_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox6_7);
    }

    private  void textBox6_6_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox6_6);
    }

    private  void textBox6_5_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox6_5);
    }

    private  void textBox6_4_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox6_4);
    }

    private  void textBox6_3_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox6_3);
    }

    private  void textBox6_2_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox6_2);
    }

    private  void textBox6_1_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox6_1);
    }

    private  void textBox5_1_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox5_1);
    }

    private  void textBox5_7_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox5_7);
    }

    private  void textBox5_6_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox5_6);
    }

    private  void textBox5_5_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox5_5);
    }

    private  void textBox5_4_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox5_4);
    }

    private  void textBox5_3_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox5_3);
    }

    private  void textBox5_2_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox5_2);
    }

    private  void textBox4_7_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox4_7);
    }

    private  void textBox4_6_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox4_6);
    }

    private  void textBox4_5_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox4_5);
    }

    private  void textBox4_4_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox4_4);
    }

    private  void textBox4_3_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox4_3);
    }

    private  void textBox4_2_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox4_2);
    }

    private  void textBox4_1_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox4_1);
    }

    private  void textBox3_7_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox3_7);
    }

    private  void textBox3_6_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox3_6);
    }

    private  void textBox3_5_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox3_5);
    }

    private  void textBox3_4_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox3_4);
    }

    private  void textBox2_7_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox2_7);
    }

    private  void textBox2_6_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox2_6);
    }

    private  void textBox2_5_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox2_5);
    }

    private  void textBox2_4_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox2_4);
    }

    private  void textBox1_7_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox1_7);
    }

    private  void textBox1_6_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox1_6);
    }

    private  void textBox1_5_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox1_5);
    }

    private  void textBox1_4_TextChanged(object sender, EventArgs e)
    {
        TextBoxChangeGeneral(textBox1_4);
    }
    #endregion

    //Common code of all the textbox changed events, checks to make sure the input to the textbox is a number, and defaults to empty
    //input the textbox object to check
    #region
    private void TextBoxChangeGeneral(TextBox box)
    {
        //doing try catch just to make sure dumb input doesn't stop program
        try
        {
            //return if the text was changed back to blank
            if (box.Text == "")
            {
                return;
            }
            //get the number from the text box
            //do quick convert just in case they dumb and input a decimal
            int num = Convert.ToInt32(float.Parse(box.Text));
            //make sure it is not 0, set it as blank if they do
            if (num == 0)
            {
                box.Text = "";
            }

            //make sure it is less than 10
            //set it to 9 if input was over 10
            if (num > 9)
            {
                box.Text = "9";
            }

            //Call the sum function
            if(!playfieldInit)
            {
                ComputeAllSum();
                checkForWin();
            }

        }
        catch
        {
            //just in case they try to put in a negative
            //MessageBox.Show("Enter numbers 1 to 9!");
           box.Text = "";
        }

        listenToKeyPress = false; //wait for next one to be selected
    }
    #endregion
}
