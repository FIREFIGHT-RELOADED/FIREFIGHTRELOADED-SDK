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
        Dictionary<RadioButton, FabType> SelectorValues { get; set; }

        public FabricatorMainForm()
        {
            InitializeComponent();

            CenterToScreen();

            SelectorValues = new Dictionary<RadioButton, FabType>();

            SelectorValues.Add(SpawnlistRadioButton, FabType.Spawnlist);
            SelectorValues.Add(OtherRadioButton, FabType.Other);
            SelectorValues.Add(CatalogRadioButton, FabType.ShopCatalog);
            SelectorValues.Add(PlaylistRadioButton, FabType.Playlist);
            SelectorValues.Add(RewardRadioButton, FabType.RewardList);
            SelectorValues.Add(MapaddRadioButton, FabType.MapAdd);
            SelectorValues.Add(LoadoutRadioButton, FabType.Loadout);

            SelectType(FabType.Spawnlist);
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

        private void SelectType(FabType type)
        {
            LocalVars.SelectedType = type;
            SelectionLabel.Text = $"Selected: {LocalVars.SelectedType.ToString()}";
        }

        private void OpenFileEditor_Click(object sender, EventArgs e)
        {
            FileTypeList.Enabled = false;
            OpenFileEditor.Enabled = false;

            switch (LocalVars.SelectedType)
            {
                case FabType.MapAdd:
                    FabricatorEditorForm_MapAdd formMA = new FabricatorEditorForm_MapAdd();
                    formMA.ShowDialog();
                    break;
                case FabType.Spawnlist:
                    FabricatorEditorForm_Spawnlist formSL = new FabricatorEditorForm_Spawnlist();
                    formSL.ShowDialog();
                    break;
                case FabType.ShopCatalog:
                    FabricatorEditorForm_ShopCatalog formSC = new FabricatorEditorForm_ShopCatalog();
                    formSC.ShowDialog();
                    break;
                case FabType.RewardList:
                    FabricatorEditorForm_RewardList formR = new FabricatorEditorForm_RewardList();
                    formR.ShowDialog();
                    break;
                case FabType.Playlist:
                    FabricatorEditorForm_Playlist formP = new FabricatorEditorForm_Playlist();
                    formP.ShowDialog();
                    break;
                case FabType.Loadout:
                    FabricatorEditorForm_Loadout formL = new FabricatorEditorForm_Loadout();
                    formL.ShowDialog();
                    break;
                case FabType.Other:
                    FabricatorEditorForm_Other form = new FabricatorEditorForm_Other();
                    form.ShowDialog();
                    break;
                default:
                    break;
            }

            FileTypeList.Enabled = true;
            OpenFileEditor.Enabled = true;
        }
    }
}
