using System.Collections.Generic;
using System.Linq;

namespace CodeBase.Infrastructure.UI
{
    public class SwitcherPanelUI : ISwitcherPanelUI
    {
        public Dictionary<PanelsUI, IPanelUI> PanelsUI { get; set; }

        public SwitcherPanelUI() => 
            PanelsUI = new Dictionary<PanelsUI, IPanelUI>();

        public void Switch<TPanel>(TPanel panelUI) where TPanel : IPanelUI
        {
            foreach (var item in PanelsUI.Values.Where(item => panelUI as IPanelUI != item))
                item.TakeGameObject.SetActive(false);

            panelUI.TakeGameObject.SetActive(true);
        }
    }
}