using DiffMatchPatch;
using nicTest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WinOne
{
    public struct Chunk
    {
        public int startpos;
        public int length;
        public Color BackColor;
    }
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        // these are the diffs
        List<Diff> diffs;

        // chunks for formatting the two RTBs:
        List<Chunk> chunklist1;
        List<Chunk> chunklist2;

        // two color lists:
        Color[] colors1 = new Color[3] { Color.LightGreen, Color.LightSalmon, Color.White };
        Color[] colors2 = new Color[3] { Color.LightSalmon, Color.LightGreen, Color.Black };

        // this is the diff object;
        diff_match_patch DIFF = new diff_match_patch();
        private void button1_Click(object sender, EventArgs e)
        {
            diffs = DIFF.diff_main(rtb1.Text, rtb2.Text);
            DIFF.diff_cleanupSemanticLossless(diffs);      // <--- see note !

            chunklist2 = new List<Chunk>();
            chunklist1 = collectChunks(rtb1);       

            foreach (var item in chunklist2)
            {
                rtb1.Select(item.startpos, item.length);
                rtb1.SelectionBackColor = Color.AntiqueWhite;                
            }
            paintChunks(rtb1, chunklist1);


            chunklist2.Clear();
            chunklist1 = collectChunks(rtb2);

            foreach (var item in chunklist2)
            {
                rtb2.Select(item.startpos, item.length);
                rtb2.SelectionBackColor = Color.AntiqueWhite;
            }
            paintChunks(rtb2, chunklist1);



            rtb1.SelectionLength = rtb1.Text.Length;
            rtb2.SelectionLength = rtb2.Text.Length;
        }

        List<Chunk> collectChunks(RichTextBox RTB)
        {
            RTB.Text = "";
            List<Chunk> chunkList = new List<Chunk>();
            int per = -1;
            bool change = false;
            foreach (Diff d in diffs)
            {
                if (RTB == rtb2 && d.operation == Operation.DELETE) continue;
                if (RTB == rtb1 && d.operation == Operation.INSERT) continue;

                Chunk ch = new Chunk();
                int length = RTB.TextLength;
                RTB.AppendText(d.text);
                if (change = !(d.operation == Operation.EQUAL))
                {
                    if (per == -1)
                    {
                        per = RTB.Lines.Length;
                    }
                }
                if (per > -1 && (RTB.Lines.Length > per || diffs.Last() == d))
                {
                    int sum = 0;
                    for (int i = 0; i < (per - 1); i++)
                    {
                        sum += RTB.Lines[i].Length + 1; //每一行要补一个换行符的长度
                    }
                    chunklist2.Add(new Chunk() { startpos = sum, length = RTB.Lines[per - 1].Length + 1 });
                    per = -1;
                }
                ch.startpos = length;
                ch.length = d.text.Length;
                ch.BackColor = colors2[(int)d.operation];
                chunkList.Add(ch);
            }
            return chunkList;

        }

        void paintChunks(RichTextBox RTB, List<Chunk> theChunks)
        {
            foreach (Chunk ch in theChunks)
            {
                RTB.Select(ch.startpos, ch.length);
                RTB.SelectionFont = new Font("微软雅黑", 14, FontStyle.Bold);
                RTB.SelectionColor = ch.BackColor;
               
            }

        }
    }
}
