using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WK.Libraries.HotkeyListenerNS;

namespace winFuZhi
{
    public partial class ConfigSettings : Form
    {
        public ConfigSettings()
        {
            InitializeComponent();
        }
        internal static HotkeySelector hotkeySelector = new HotkeySelector();
 
        private void ConfigSettings_Load(object sender, EventArgs e)
        {
            hotkeySelector.Enable(txtClippingHotkey, MainForm.hotkey_fuzhi);
            //txtClippingHotkey.Focus();
        }

        private void btnSaveClose_Click(object sender, EventArgs e)
        {
            MainForm.hotkeyListener.Remove(MainForm.hotkey_fuzhi);
            // Update the default clipping hotkey 
            // to the new user-defined hotkey.
            MainForm.hotkeyListener.Update
            (
                // Reference the current clipping hotkey for directly updating 
                // the hotkey without a need for restarting your application.
                ref MainForm.hotkey_fuzhi,

                // Convert the selected hotkey's text representation 
                // to a Hotkey object and update it.
                HotkeyListener.Convert(txtClippingHotkey.Text)
            );

            Close();
        }
    }
}
