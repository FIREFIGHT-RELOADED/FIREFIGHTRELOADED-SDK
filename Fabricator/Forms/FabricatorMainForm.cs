using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using ValveKeyValue;
using System.Reflection;

namespace Fabricator
{
    public partial class FabricatorMainForm : Form
    {
        enum Type
        {
            Other,
            Spawnlist,
            Catalog,
            Reward,
            Playlist,
            MapAdd,
            Loadout
        }

        Dictionary<RadioButton, Type> SelectorValues { get; set; }
        Type SelectedType { get; set; }

        public FabricatorMainForm()
        {
            InitializeComponent();

            CenterToScreen();

            SelectorValues = new Dictionary<RadioButton, Type>();

            SelectorValues.Add(SpawnlistRadioButton, Type.Spawnlist);
            SelectorValues.Add(OtherRadioButton, Type.Other);
            SelectorValues.Add(CatalogRadioButton, Type.Catalog);
            SelectorValues.Add(PlaylistRadioButton, Type.Playlist);
            SelectorValues.Add(RewardRadioButton, Type.Reward);
            SelectorValues.Add(MapaddRadioButton, Type.MapAdd);
            SelectorValues.Add(LoadoutRadioButton, Type.Loadout);

            SelectType(Type.Spawnlist);
        }

        private void FabricatorForm_Load(object sender, EventArgs e)
        {

        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var value in SelectorValues)
            {
                RadioButton radioButton = (RadioButton)sender;

                if (radioButton != null)
                {
                    if (value.Key == radioButton)
                    {
                        SelectType(value.Value);
                        break;
                    }
                }
            }
        }

        private void SelectType(Type type)
        {
            SelectedType = type;
            SelectionLabel.Text = $"Selected: {SelectedType.ToString()}";
        }

        private void OpenFileEditor_Click(object sender, EventArgs e)
        {
            switch (SelectedType)
            {
                case Type.Other:
                    FabricatorOtherForm form = new FabricatorOtherForm();
                    form.ShowDialog();
                    break;
                default:
                    break;
            }
        }
    }
}
