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

namespace TimSyed_Assign5
{

    public enum difficultyLevels
    {
        easy, medium, hard
    };


    //The first form will act as our play area
    public partial class Form1 : Form
    {

        public PlayfieldCreator Pfc;
        public MenuMaker Mmkr;
        public FileHelper Flhp;
        //We change the form to the menu, then the playfield
        public Form1()
        {
            InitializeComponent();
            Pfc = new PlayfieldCreator();
            
            Flhp = new FileHelper();
            Mmkr = new MenuMaker(this, Pfc, Flhp);
        }

        //Onload make the menu
        private void Form1_Load(object sender, EventArgs e)
        {
            //We first use our menu maker to change the form into the main menu
            Mmkr.MakeMenu(this, Pfc);
            //The menu handles button events in it, then shifts to playfield creator class
        }

        //try to collect general keydown events
        #region
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Not sure if anything but the escape works on this

           // MessageBox.Show("KeyDown Event");
            // Taken from Microsoft C# documentation on keyEvents
            // Determine whether the keystroke is a number from the keypad.
            // Determine whether the keystroke is a number from the top of the keyboard.
            if ((e.KeyCode < Keys.D0 && e.KeyCode > Keys.D9) || (e.KeyCode < Keys.NumPad0 && e.KeyCode > Keys.NumPad9))
            {
                //Keyhit is a number
                Pfc.lastKeyhit = e.KeyValue;
                //MessageBox.Show("KeyDown Event");

                //Pfc.selectedTxtBx;
                //lastKeyhit.KeyValue;
                try
                    {
                        //make sure it is not 0, set it as blank if they do
                        if (Pfc.lastKeyhit == 0)
                        {
                            Pfc.selectedTxtBx.Text = "";
                        }
                        else
                        {
                            //int inNum = Pfc.lastKeyhit;
                            //set the textbox to that number
                            Pfc.selectedTxtBx.Text = Pfc.lastKeyhit.ToString();
                        }
                        //Call the sum function
                        //Pfc.ComputeAllSum();
                    }
                    catch
                    {
                        //just in case they try to put in something wierd
                        MessageBox.Show("Enter numbers 1 to 9!");
                        Pfc.selectedTxtBx.Text = "";
                    }


            }

            //if the escape key hit...
            if(e.KeyCode == Keys.Escape)
            {
                MessageBox.Show("Escape hit");
                Pfc.EscapePlayfield();
            }


        }
        #endregion
    }
}
