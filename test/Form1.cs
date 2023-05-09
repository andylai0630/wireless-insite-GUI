using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add(new { Value = "0", Text = "5.6G" });
            comboBox1.Items.Add(new { Value = "1", Text = "28G" });
            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string filePath = "C:/Users/AndyLai/Desktop/6_G Wireless insite/Dr.Tang_3/AI TEST.txrx";
            string filePath = textBox3.Text;
            string tempFilePath = "C:/Users/AndyLai/Desktop/6_G Wireless insite/AI TEST1.txrx";

            // 開啟檔案以讀取
            StreamReader reader = new StreamReader(filePath);

            // 開啟檔案以寫入
            StreamWriter writer = new StreamWriter(tempFilePath);

            // 取得 textBox1 控制項的文字值
            string power_text = textBox1.Text;
            string ante_text = textBox2.Text;

            string line;
            int ante_flag = 0;
            bool risSection = false;
            while ((line = reader.ReadLine()) != null)
            {
                // 檢查是否進入 RIS 區段
                
                if (line.Contains("begin_<points> RIS"))
                {
                    risSection = true;
                    
                }
                else if (line.Contains("end_<points>"))
                {
                    risSection = false;
                }

                // 如果在 RIS 區段內，並且是 power 欄位，就更改數值
                if (risSection && line.Contains("power "))
                {
                    
                    textBox1.Text = "got it";
                    line = "power " + power_text;

                    // 在 MessageBox 中顯示文字值
                    var selectedItem = (dynamic)comboBox1.SelectedItem;
                    MessageBox.Show("power:" + power_text + "\n" + "antenna no:" + ante_text + "\n" + "waveform:" + selectedItem.Text);

                }
                // 如果在 RIS 區段內，並且是 antenna 欄位，就更改數值
                if (risSection && line.Contains("antenna"))
                {
                    ante_flag++;
                    if(ante_flag==2 && line.Contains("antenna"))
                    {
                        //line = "power " + textBox1.Text;
                        textBox2.Text = "got it";
                        line = "antenna " + ante_text;
                    }
                    
                }
                if (risSection && line.Contains("waveform "))
                {
                    var selectedItem = (dynamic)comboBox1.SelectedItem;
                    line = "waveform " + selectedItem.Value;
                }
                

                // 將修改後的行寫入暫存檔案
                writer.WriteLine(line);
            }

            // 關閉檔案
            reader.Close();
            writer.Close();
            // 刪除原檔案，並將暫存檔案改名為原檔案名稱
            File.Delete(filePath);
            File.Move(tempFilePath, filePath);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 顯示檔案選取對話方塊，並取得所選取的檔案路徑
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                textBox3.Text = filePath;
               
                // ...
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
