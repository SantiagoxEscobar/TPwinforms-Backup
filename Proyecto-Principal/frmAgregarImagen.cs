using Dominio;
using LecturaDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Principal
{
    public partial class frmAgregarImagen : Form
    {
        private Articulo selc=null;
        private int Id;
        public frmAgregarImagen(int Id)
        {
            InitializeComponent();
            this.Id = Id;   
        }
        private void cargarImagen(string imagenUrl)
        {
            try
            {
                pbxImagenUrl.Load(imagenUrl);
            }
            catch (Exception)
            {
                pbxImagenUrl.Load("https://storage.googleapis.com/proudcity/mebanenc/uploads/2021/03/placeholder-image.png");
            }
        }

        private void frmAgregarImagen_Load(object sender, EventArgs e)
        {
            cargarImagen((string)txtImagenUrl.Text);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            LecturaArticulo lecturaArticulo = new LecturaArticulo();
            try
            {
                lecturaArticulo.agregarImagen(Id,txtImagenUrl.Text);
                MessageBox.Show("Agregado con exito"); 
                this.Dispose();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void txtImagenUrl_Leave(object sender, EventArgs e)
        {
            cargarImagen((string)txtImagenUrl.Text);
        }

        
    }
}
