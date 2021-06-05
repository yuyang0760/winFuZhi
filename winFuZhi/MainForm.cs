using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WK.Libraries.HotkeyListenerNS;
using WindowsInput;
using WindowsInput.Native;

namespace winFuZhi
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();



            hotkeyListener.Add(hotkey_fuzhi);
            hotkeyListener.HotkeyPressed += Hkl_HotkeyPressed;
      
        }
        InputSimulator simulator = new InputSimulator();
        // 是否粘贴
        bool isZhanTie = false;
        // 是否回车
        bool isEnter = false;

        // 设置窗口
          ConfigSettings configSettings = new ConfigSettings();

        // 绑定快捷键
        internal static  HotkeyListener hotkeyListener = new HotkeyListener();

        internal static Hotkey hotkey_fuzhi = new Hotkey(Keys.D);
       
        // 题目List
        BindingList<TiMuProp> TiMuPropList = new BindingList<TiMuProp>();

        // 按下快捷键
        private void Hkl_HotkeyPressed(object sender, HotkeyEventArgs e)
        {

            if (this.dataGridView1.CurrentRow==null || this.dataGridView1.CurrentRow.Index < 0 )
            {
                return;
            }
            int CurrentRowIndex = this.dataGridView1.CurrentRow.Index;
            // 按下快捷键
            if (e.Hotkey == hotkey_fuzhi)
            {
                // 复制到剪贴板
                Clipboard.SetText(this.TiMuPropList[CurrentRowIndex].LaiYuan+(isEnter ?"\r\n":""));

                // 粘贴
                if (isZhanTie)
                {
                    this.simulator.Keyboard.KeyDown(VirtualKeyCode.CONTROL);
                    this.simulator.Keyboard.KeyDown(VirtualKeyCode.VK_V);
                    this.simulator.Keyboard.KeyUp(VirtualKeyCode.VK_V);
                    this.simulator.Keyboard.KeyUp(VirtualKeyCode.CONTROL);
                }

                if (this.dataGridView1.CurrentRow.Index+1<this.dataGridView1.Rows.Count)
                {
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[CurrentRowIndex + 1].Cells[0];
                    dataGridView1.Rows[CurrentRowIndex + 1].Selected = true; //  
                }
            }
        }

        // 导入
        private void bt_daoru_Click(object sender, EventArgs e)
        {

        }

        // 行号
        private void dataGridViewMain_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, new SolidBrush(Color.Black), e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + 5);

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = this.checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 获取地址
            string path = "题目.txt";

            // 读取文件
            StreamReader sr = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] linestr = line.Split('【', '】');
                this.TiMuPropList.Add(new TiMuProp(linestr[1], linestr[2]));
            }
            // 绑定 显示
         
            this.dataGridView1.DataSource = TiMuPropList;
        }

        private void btnChangeClippingHotkey_Click(object sender, EventArgs e)
        {
            configSettings.ShowDialog(this);
        }

        private void bt_clear_Click(object sender, EventArgs e)
        {
            this.TiMuPropList.Clear();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                hotkeyListener.Resume();
            }
            else
            {
                hotkeyListener.Suspend();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 先暂停快捷键
            hotkeyListener.Suspend();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            isZhanTie = checkBox3.Checked;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            isEnter = checkBox4.Checked;
        }
    }
}
