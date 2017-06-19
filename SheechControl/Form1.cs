using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using Microsoft.Speech.Recognition;

namespace SheechControl
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SpeechRecognitionEngine recognizer;

        private void RecognizeSpeech(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence < 0.7) return;

            switch(e.Result.Text)
            {
                case "зэлэный": BackColor = Color.Green;break;
                case "червоный": BackColor = Color.Red;break;
                case "жовтый": BackColor = Color.Yellow; break;
                case "зныкны": label1.Visible = false; break;
                case "появысь": label1.Visible = true; break;
                case "закрыйся": Close(); break;
                
            }
                
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            CultureInfo ci = new CultureInfo("ru-ru");
          recognizer = new SpeechRecognitionEngine(ci);
            recognizer.SetInputToDefaultAudioDevice();
            

            recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(RecognizeSpeech);


            Choices numbers = new Choices();
            numbers.Add(new string[] { "зэлэный", "червоный", "жовтый", "зныкны","появысь", "закрыйся"});


            GrammarBuilder gb = new GrammarBuilder()  { Culture = ci };
            gb.Append(numbers);


            Grammar g = new Grammar(gb);
            recognizer.LoadGrammar(g);

            recognizer.RecognizeAsync(RecognizeMode.Multiple);            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            recognizer.RecognizeAsyncStop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }
    }
}
