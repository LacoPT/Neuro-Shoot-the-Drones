using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neuro_Shoot_the_Drones.MVC
{
    internal abstract class BaseController
    {
        protected BaseModel Model { get; set; }
        protected BaseView View { get; set; }

        protected BaseController(BaseModel model, BaseView view)
        {
            Model = model;
            View = view;
            Model.PropertyChanged += OnModelUpdated;
        }

        protected virtual void OnModelUpdated(object sender, PropertyChangedEventArgs e)
        {
        }
    }
}
