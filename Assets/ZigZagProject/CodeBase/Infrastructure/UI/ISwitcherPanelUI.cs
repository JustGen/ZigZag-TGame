using System.Collections.Generic;
using CodeBase.Infrastructure.AssetManagement;

namespace CodeBase.Infrastructure.UI
{
    public interface ISwitcherPanelUI : IService
    {
        Dictionary<PanelsUI, IPanelUI> PanelsUI { get; set; }
        void Switch<TPanel>(TPanel panelUI) where TPanel : IPanelUI;
    }
}