using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Proyecto_Principal
{
    public partial class PaginaPrincipal : Form
    {
        public PaginaPrincipal()
        {
            InitializeComponent();

        }
        private void PaginaPrincipal_Load(object sender, EventArgs e)
        {
            VentanaInicio ventana = new VentanaInicio();
            ventana.MdiParent = this;
            ventana.Show();
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnArticulos_Click(object sender, EventArgs e)
        {
            
            foreach(var item in Application.OpenForms)
            {
                if (item.GetType() == typeof(VentanaListaDeArticulos))
                    return;
            }
            VentanaListaDeArticulos ventana = new VentanaListaDeArticulos();
            ventana.MdiParent = this;
            ventana.Show();          
        }


        private void btnAbout_Click(object sender, EventArgs e)
        {
                foreach (var item in Application.OpenForms)
            {
                if (item.GetType() == typeof(VentanaAbout))
                {
                    return;
                }
            }
            VentanaAbout ventana = new VentanaAbout();
            ventana.MdiParent = this;
            ventana.Show();
        }

        private void btnCategoriasyMarcas_Click_1(object sender, EventArgs e)
        {
            foreach (var item in Application.OpenForms)
            {
                if (item.GetType() == typeof(VentanaCategoriasyMarcas))
                {
                    return;
                }
            }
            VentanaCategoriasyMarcas ventana = new VentanaCategoriasyMarcas();
            ventana.MdiParent = this;
            ventana.Show();
        }
    }
}
