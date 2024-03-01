using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WordSearch
{
    public partial class Form1 : Form
    {
        //static string selectedWord = string.Empty;
        string database = @"Data Source=.\TESTSERVER;Initial Catalog=Dev;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        public Form1()
        {
            InitializeComponent();
            textBox1.Select();
        }

        public void searchBtn_Click(object sender, EventArgs e)
        {
            listWords.Items.Clear();
            string zoekWoord = textBox1.Text;
            String[] abc = {"a","b","c","d","e","f","g","h","i","j","k",
                "l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"};

            //String[] abc = { "i" };
            List<String> AList = new List<String>();
            string txtFile = @"C:\Temp\AllLines.txt";

            foreach (string letter in abc)
            {
                if (File.Exists(letter + "-woorden.txt"))
                {
                    StreamReader sra = new StreamReader(letter + "-woorden.txt", Encoding.UTF8);
                    do
                    {
                        string newWord = sra.ReadLine();
                        if (newWord.EndsWith(" "))
                        {
                            newWord = newWord.Replace(" ", "");
                        }
                        AList.Add(newWord);
                    } while (sra.Peek() > -1);
                    File.WriteAllLines(txtFile, AList, Encoding.UTF8);
                }
            }

            List<string> listOfPossibleStrings = WordSearcher.FindPattern(zoekWoord, AList);

            foreach (string word in listOfPossibleStrings)
            {
                int len = word.Length;
                int length = word.Length;
                for (int i = 0; i < len - 1; i++)
                {
                    if (word[i] != len && word[i] == 'i' && word[i + 1] == 'j' ||
                        word[i] != len && word[i] == 'I' && word[i + 1] == 'J')
                    {
                        length--;
                    }
                }
                // Add the word with word-length to the ListBox.
                listWords.Items.Add(word + " (" + length + ")");
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchBtn_Click((object)sender, (EventArgs)e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listWords.Text != string.Empty)
            {
                int wordLength = listWords.Text.Length;
                int index = 0;
                for (int i = 0; i < wordLength; i++)
                {
                    char[] ch = listWords.Text.ToCharArray();
                    if (ch[i] == '(')
                    {
                        index = i;
                    }

                }
                index--;
                string selectedWord = listWords.Text.Remove(index);
                Clipboard.SetText(selectedWord);
            }
            else
            {
                Clipboard.SetText("");
            }
        }
    }
}
