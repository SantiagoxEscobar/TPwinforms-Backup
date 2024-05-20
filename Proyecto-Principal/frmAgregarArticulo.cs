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
    public partial class frmAgregarArticulo : Form
    {
        private Articulo articulo = null;
        public frmAgregarArticulo()
        {
            InitializeComponent();
        }

       public frmAgregarArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Editar Artículo";
        }

        private void frmAgregarArticulo_Load(object sender, EventArgs e)
        {
            LecturaCategoria LecturaCategoria = new LecturaCategoria();
            LecturaMarca LecturaMarca = new LecturaMarca();
            try
            {
                cbxCategoria.DataSource = LecturaCategoria.listar();
                cbxCategoria.ValueMember = "Id";
                cbxCategoria.DisplayMember = "Descripcion";
                cbxMarca.DataSource = LecturaMarca.listar();
                cbxMarca.ValueMember = "Id";
                cbxMarca.DisplayMember = "Descripcion";

                if(articulo != null)
                {
                    txtCodigo.Text = articulo.Codigo;
                    txtNombre.Text = articulo.Nombre;
                    txtPrecio.Text = articulo.Precio.ToString();
                    txtDescripcion.Text = articulo.Descripcion;
                    txtImagenUrl.Text = articulo.ImagenUrl;
                    cargarImagen(articulo.ImagenUrl);

                    cbxMarca.SelectedValue = articulo.Marca.Id;
                    cbxCategoria.SelectedValue = articulo.Categoria.Id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            LecturaArticulo lecturaArticulo = new LecturaArticulo();
            try
            {
                if(articulo == null)
                {
                    articulo = new Articulo();
                }
                articulo.Codigo = txtCodigo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.Marca = (Marca)cbxMarca.SelectedItem;
                articulo.Categoria = (Categoria)cbxCategoria.SelectedItem;

                if (txtPrecio.Text == "")
                {
                    txtPrecio.Text = "0";
                }
                else if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
                {
                    lblAdvertencia.Text = "El campo Precio debe ser un número.";
                    articulo = null;
                    return;
                }
                else
                {
                    articulo.Precio = decimal.Parse(txtPrecio.Text);
                }

                if (txtImagenUrl.Text != "" && txtImagenUrl.Text != articulo.ImagenUrl)
                {
                    articulo.ImagenUrl = (string)txtImagenUrl.Text;
                }

                if (articulo.Id != 0)
                {
                    lecturaArticulo.editarArticulo(articulo);
                    lecturaArticulo.editarImagen(articulo);
                    MessageBox.Show("Artículo editado correctamente");
                }
                else
                {
                    if (articulo.Codigo == "")
                    {
                        lblAdvertencia.Text = "Debe completar el campo Código.";
                        articulo = null;
                        return;
                    }
                    else if (articulo.Nombre == "")
                    {
                        lblAdvertencia.Text = "Debe completar el campo Nombre.";
                        articulo = null;
                        return;
                    }
                    else if (articulo.Categoria == null)
                    {
                        lblAdvertencia.Text = "Debe seleccionar una categoría.";
                        articulo = null;
                        return;
                    }
                    else if (articulo.Marca == null)
                    {
                        lblAdvertencia.Text = "Debe seleccionar una marca.";
                        articulo = null;
                        return;
                    }
                    else if (articulo.Precio == 0)
                    {
                        lblAdvertencia.Text = "Debe completar el campo Precio.";
                        articulo = null;
                        return;
                    }
                    if (articulo.Codigo == "" || articulo.Nombre == "" || articulo.Categoria == null || articulo.Marca == null || articulo.Precio == 0)
                    {
                        articulo = null;
                        MessageBox.Show("Debe completar todos los campos.");
                        return;
                    }

                    lecturaArticulo.agregar(articulo);
                    lecturaArticulo.agregarImagen(articulo);
                    MessageBox.Show("Artículo agregado correctamente");
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtImagenUrl_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagenUrl.Text);
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

        
    }
}
