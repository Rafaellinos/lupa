using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace PIM4SEMVER1._0.GUI
{
    public partial class lupa : Form
    {
        Graphics GraficoCaptura;
        Bitmap ImagemTemporal;
        Point frmMover;
        bool MoverMouse;
        int Zoom = 2; // 1px, 2px, 3px ....

        public lupa()
        {
            InitializeComponent();
            this.TransparencyKey = Color.Turquoise;
            this.BackColor = Color.Turquoise;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //
            int achoImagem = pictureBox1.Width;
            int AltoImagem = pictureBox1.Height;
            //posição do mouse
            int mouseX = MousePosition.X;
            int mouseY = MousePosition.Y;
            //captura tela
            ImagemTemporal = new Bitmap(achoImagem / Zoom, AltoImagem / Zoom, System.Drawing.Imaging.PixelFormat.Format64bppPArgb);
            GraficoCaptura = this.CreateGraphics();
            GraficoCaptura = Graphics.FromImage(ImagemTemporal);
            //copia a tela exata
            GraficoCaptura.CopyFromScreen(mouseX - achoImagem / (Zoom * 2), mouseY - AltoImagem / (Zoom * 2), 0, 0, pictureBox1.Size);

            //aumenta o tamanho
            Bitmap NovaImagem = new Bitmap(achoImagem, AltoImagem);
            GraficoCaptura = Graphics.FromImage(NovaImagem);
            GraficoCaptura.SmoothingMode = SmoothingMode.HighQuality; //qualidade
            GraficoCaptura.DrawImage(ImagemTemporal, new Rectangle(0, 0, achoImagem, AltoImagem));
            pictureBox1.Image = NovaImagem;

            //cria forma circular da lupa
            Rectangle rect = new Rectangle(0, 0, achoImagem, AltoImagem);
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(rect);
            pictureBox1.Region = new Region(path);

            //painel circular
            Rectangle rectp = new Rectangle(0, 0, panel1.Width, panel1.Height);
            GraphicsPath pathp = new GraphicsPath();
            pathp.AddEllipse(rectp);
            panel1.Region = new Region(pathp);

            // Lupa segue o mouse
            this.Location = new Point(Cursor.Position.X, Cursor.Position.Y);

        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (MoverMouse)
            {
                Location = new Point(Cursor.Position.X - frmMover.X, Cursor.Position.Y - frmMover.Y);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            frmMover = new Point(Cursor.Position.X - Location.X, Cursor.Position.Y - Location.Y);
            MoverMouse = true;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            MoverMouse = false;
        }

        private void lupa_KeyDown(object sender, KeyEventArgs e)
        {
            //quando pressiona uma tecla
            if ((e.KeyCode & Keys.Up) == Keys.Up)
            {
                Zoom++; //aumenta zoom
            }
            if ((e.KeyCode & Keys.Down) == Keys.Down)
            {
                if (Zoom > 2)
                {
                    Zoom--; //diminui zoom
                }
            }
            if ((e.KeyCode & Keys.Escape) == Keys.Escape)
            {
                this.Close();
            }
        }

        
    }
}
