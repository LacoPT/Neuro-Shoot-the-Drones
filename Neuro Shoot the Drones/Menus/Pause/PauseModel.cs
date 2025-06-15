using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Neuro_Shoot_the_Drones.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.Menus.Pause
{
    internal class PauseModel : BaseModel
    {
        public List<PauseMenuElement> Options { get; private set; }
        public int SelectedIndex { get; private set; }

        public RenderTarget2D GamePlayLastFrame { get; set; }

        public event Action OnUnpause;
        public event Action OnExit;
        public event Action OnBackToMainMenu;

        public PauseModel()
        {
            Options = new List<PauseMenuElement>
            {
                new(new Vector2(100, 100), "Continue"),
                new(new Vector2(100, 200), "Back to Main Menu"),
                new(new Vector2(100, 300), "Quit")
            };

            SelectedIndex = 0;
            Options[SelectedIndex].Selected = true;

            Options[0].OnActivated += () => OnUnpause?.Invoke();
            Options[1].OnActivated += () => OnBackToMainMenu?.Invoke();
            Options[2].OnActivated += () => OnExit?.Invoke();
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

        public void Unpause()
        {
            OnUnpause?.Invoke();
        }
    }
}
