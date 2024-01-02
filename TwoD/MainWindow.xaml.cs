using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using SharpGL.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TwoD
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
        }

        private void openGLControl1_OpenGLInitialized(object sender, OpenGLRoutedEventArgs args)
        {
            OpenGL gl = args.OpenGL;

            //gl.Hint(OpenGL.GL_PERSPECTIVE_CORRECTION_HINT, OpenGL.GL_FASTEST);
            //gl.ShadeModel(OpenGL.GL_SMOOTH);
            //gl.Enable(OpenGL.GL_TEXTURE_2D);
            //gl.Enable(OpenGL.GL_BLEND);
            //gl.Enable(OpenGL.GL_DEPTH_TEST);
            //gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
            //gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);
        }

        int i = 0;
        private Data data = new Data();
        private void openGLControl1_OpenGLDraw(object sender, OpenGLRoutedEventArgs args)
        {
            ////  If there aren't any shapes, create them.
            //if (!shapes.Any())
            //    CreateShapes();

            ////  Get the OpenGL instance.
            //var gl = args.OpenGL;

            //gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //gl.PointSize(2.0f);

            //foreach (var shape in shapes)
            //{
            //    gl.Color(shape.Red, shape.Green, shape.Blue);

            //    gl.Begin(BeginMode.LineLoop);
            //    shape.Points.ForEach(sp => gl.Vertex(sp.Position));
            //    gl.End();
            //}

            //Tick();
            if (i != 1)
                return;

            var gl = args.OpenGL;
            // Clear The Screen And The Depth Buffer
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();

            // ViewPort will see but still need time ~1 sec
            //gl.Viewport(100, 100, 1800, 500);
            //gl.MatrixMode(OpenGL.GL_PROJECTION);
            //gl.LoadIdentity();
            //var ortho = true;
            //if (ortho)
            //{
            //    gl.Ortho2D(1000, 0, 1000, 0);
            //}
            //else
            //{
            //    gl.Frustum(-1.0, 1.0, -1.0, 1.0, 1.5, 20.0);
            //}
            //gl.MatrixMode(OpenGL.GL_MODELVIEW);

            // ***** reduce draw call (gl.Vertex) with same view result by Scale or
            //gl.Scale(5, 1.0, 1.0);


            gl.Color(0.0, 1.0, 0.0);
            //180w
            int width = 1910;
            int height = 950;
            int times = 5;
            // Method 1:　
            // Draw 1910*950*5 points ~ 1 sec 
            // Draw 1910*950*1 points ~ 0.2 sec
            {
                //gl.Begin(BeginMode.Points);
                //for (int i = 0; i < times; i++)
                //{
                //    for (int x = 0; x < width; x++)
                //    {
                //        for (int y = 0; y < height; y++)
                //            gl.Vertex(x, y, 0.0);
                //    }
                //}
                //gl.End();
            }


            // Method 2:　
            // Draw 1910*950*5 points ~ 0.6 sec 
            // Draw 1910*950*1 points ~ 0.13 sec 
            {
                int[] points = new int[]
                {
                    0,0,0,
                    0,1,0,
                    0,2,0,
                    1,0,0,
                    1,1,0,
                    1,2,0,
                    2,0,0,
                    2,1,0,
                    2,2,0,
                };

                int[] points_vertex = new int[width * height * 3];
                int j = 0;
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (j == points_vertex.Length)
                            break;

                        points_vertex[j] = x;
                        points_vertex[j + 1] = y;
                        points_vertex[j + 2] = 0;
                        j += 3;
                    }
                }

                for (int i = 0; i < times; i++)
                {
                    gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);
                    gl.VertexPointer(3, 0, points_vertex);
                    gl.DrawArrays(OpenGL.GL_POINTS, 0, width * height);
                    gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
                }
            }


            // rect 1910
            gl.Color(1.0, 0.0, 0.0);
            gl.Translate(0.0f, 100.0f, 0.0f);
            gl.Rect(0, 0, 1910, 20);

            //triangle
            gl.Begin(BeginMode.LineLoop);
            gl.Color(0.0, 0.0, 1.0);
            gl.Vertex(0, 0, 0);
            gl.Vertex(100, 100, 0);
            gl.Vertex(0, 100, 0);
            gl.End();

            //Draw
            glDrawArray(gl);

        }

        private void glDrawArray(OpenGL gl)
        {
            // Build a rectangle
            float Width = 100.0f;
            float Height = 100.0f;
            float[] vertices = new float[18];
            vertices[0] = 0; vertices[1] = 0; vertices[2] = 0.0f;
            vertices[3] = 0; vertices[4] = Height; vertices[5] = 0.0f;
            vertices[6] = Width; vertices[7] = Height; vertices[8] = 0.0f;

            vertices[9] = 0; vertices[10] = 0; vertices[11] = 0.0f;
            vertices[12] = Width; vertices[13] = Height; vertices[14] = 0.0f;
            vertices[15] = Width; vertices[16] = 0; vertices[17] = 0.0f;

            float[] triangle_rectangle = new[]
            {
                0.0f, 0.0f, 0.0f,
                0.0f, 100.0f, 0.0f,
                100.0f, 100.0f, 0.0f,
                0.0f, 0.0f, 0.0f,
                100.0f, 0.0f, 0.0f,
                100.0f, 100.0f, 0.0f,
            };

            float[] triangle_vertex = new[]
            {
                0.0f, 0.0f, 0.0f,
                0.0f, 100.0f, 0.0f,
                100.0f, 100.0f, 0.0f,
            };

            gl.EnableClientState(OpenGL.GL_VERTEX_ARRAY);

            gl.Translate(100.0f, 100.0f, 0.0f);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.VertexPointer(3, 0, triangle_vertex);
            gl.DrawArrays(OpenGL.GL_TRIANGLES, 0, 3);

            gl.DisableClientState(OpenGL.GL_VERTEX_ARRAY);
            //gl.Flush();
        }

        private void Tick()
        {
            shapes.SelectMany(s => s.Points).ToList().ForEach(sp => ApplyVelocity(ref sp.Position, ref sp.Velocity));
        }

        private void ApplyVelocity(ref Vertex position, ref Vertex velocity)
        {
            float newX = position.X + velocity.X;
            if (newX < 0)
            {
                position.X = -newX;
                velocity.X *= -1;
            }
            else if (newX > openGLControl1.ActualWidth)
            {
                position.X -= (newX - (float)openGLControl1.ActualWidth);
                velocity.X *= -1;
            }
            else
            {
                position.X = newX;
            }


            float newy = position.Y + velocity.Y;
            if (newy < 0)
            {
                position.Y = -newy;
                velocity.Y *= -1;
            }
            else if (newy > openGLControl1.ActualHeight)
            {
                position.Y -= (newy - (float)openGLControl1.ActualHeight);
                velocity.Y *= -1;
            }
            else
            {
                position.Y = newy;
            }
        }

        private void CreateShapes()
        {
            //  Create some shapes...
            int shapeCount = random.Next(2, 5);
            for (int i = 0; i < shapeCount; i++)
            {
                //  Create the shape.
                var shape = new CrazyShape { Red = RandomFloat(), Green = RandomFloat(), Blue = RandomFloat() };

                //  Create the points.
                int pointCount = random.Next(3, 6);
                for (int j = 0; j < pointCount; j++)
                    shape.Points.Add(new ShapePoint
                    {
                        Position = new Vertex(RandomFloat() * (float)openGLControl1.ActualWidth,
                                                 RandomFloat() * (float)openGLControl1.ActualHeight, 0),
                        Velocity = new Vertex(RandomFloat(1f, 10f), RandomFloat(1f, 10f), 0)
                    });

                //  Add the shape.
                shapes.Add(shape);
            }
        }

        private float RandomFloat(float min = 0, float max = 1)
        {
            return (float)random.NextDouble() * (max - min) + min;
        }

        public double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        private void openGLControl1_Resized(object sender, OpenGLRoutedEventArgs args)
        {
            //  Get the OpenGL instance.
            var gl = args.OpenGL;

            //  Create an orthographic projection.
            gl.MatrixMode(MatrixMode.Projection);
            gl.LoadIdentity();
            gl.Ortho(0, openGLControl1.ActualWidth, openGLControl1.ActualHeight, 0, -10, 10);

            //  Back to the modelview.
            gl.MatrixMode(MatrixMode.Modelview);
        }

        private readonly Random random = new Random();

        private readonly List<CrazyShape> shapes = new List<CrazyShape>();


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //  Clear the shapes - they'll be recreated next draw.
            shapes.Clear();

            i = (i + 1) % 2;
        }

    }

    public class CrazyShape
    {
        public List<ShapePoint> Points = new List<ShapePoint>();
        public float Red;
        public float Green;
        public float Blue;
    }

    public class ShapePoint
    {
        public Vertex Position;
        public Vertex Velocity;
    }
}
