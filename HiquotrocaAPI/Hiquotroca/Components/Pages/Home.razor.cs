using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hiquotroca.Components.Pages
{
    public partial class Home
    {
        List<string> images = new List<string>
        {
            "images/hiquotroca1.png",
            "images/hiquotroca2.png",
            "images/hiquotroca3.png",
            "images/hiquotroca4.png",
            "images/hiquotroca5.png",
            "images/hiquotroca6.png",
        };

        private string _search = "";
        private bool drawer = false;

        private void IrParaPerfil()
        {
            NavigationManager.NavigateTo("/perfil");
        }
    }
}
