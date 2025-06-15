using Microsoft.Xna.Framework;
using Neuro_Shoot_the_Drones.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Menus.Main
{
    internal class MainMenuModel : BaseModel
    {
        public List<PauseMenuElement> Options { get; private set; }
        public int SelectedIndex { get; private set; }

        public event Action OnStart;
        public event Action OnExit;
        public MainMenuModel()
        {

            Options = new List<PauseMenuElement>
            {
                new(new Vector2(1250, 185), "Start game"),
                new(new Vector2(1330, 285), "Quit")
            };
            Options[0].Selected = true;
            Options[0].OnActivated += Start;
            Options[1].OnActivated += () => OnExit();
        }
        public void SelectNext()
        {
            Options[SelectedIndex].Selected = false;
            SelectedIndex = (SelectedIndex + 1) % Options.Count;
            Options[SelectedIndex].Selected = true;
        }

        public void SelectPrevious()
        {
            Options[SelectedIndex].Selected = false;
            SelectedIndex = (SelectedIndex - 1 + Options.Count) % Options.Count;
            Options[SelectedIndex].Selected = true;
        }

        public void ActivateSelected()
        {
            Options[SelectedIndex].Activate();
        }

        public void Start()
        {
            OnStart?.Invoke();
        }
    }
}
