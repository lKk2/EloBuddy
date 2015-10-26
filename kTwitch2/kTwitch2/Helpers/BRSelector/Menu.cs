using System;
using System.Windows.Forms;
using BRSelector.Util;

namespace BRSelector.External
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void showTarget_CheckedChanged(object sender, EventArgs e)
        {
            Misc.SetChecked(Selector.DrawMenu, "drawTarget", showTarget.Checked);
        }

        private void selectorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Misc.SetSliderValue(Selector.SelectorMenu, "selectorType", selectorType.SelectedIndex);
        }

        private void forceSelectedTarget_CheckedChanged(object sender, EventArgs e)
        {
            Misc.SetChecked(Selector.SelectorMenu, "forceTarget", forceSelectedTarget.Checked);
        }

        private void drawForcedTarget_CheckedChanged(object sender, EventArgs e)
        {
            Misc.SetChecked(Selector.DrawMenu, "drawForcedTarget", drawForcedTarget.Checked);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Misc.SetSliderValue(Selector.SelectorMenu, trackBar1.Text, trackBar1.Value + 1);
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Misc.SetSliderValue(Selector.SelectorMenu, trackBar2.Text, trackBar2.Value + 1);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            Misc.SetSliderValue(Selector.SelectorMenu, trackBar3.Text, trackBar3.Value + 1);
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            Misc.SetSliderValue(Selector.SelectorMenu, trackBar4.Text, trackBar4.Value + 1);
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            Misc.SetSliderValue(Selector.SelectorMenu, trackBar5.Text, trackBar5.Value + 1);
        }
    }
}
