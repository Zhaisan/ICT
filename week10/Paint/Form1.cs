using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    enum Tool
    {
        Line,
        Rectangle,
        Pen,
        Fill,
        Fill2,
        Circle,
        Star,
        Eraser,
        Text
    }
    public partial class Form1 : Form
    {
        Bitmap bitmap = default(Bitmap);
        Graphics graphics = default(Graphics);
        Pen pen = new Pen(Color.Black);
        Pen pen2 = new Pen(Color.White, 10);
        Point prevPoint = default(Point);
        Point currentPoint = default(Point);
        Color color = default(Color);
        bool isMousePressed = false;
        Tool currentTool = Tool.Pen;
        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height); 
            graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            pictureBox1.Image = bitmap;
            graphics.Clear(Color.White);
            openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;


        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        bitmap.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        bitmap.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        bitmap.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                bitmap = Bitmap.FromFile(openFileDialog1.FileName) as Bitmap;
                pictureBox1.Image = bitmap;
                graphics = Graphics.FromImage(bitmap);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Pen;
            DialogResult colorResult = colorDialog1.ShowDialog();
            if(colorResult == DialogResult.OK)
            {
                color = colorDialog1.Color;
                pen.Color = color;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Line;
        }


        Rectangle GetMRectangle(Point pPoint, Point cPoint)
        {

            return new Rectangle
            {
                X = Math.Min(pPoint.X, cPoint.X),
                Y = Math.Min(pPoint.Y, cPoint.Y),
                Width = Math.Abs(pPoint.X - cPoint.X),
                Height = Math.Abs(pPoint.Y - cPoint.Y)
            };
        }

        Point[] GetPolygon(Point pPoint, Point cPoint)
        {
            int x1 = Math.Min(pPoint.X, cPoint.X);
            int y1 = Math.Min(pPoint.Y, cPoint.Y);
            int x2 = Math.Max(pPoint.X, cPoint.X);
            int y2 = Math.Max(pPoint.Y, cPoint.Y);

            return new Point[]
            {
                new Point(x1, y2),
                new Point((x1 + x2) / 2, y1),
                new Point(x2, y2),
                new Point(x1, (y1 + y2) / 2),
                new Point(x2, (y1 + y2) / 2)
            };
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = e.Location.ToString();
            if (isMousePressed)
            {
                switch (currentTool)
                {
                    case Tool.Line:
                    case Tool.Rectangle:
                        currentPoint = e.Location;
                        break;
                    case Tool.Circle:
                        currentPoint = e.Location;
                        break;
                    case Tool.Star:
                        currentPoint = e.Location;
                        break;
                    case Tool.Pen:
                        prevPoint = currentPoint;
                        currentPoint = e.Location;
                        graphics.DrawLine(pen, prevPoint, currentPoint);
                        break;
                    case Tool.Eraser:
                        prevPoint = currentPoint;
                        currentPoint = e.Location;
                        graphics.DrawLine(pen2, prevPoint, currentPoint);
                        break;

                    default:
                        break;
                }
                
                pictureBox1.Refresh();
            }
            
            
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            prevPoint = e.Location;
            currentPoint = e.Location;
            isMousePressed = true;
            if (currentTool == Tool.Fill)
            {

                Utils.Fill(bitmap, currentPoint, bitmap.GetPixel(e.X, e.Y), color);
                graphics = Graphics.FromImage(bitmap);
                pictureBox1.Image = bitmap;
                pictureBox1.Refresh();
            }
            else if(currentTool == Tool.Fill2)
            {
                MapFill mf = new MapFill();
                mf.Fill(graphics, currentPoint, color, ref bitmap);
                graphics = Graphics.FromImage(bitmap);
                pictureBox1.Image = bitmap;
                pictureBox1.Refresh();
            }
            else if(currentTool == Tool.Text)
            {
                pictureBox1.Controls.Add(new TextBox() { Name = e.X.ToString() + "," + e.Y.ToString(), Location = new Point(e.X, e.Y), BorderStyle = BorderStyle.None, Size = new Size(100, 100) });
            }

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMousePressed = false;
            
            switch (currentTool)
            {
                case Tool.Line:
                    graphics.DrawLine(pen, prevPoint, currentPoint);
                    break;
                case Tool.Rectangle:
                    graphics.DrawRectangle(pen, GetMRectangle(prevPoint, currentPoint));
                    break;
                case Tool.Circle:
                    graphics.DrawEllipse(pen, GetMRectangle(prevPoint, currentPoint));
                    break;
                case Tool.Star:
                    graphics.DrawPolygon(pen, GetPolygon(prevPoint, currentPoint));
                    break;
                case Tool.Pen:
                    break;
                default:
                    break;
            }
            prevPoint = e.Location;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            switch (currentTool)
            {
                case Tool.Line:
                    e.Graphics.DrawLine(pen, prevPoint, currentPoint);
                    break;
                case Tool.Rectangle:
                    e.Graphics.DrawRectangle(pen, GetMRectangle(prevPoint, currentPoint));
                    break;
                case Tool.Circle:
                    e.Graphics.DrawEllipse(pen, GetMRectangle(prevPoint, currentPoint));
                    break;
                case Tool.Star:
                    e.Graphics.DrawPolygon(pen, GetPolygon(prevPoint, currentPoint));
                    break;
                case Tool.Pen:
                    break;
                default:
                    break;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Rectangle;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void menuStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Fill;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Fill2;   
        }

        private void button6_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Circle;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Star;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Eraser;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            currentTool = Tool.Text;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            pen.Width = Convert.ToSingle(numericUpDown1.Value);
        }
    }
}
